using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PlaygamaBridge.Examples.SandboxUI.Scripts.Base
{
    public class PropertyTextView : MonoBehaviour
    {
        private const char DELIMITER = ':';
        private const string VALUE_SEPARATOR = "->";

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _name;
        [SerializeField] private Color _valueColor;

        private readonly List<string> _values = new();

        public void SetText(string value)
        {
            _text.text = $"{_name}{DELIMITER} <color=#{ColorUtility.ToHtmlStringRGB(_valueColor)}>{value}</color>";
        }

        public void AddValueAndSet(string value)
        {
            _values.Add(value);
            SetText(string.Join(VALUE_SEPARATOR, _values));
        }

        private void OnDisable()
        {
            _values.Clear();
        }
    }
}