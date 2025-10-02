using UnityEngine;
using UnityEngine.UI;

namespace SandboxUI.Scripts.Utils
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