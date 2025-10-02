using System.Collections.Generic;
using Playgama;
using SandboxUI.Scripts.Base;

namespace SandboxUI.Scripts.Screens.Achievements.Requests
{
    public class ShowAchievementsNativePopupBridgeRequestHandler : BaseBridgeRequestHandler
    {
        public override void SendRequest()
        {
            var options = new Dictionary<string, object>()
            {
                ["achievement"] = "Test achievement",
                ["achievementKey"] = "test_achievement_key",
            };

            Bridge.achievements.ShowNativePopup(options);
        }
    }
}