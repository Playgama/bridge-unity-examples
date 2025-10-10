using System.Collections.Generic;
using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Social.Requests
{
    public class CreatePostBridgeRequestHandler : BaseBridgeRequestHandler
    {
        private readonly Dictionary<string, object> _options = new()
        {
            ["media"] = new object[]
            {
                new Dictionary<string, object>
                {
                    { "type", "text" },
                    { "text", "Hello World!" },
                },
                new Dictionary<string, object>
                {
                    { "type", "link" },
                    { "url", "https://apiok.ru" },
                },
                new Dictionary<string, object>
                {
                    { "type", "poll" },
                    { "question", "Do you like our API?" },
                    {
                        "answers",
                        new object[]
                        {
                            new Dictionary<string, object>
                            {
                                { "text", "Yes" },
                            },
                            new Dictionary<string, object>
                            {
                                { "text", "No" },
                            }
                        }
                    },
                    { "options", "SingleChoice,AnonymousVoting" },
                },
            }
        };

        public override void SendRequest()
        {
            Bridge.social.CreatePost(_options);
        }
    }
}