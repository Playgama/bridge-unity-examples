using System.Collections.Generic;
using Playgama;
using UnityEngine.UIElements;

public class SocialPanelUIHandler : PanelUIHandler {
    private readonly Label isShareSupportedLabel;
    private readonly Label isJoinCommunitySupportedLabel;
    private readonly Label isInviteFriendsSupportedLabel;
    private readonly Label isCreatePostSupportedLabel;
    private readonly Label isAddToFavoritesSupportedLabel;
    private readonly Label isAddToHomeScreenSupportedLabel;
    private readonly Label isRateSupportedLabel;
    private readonly Label isExternalLinksAllowedLabel;

    private readonly Button shareButton;
    private readonly Button joinCommunityButton;
    private readonly Button inviteFriendsButton;
    private readonly Button createPostButton;
    private readonly Button addToFavoritesButton;
    private readonly Button addToHomeScreenButton;
    private readonly Button rateButton;

    public SocialPanelUIHandler(UIDocument uiDocument) : base(uiDocument) {
        isShareSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-share-supported");
        isJoinCommunitySupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-join-community-supported");
        isInviteFriendsSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-invite-friends-supported");
        isCreatePostSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-create-post-supported");
        isAddToFavoritesSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-add-to-favorites-supported");
        isAddToHomeScreenSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-add-to-home-screen-supported");
        isRateSupportedLabel = uiDocument.rootVisualElement.Q<Label>("is-rate-supported");
        isExternalLinksAllowedLabel = uiDocument.rootVisualElement.Q<Label>("is-external-links-allowed");

        shareButton = uiDocument.rootVisualElement.Q<Button>("share");
        joinCommunityButton = uiDocument.rootVisualElement.Q<Button>("join-community");
        inviteFriendsButton = uiDocument.rootVisualElement.Q<Button>("invite-friends");
        createPostButton = uiDocument.rootVisualElement.Q<Button>("create-post");
        addToFavoritesButton = uiDocument.rootVisualElement.Q<Button>("add-to-favorites");
        addToHomeScreenButton = uiDocument.rootVisualElement.Q<Button>("add-to-home-screen");
        rateButton = uiDocument.rootVisualElement.Q<Button>("rate");

        shareButton.RegisterCallback<ClickEvent>(_ => Bridge.social.Share(new Dictionary<string, object> {
            { "some-link", "https://example.com/" }
        }));
        joinCommunityButton.RegisterCallback<ClickEvent>(_ => Bridge.social.JoinCommunity(new Dictionary<string, object> {
            { "groupId", "some-group-id" }
        }));
        inviteFriendsButton.RegisterCallback<ClickEvent>(_ => Bridge.social.InviteFriends(new Dictionary<string, object> {
            { "text", "Hello World!" }
        }));
        createPostButton.RegisterCallback<ClickEvent>(_ => Bridge.social.CreatePost(new Dictionary<string, object> {
            { "type", "text" },
            { "text", "Hello World!" },
        }));
        addToFavoritesButton.RegisterCallback<ClickEvent>(_ => Bridge.social.AddToFavorites());
        addToHomeScreenButton.RegisterCallback<ClickEvent>(_ => Bridge.social.AddToHomeScreen());
        rateButton.RegisterCallback<ClickEvent>(_ => Bridge.social.Rate());
    }

    public override void Toggle(bool enable) {
        base.Toggle(enable);
        var isShareSupported = Bridge.social.isShareSupported;
        isShareSupportedLabel.text = isShareSupported.ToString();
        bool isJoinCommunitySupported = Bridge.social.isJoinCommunitySupported;
        isJoinCommunitySupportedLabel.text = isJoinCommunitySupported.ToString();
        var isInviteFriendsSupported = Bridge.social.isInviteFriendsSupported;
        isInviteFriendsSupportedLabel.text = isInviteFriendsSupported.ToString();
        var isCreatePostSupported = Bridge.social.isCreatePostSupported;
        isCreatePostSupportedLabel.text = isCreatePostSupported.ToString();
        var isAddToFavoritesSupported = Bridge.social.isAddToFavoritesSupported;
        isAddToFavoritesSupportedLabel.text = isAddToFavoritesSupported.ToString();
        var isAddToHomeScreenSupported = Bridge.social.isAddToHomeScreenSupported;
        isAddToHomeScreenSupportedLabel.text = isAddToHomeScreenSupported.ToString();
        var isRateSupported = Bridge.social.isRateSupported;
        isRateSupportedLabel.text = isRateSupported.ToString();
        isExternalLinksAllowedLabel.text = Bridge.social.isExternalLinksAllowed.ToString();

        shareButton.SetEnabled(isShareSupported);
        joinCommunityButton.SetEnabled(isJoinCommunitySupported);
        inviteFriendsButton.SetEnabled(isInviteFriendsSupported);
        createPostButton.SetEnabled(isCreatePostSupported);
        addToFavoritesButton.SetEnabled(isAddToFavoritesSupported);
        addToHomeScreenButton.SetEnabled(isAddToHomeScreenSupported);
        rateButton.SetEnabled(isRateSupported);
    }
}