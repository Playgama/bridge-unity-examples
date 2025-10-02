using TMPro;
using UnityEngine;

namespace SandboxUI.Scripts
{
    public class PropertyTextView : MonoBehaviour
    {
        private const char Delimiter = ':';

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _name;
        [SerializeField] private Color _valueColor;

        public void SetText(string value)
        {
            _text.text = $"{_name}{Delimiter} <color=#{ColorUtility.ToHtmlStringRGB(_valueColor)}{value}</color>";
        }
    }
}