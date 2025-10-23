using TMPro;
using UnityEngine;

namespace Playgama.Examples.Starter.Scripts.MenuSystem
{
    public abstract class Menu : MonoBehaviour
    {
        protected MenuManager MenuManager;
        
        public void Register(MenuManager menuManager)
        {
            MenuManager = menuManager;
            
            if (!gameObject.activeSelf)
            {
                Activate();
                Deactivate();
            }
        }
        
        public virtual void Open()
        {
            MenuManager.OpenMenu(this);
            InitMenu();
        }
        
        public virtual void Close()
        {
            MenuManager.CloseMenu(this);
        }
        
        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        public void SetAsLastSibling()
        {
            transform.SetAsLastSibling();
        }
        
        protected virtual void Awake()
        {
        }

        protected virtual void Start()
        {
            InitMenu();
        }
        
        protected virtual void OnDestroy()
        {
            MenuManager = null;
        }

        protected virtual void InitMenu()
        {
        }
        protected virtual void SetTextProperty(TextMeshProUGUI text, string label, string value)
        {
            text.text = $"{label}: <color=#D8BBFF>{value}</color>";
        }
    }
}