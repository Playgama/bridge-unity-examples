using Playgama;
using Playgama.Modules.Platform;
using UnityEngine;

namespace SandboxUI.Scripts
{
    public sealed class BridgePlatformMessageSender : MonoBehaviour
    {
        [SerializeField] private PlatformMessage _platformMessage;

        public void SendBridgeMessage()
        {
            Bridge.platform.SendMessage(_platformMessage);
            Debug.Log($"Message {_platformMessage} sent");
        }
    }
}