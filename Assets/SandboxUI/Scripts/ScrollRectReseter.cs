using UnityEngine;
using UnityEngine.UI;

namespace SandboxUI.Scripts
{
    public class ScrollRectReseter : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;

        private void OnDisable()
        {
            _scrollRect.verticalNormalizedPosition = 0;
        }
    }
}