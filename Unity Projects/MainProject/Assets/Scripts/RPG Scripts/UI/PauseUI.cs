using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private RectTransform PausePanel;
    [SerializeField] private RectTransform OptionPanel;

    enum Menus {None, PauseMenu, OptionMenu };
    Menus currentMenu;

    void Awake()
    {
        PausePanel.gameObject.SetActive(false);
        OptionPanel.gameObject.SetActive(false);
        currentMenu = Menus.None;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            //if (!StartMenu.MenuObject.activeSelf)
            //{
                switch (currentMenu)
                {
                    case Menus.PauseMenu:
                        ResumeGame();
                        break;
                    case Menus.OptionMenu:
                        CloseOptionsMenu();
                        break;
                    case Menus.None:
                        pauseGame();
                        break;
                }
            }
        //}
    }
    void pauseGame()
    {
        Time.timeScale = 0.0001f;
        currentMenu = Menus.PauseMenu;
        PausePanel.gameObject.SetActive(true);
        MouseCam.EnableCursor(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        currentMenu = Menus.None;
        PausePanel.gameObject.SetActive(false);
        OptionPanel.gameObject.SetActive(false);
        MouseCam.EnableCursor(false);
    }
    public void OpenOptionsMenu()
    {
        currentMenu = Menus.OptionMenu;
        OptionPanel.gameObject.SetActive(true);
    }
    public void CloseOptionsMenu()
    {
        currentMenu = Menus.PauseMenu;
        OptionPanel.gameObject.SetActive(false);
    }
    
}
