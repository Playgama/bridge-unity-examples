using System.Collections.Generic;
using Newtonsoft.Json;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Payments.Requests
{
    public class GetCatalogBridgeRequestHandler : BaseBridgeRequestWithMultilineResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.payments.GetCatalog(HandleRequestCompleted);
        }

        private void HandleRequestCompleted(bool success, List<Dictionary<string, string>> response)
        {
            if (!success)
            {
                SetResponse("GetCatalog request failed.");
                return;
            }

            var strResponse = JsonConvert.SerializeObject(response);
            SetResponse(strResponse);
        }
    }
}