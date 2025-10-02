using Playgama;
using SandboxUI.Scripts.Base;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.DeviceType
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