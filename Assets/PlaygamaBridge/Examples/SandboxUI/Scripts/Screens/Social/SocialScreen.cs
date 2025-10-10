using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Social
{
    public class SocialScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _isShareSupported;
        [SerializeField] private PropertyTextView _isJoinCommunitySupported;
        [SerializeField] private PropertyTextView _isInviteFriendsSupported;
        [SerializeField] private PropertyTextView _isCreatePostSupported;
        [SerializeField] private PropertyTextView _isAddToFavoritesSupported;
        [SerializeField] private PropertyTextView _isAddToHomeScreenSupported;
        [SerializeField] private PropertyTextView _isRateSupported;
        [SerializeField] private PropertyTextView _isExternalLinksAllowed;

        private void OnEnable()
        {
            _isShareSupported.SetText(Bridge.social.isShareSupported.ToString());
            _isJoinCommunitySupported.SetText(Bridge.social.isJoinCommunitySupported.ToString());
            _isInviteFriendsSupported.SetText(Bridge.social.isInviteFriendsSupported.ToString());
            _isCreatePostSupported.SetText(Bridge.social.isCreatePostSupported.ToString());
            _isAddToFavoritesSupported.SetText(Bridge.social.isAddToFavoritesSupported.ToString());
            _isAddToHomeScreenSupported.SetText(Bridge.social.isAddToHomeScreenSupported.ToString());
            _isRateSupported.SetText(Bridge.social.isRateSupported.ToString());
            _isExternalLinksAllowed.SetText(Bridge.social.isExternalLinksAllowed.ToString());
        }
    }
}