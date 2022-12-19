using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using System.IO;
public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Continue;
    public PlayableDirector Director;
    public static GameObject MenuObject;
    void Start()
    {
        MenuObject = this.gameObject;
        //Director.Pause();

        if (!File.Exists(Application.persistentDataPath + "/save.sky"))
        {
            Continue.interactable = false;
        }

        simpleControl.canControlCam = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        Director.time = 0;
    }

    public void StartGame()
    {
        ProgressTracker.Instance.deleteSave();
        Director.Play();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ProgressTracker.Instance.loadProgress();
        DisableUI();
    }
    public void ContinueGame()
    {
        ProgressTracker.Instance.loadProgress();
        Director.time = 1000;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        simpleControl.canControlCam = true;
        DisableUI();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void DisableUI()
    {
        this.gameObject.SetActive(false);
    }
}
