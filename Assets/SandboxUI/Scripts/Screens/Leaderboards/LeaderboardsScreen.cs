using System;
using Playgama;
using SandboxUI.Scripts.Base;
using UnityEngine;

namespace SandboxUI.Scripts.Screens.Leaderboards
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