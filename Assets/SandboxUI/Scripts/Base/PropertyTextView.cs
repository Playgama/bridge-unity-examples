using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts
{
    public class PropertyTextView : MonoBehaviour
    {
        private const char Delimiter = ':';
        private const string ValueSeparator = "->";

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _name;
        [SerializeField] private Color _valueColor;

        private readonly List<string> _values = new();

        public void SetText(string value)
        {
            _text.text = $"{_name}{Delimiter} <color=#{ColorUtility.ToHtmlStringRGB(_valueColor)}>{value}</color>";
        }

        public void AddValueAndSet(string value)
        {
            _values.Add(value);
            SetText(string.Join(ValueSeparator, _values));
        }

        private void OnDisable()
        {
            _values.Clear();
        }
    }
}