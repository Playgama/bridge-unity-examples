using System;
using System.Globalization;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Platform.Requests
{
    public sealed class GetServerTimeBridgeRequestHandler : BaseBridgeRequestWithPropertyValueResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.platform.GetServerTime(HandleGetServerTimeResponse);
        }

        private void HandleGetServerTimeResponse(DateTime? result)
        {
            if (!result.HasValue)
            {
                SetResponse("Failed to get server time");
                return;
            }

            SetResponse(result.Value.ToString(CultureInfo.CurrentCulture));
        }
    }
}