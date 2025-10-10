using System.Collections.Generic;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Storage.Requests
{
    public class SaveDataBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private TMP_InputField _coinsCountInput;
        [SerializeField] private TMP_InputField _levelIdInput;
        [SerializeField] private ToggleGroup _toggleGroup;

        public override void SendRequest()
        {
            if (_coinsCountInput.text == string.Empty || _levelIdInput.text == string.Empty)
            {
                return;
            }

            if (!float.TryParse(_coinsCountInput.text, out var coinsCount))
            {
                return;
            }

            var toggle = _toggleGroup.GetFirstActiveToggle();

            if (!toggle)
            {
                return;
            }

            var storageType = StorageToggleUtils.ConvertToggleToStorageType(toggle);

            var data = new List<object> {_levelIdInput.text, coinsCount};

            Bridge.storage.Set(Constants.keys, data, storageType: storageType);
        }

        private void OnDisable()
        {
            _coinsCountInput.text = string.Empty;
            _levelIdInput.text = string.Empty;
        }
    }
}