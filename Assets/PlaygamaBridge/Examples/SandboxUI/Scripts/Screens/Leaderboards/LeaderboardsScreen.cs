using Playgama;
using PlaygamaBridge.Examples.SandboxUI.Scripts.Base;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Screens.Leaderboards
{
    public class LeaderboardsScreen : MonoBehaviour
    {
        [SerializeField] private PropertyTextView _leaderboardsType;

        private void OnEnable()
        {
            _leaderboardsType.SetText(Bridge.leaderboards.type.ToString());
        }
    }
}