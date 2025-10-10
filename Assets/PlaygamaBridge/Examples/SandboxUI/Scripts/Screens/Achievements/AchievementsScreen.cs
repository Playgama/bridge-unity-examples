using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Achievements
{
    public class AchievementsScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _isAchievementsSupported;
        [SerializeField] private PropertyTextView _isGetListSupported;
        [SerializeField] private PropertyTextView _isNativePopupSupported;

        private void OnEnable()
        {
            _isAchievementsSupported.SetText(Bridge.achievements.isSupported.ToString());
            _isGetListSupported.SetText(Bridge.achievements.isGetListSupported.ToString());
            _isNativePopupSupported.SetText(Bridge.achievements.isNativePopupSupported.ToString());
        }
    }
}