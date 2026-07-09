using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using Playgama.Modules.Tasks;
using UnityEngine.UIElements;

public class TasksPanelUIHandler : PanelUIHandler {
    private readonly Label responseLabel;

    private readonly Button getTasksButton;
    private readonly Button addProgressButton;
    private readonly Button claimRewardButton;

    private readonly TextField metricField;
    private readonly TextField amountField;
    private readonly TextField taskIdField;

    public TasksPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        responseLabel = uiDocument.rootVisualElement.Q<Label>("response");

        getTasksButton = uiDocument.rootVisualElement.Q<Button>("get-tasks");
        addProgressButton = uiDocument.rootVisualElement.Q<Button>("add-progress");
        claimRewardButton = uiDocument.rootVisualElement.Q<Button>("claim-reward");
        metricField = uiDocument.rootVisualElement.Q<TextField>("metric-field");
        amountField = uiDocument.rootVisualElement.Q<TextField>("amount-field");
        taskIdField = uiDocument.rootVisualElement.Q<TextField>("task-id-field");

        getTasksButton.RegisterCallback<ClickEvent>(_ => Bridge.tasks.GetTasks(OnGetTasks));
        addProgressButton.RegisterCallback<ClickEvent>(_ => {
            if (!int.TryParse(amountField.value, out var amount) || amount <= 0) {
                amount = 1;
            }

            // AddProgress reports only success; read updated state via GetTasks
            Bridge.tasks.AddProgress(metricField.value, amount, OnAddProgress);
        });
        claimRewardButton.RegisterCallback<ClickEvent>(_ => Bridge.tasks.ClaimReward(taskIdField.value, OnClaimReward));
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        metricField.value = string.Empty;
        amountField.value = string.Empty;
        taskIdField.value = string.Empty;
        responseLabel.text = string.Empty;
    }

    private void OnGetTasks(bool success, List<Task> tasks) {
        responseLabel.text = success ? JsonConvert.SerializeObject(tasks) : "getTasks failed";
    }

    private void OnAddProgress(bool success) {
        responseLabel.text = "addProgress: " + success;
        // refresh the list to show updated progress
        Bridge.tasks.GetTasks(OnGetTasks);
    }

    // claimReward returns whether the claim succeeded; rewards to grant are on Task.rewards
    private void OnClaimReward(bool claimed) {
        responseLabel.text = "claimReward: " + claimed;
    }
}
