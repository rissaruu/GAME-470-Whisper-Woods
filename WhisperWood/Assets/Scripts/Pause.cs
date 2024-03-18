using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject notebook;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.canPause)
        {
            if (!GameManager.isGamePaused)
            {
                PauseGame();
            }
           else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameManager.isGamePaused = true;
        GameManager.canCamera = false;
        GameManager.canPlayer.walk = false;
        GameManager.canPlayer.run = false;
        GameManager.canPlayer.jump = false;
        GameManager.canPlayer.rotate = false;
        //   Time.timeScale = 0f;
        notebook.SetActive(true); 
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.canCamera = true;
        GameManager.isGamePaused = false;
        GameManager.canPlayer.walk = true;
        GameManager.canPlayer.run = true;
        GameManager.canPlayer.jump = true;
        GameManager.canPlayer.rotate = true;
      //  Time.timeScale = 1f;
        notebook.SetActive(false);
    }
}
