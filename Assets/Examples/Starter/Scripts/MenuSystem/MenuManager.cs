using UnityEngine;

namespace Playgama.Examples.Starter.Scripts.MenuSystem
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Transform menuHolder;

        public void OpenMenu(Menu menu)
        {
            menu.Activate();
            menu.SetAsLastSibling();
        }

        public void CloseMenu(Menu menu)
        {
            menu.Deactivate();
        }

        private void Awake()
        {
            foreach (var menu in menuHolder.GetComponentsInChildren<Menu>(true))
            {
                menu.Register(this);
            }
        }
    }
}