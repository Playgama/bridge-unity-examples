using System.Collections.Generic;
using Playgama;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SandboxUI.Scripts.Requests
{
    public class LoadDataBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private TMP_InputField _coinsCountInput;
        [SerializeField] private TMP_InputField _levelIdInput;
        [SerializeField] private ToggleGroup _toggleGroup;

        public override void SendRequest()
        {
            var toggle = _toggleGroup.GetFirstActiveToggle();

            if (!toggle)
            {
                return;
            }

            var storageType = StorageToggleUtils.ConvertToggleToStorageType(toggle);

            Bridge.storage.Get(Constants.Keys, HandleStorageGetCompleted, storageType);
        }

        private void HandleStorageGetCompleted(bool success, List<string> data)
        {
            if (!success)
                return;

            _coinsCountInput.text = data[0];
            _levelIdInput.text = data[1];
        }

        private void OnDisable()
        {
            _coinsCountInput.text = string.Empty;
            _levelIdInput.text = string.Empty;
        }
    }
}