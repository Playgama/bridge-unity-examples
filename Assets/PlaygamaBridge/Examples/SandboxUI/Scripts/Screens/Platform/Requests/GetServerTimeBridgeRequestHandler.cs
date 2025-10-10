using System;
using System.Globalization;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Platform.Requests
{
    public sealed class GetServerTimeBridgeRequestHandler : BaseBridgeRequestWithPropertyValueResponseHandler
    {
        public override void SendRequest()
        {
            Bridge.platform.GetServerTime(OnGetServerTimeResponse);
        }

        private void OnGetServerTimeResponse(DateTime? result)
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