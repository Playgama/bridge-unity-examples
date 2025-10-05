using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Examples.Starter.Scripts.MenuSystem
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Transform menuHolder;

        private void Awake()
        {
            foreach (var menu in menuHolder.GetComponentsInChildren<Menu>(true))
            {
                menu.Register(this);
            }
        }
        
        public void OpenMenu(Menu menu)
        {
            menu.Activate();
            menu.SetAsLastSibling();
        }
        
        public void CloseMenu(Menu menu)
        {
            menu.Deactivate();
        }
    }
}