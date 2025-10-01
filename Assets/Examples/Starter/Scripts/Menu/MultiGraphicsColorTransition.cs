namespace Examples.Starter.Scripts.Menu
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections;

    [RequireComponent(typeof(Button))]
    public class MultiGraphicColorTransition : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Target Graphics")] public Image background;
        public Graphic[] graphicsToTint;

        [Header("Colors")] public Color normalColor = Color.white;
        public Color highlightedColor = Color.gray;
        public Color pressedColor = Color.black;
        public Color disabledColor = Color.grey;

        [Header("Transition")] public float fadeDuration = 0.1f;

        private Button _button;
        private Coroutine _currentCoroutine;

        private void Awake()
        {
            _button = GetComponent<Button>();
            SetBackgroundColorInstant(normalColor);
            SetContentColorInstant(highlightedColor);
        }

        private void SetBackgroundColorInstant(Color c)
        {
            if (background)
            {
                background.color = c;
            }
        }

        private void SetContentColorInstant(Color c)
        {
            if (graphicsToTint == null) return;
            
            foreach (var g in graphicsToTint)
            {
                g.color = c;
            }
        }

        private void SetColor(Color targetBackgroundColor, Color targetContentColor)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine = StartCoroutine(FadeColor(targetBackgroundColor, targetContentColor));
        }

        private IEnumerator FadeColor(Color targetBackgroundColor, Color targetContentColor)
        {
            var bgStart = background ? background.color : targetBackgroundColor;
            var graphicsStart = new Color[graphicsToTint.Length];
            for (var i = 0; i < graphicsToTint.Length; i++)
                graphicsStart[i] = graphicsToTint[i].color;

            var elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                var t = Mathf.Clamp01(elapsed / fadeDuration);

                if (background)
                    background.color = Color.Lerp(bgStart, targetBackgroundColor, t);

                for (int i = 0; i < graphicsToTint.Length; i++)
                    graphicsToTint[i].color = Color.Lerp(graphicsStart[i], targetContentColor, t);

                yield return null;
            }
            
            SetBackgroundColorInstant(targetBackgroundColor);
            _currentCoroutine = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                SetColor(highlightedColor, normalColor);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                SetColor(normalColor, highlightedColor);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                SetColor(pressedColor, normalColor);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_button.interactable)
            {
                SetColor(highlightedColor, normalColor);
            }
        }

        private void OnEnable()
        {
            SetBackgroundColorInstant(normalColor);
            SetContentColorInstant(highlightedColor);
        }
    }
}