using System;
using System.Threading;
using Playgama;
using Playgama.Common;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Player
{
    public class AvatarLoader : Singleton<AvatarLoader>
    {
        private CancellationTokenSource _loadTokenSource = new();

        protected override void Awake()
        {
            base.Awake();
            _ = destroyCancellationToken;
        }

        public void InitializeAvatars(RawImage[] targetImages, Texture2D defaultAvatar)
        {
            if (_loadTokenSource != null && !_loadTokenSource.IsCancellationRequested)
            {
                _loadTokenSource.Cancel();
                _loadTokenSource.Dispose();
            }

            _loadTokenSource = CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);

            var playerAvatars = Bridge.player.photos;
            if (playerAvatars != null && playerAvatars.Count > 0)
            {
                for (var i = 0; i < playerAvatars.Count; i++)
                {
                    if (i >= targetImages.Length)
                    {
                        break;
                    }

                    RetrieveAndSetTexture(playerAvatars[i], targetImages[i], defaultAvatar, _loadTokenSource.Token);
                }
            }
            else
            {
                foreach (var playerAvatar in targetImages)
                {
                    playerAvatar.texture = defaultAvatar;
                }
            }
        }


        private async Awaitable RetrieveAndSetTexture(string url, RawImage targetImage, Texture2D defaultAvatar, CancellationToken token)
        {
            try
            {
                var request = UnityWebRequestTexture.GetTexture(url);
                Debug.Log($"Sending web request for user's avatar: {url}");

                await request.SendWebRequest();
                if (token.IsCancellationRequested)
                {
                    return;
                }

                if (!targetImage)
                {
                    return;
                }

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogWarning($"Failed to retrieve player's avatar.\nurl: {url}\nerror: {request.error}");
                    targetImage.texture = defaultAvatar;
                }
                else
                {
                    var texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                    targetImage.texture = texture;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}