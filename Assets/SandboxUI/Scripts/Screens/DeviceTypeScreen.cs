using Playgama;
using UnityEngine;

namespace SandboxUI.Scripts.Screens
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