using Playgama;
using SandboxUI.Scripts.Base;
using UnityEngine;

namespace SandboxUI.Scripts.Requests
{
    public class CheckAdBlockBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private GameObject _adBlockDetectedObject;

        public override void SendRequest()
        {
            Bridge.advertisement.CheckAdBlock(HandleCheckAdBlock);
        }

        private void HandleCheckAdBlock(bool adBlockDetected)
        {
            _adBlockDetectedObject.SetActive(adBlockDetected);
        }

        private void OnDisable()
        {
            _adBlockDetectedObject.SetActive(false);
        }
    }
}