using UnityEngine;
using UnityEngine.UI;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts
{
    public class ScrollRectReseter : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void OnDisable()
        {
            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}