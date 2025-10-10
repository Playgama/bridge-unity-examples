using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.DeviceType
{
    public class DeviceTypeScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _deviceType;

        private void OnEnable()
        {
            _deviceType.SetText(Bridge.device.type.ToString());
        }
    }
}