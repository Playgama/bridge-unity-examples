using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SandboxUI.Scripts
{
    public class ButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private UnityEvent _onClick;

        [Header("Styles/General")]
        [SerializeField] private Graphic[] _elements;
        [SerializeField] private Graphic _bg;
        [SerializeField] private GameObject _arrow;

        [Header("Style/Normal")]
        [SerializeField] private Color _elementNormalColor;
        [SerializeField] private Color _bgNormalColor;

        [Header("Style/Hover")]
        [SerializeField] private Color _elementHoverColor;
        [SerializeField] private Color _bgHoverColor;

        private void Awake()
        {
            if (_arrow)
            {
                _arrow.SetActive(false);
            }

            SetNormalState();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetHoverState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click");
            _onClick.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetNormalState();
        }

        [ContextMenu("Button State/Normal")]
        private void SetNormalState()
        {
            foreach (var graphic in _elements)
            {
                graphic.color = _elementNormalColor;
            }

            _bg.color = _bgNormalColor;

            if (_arrow)
            {
                _arrow.SetActive(false);
            }
        }

        [ContextMenu("Button State/Hover")]
        private void SetHoverState()
        {
            foreach (var graphic in _elements)
            {
                graphic.color = _elementHoverColor;
            }

            _bg.color = _bgHoverColor;

            if (_arrow)
            {
                _arrow.SetActive(true);
            }
        }
    }
}
