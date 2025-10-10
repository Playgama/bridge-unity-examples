using System.Collections.Generic;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Storage.Requests
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

            Bridge.storage.Get(Constants.keys, OnStorageGetCompleted, storageType);
        }

        private void OnStorageGetCompleted(bool success, List<string> data)
        {
            if (!success)
                return;

            _levelIdInput.text = data[0];
            _coinsCountInput.text = data[1];
        }

        private void OnDisable()
        {
            _coinsCountInput.text = string.Empty;
            _levelIdInput.text = string.Empty;
        }
    }
}