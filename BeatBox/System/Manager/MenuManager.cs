using System;
using UnityEngine;

namespace BeatBox.System.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager instance;
        public MenuObject currentMenu = null;

        public void ChangeMenu(MenuObject newMenu)
        {
            if (currentMenu) currentMenu.Deactivate();
            
            currentMenu = newMenu;
            
            newMenu.Activate();
        }

        public void DeselectMenu()
        {
            if (currentMenu) currentMenu.Deactivate();
            currentMenu = null;
        }

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Cube.Cube.inputManager.pressDownKeyUp)
            {
                if (currentMenu) currentMenu.RemoveIndex();
                Debug.Log("added");
            }

            if (Cube.Cube.inputManager.pressDownKeyDown)
            {
                if (currentMenu) currentMenu.AddIndex();
                Debug.Log("decreased");
            }
        }
    }
}