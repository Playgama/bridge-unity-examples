using System;
using System.Globalization;
using Playgama;

namespace SandboxUI.Scripts.Requests
{
    public sealed class GetServerTimeRequestHandler : BaseRequestWithPropertyValueResponseHandler
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