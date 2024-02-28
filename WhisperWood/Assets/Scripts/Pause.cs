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
        if (Input.GetKeyDown(KeyCode.Escape))
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
        
        GameManager.isGamePaused = true;
        Time.timeScale = 0f;
        notebook.SetActive(true); //currently need to fix functionality while paused - Damian
    }

    public void ResumeGame()
    {
        
        GameManager.isGamePaused = false;
        Time.timeScale = 1f;
        notebook.SetActive(false);
    }
}
