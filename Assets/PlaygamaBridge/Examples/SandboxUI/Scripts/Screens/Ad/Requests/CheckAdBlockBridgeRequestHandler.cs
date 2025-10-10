using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Ad.Requests
{
    public class CheckAdBlockBridgeRequestHandler : BaseBridgeRequestHandler
    {
        [SerializeField] private GameObject _adBlockDetectedObject;

        public override void SendRequest()
        {
            Bridge.advertisement.CheckAdBlock(OnCheckAdBlock);
        }

        private void OnCheckAdBlock(bool adBlockDetected)
        {
            _adBlockDetectedObject.SetActive(adBlockDetected);
        }

        private void OnDisable()
        {
            _adBlockDetectedObject.SetActive(false);
        }
    }
}