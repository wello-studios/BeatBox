using System;
using UnityEngine;

/* { MenuManager }
 *
 * [VARIABLE]
 * MenuObject currentMenu
 *   현재 상호작용중인 Menu 스크립트
 *
 * [METHOD]
 * void ChangeMenu ( MenuObject newMenu )
 *   상호작용중인 메뉴를 newMenu로 변경한다.
 * void DeselectMenu
 *   아무런 메뉴와도 상호작용중이지 않은 상태로 전환한다.
 * void ModifyMenu
 *   메뉴 요소 사이의 이동을 제어한다.
 * 
 * [UNITY EVENT]
 * AWAKE - IN. TH.
 * START -
 * UPDATE - (bool)currentMenu가 True일때 ModifyMenu를 호출한다.
 */

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

        public void ModifyMenu()
        {
            if (Cube.Cube.inputManager.pressDownKeyUp)
            {
                currentMenu.RemoveIndex();
                Debug.Log("added");
            }

            if (Cube.Cube.inputManager.pressDownKeyDown)
            {
                currentMenu.AddIndex();
                Debug.Log("decreased");
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (currentMenu) ModifyMenu();
        }
    }
}