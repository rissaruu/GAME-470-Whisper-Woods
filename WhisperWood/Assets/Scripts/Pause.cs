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
        notebook.SetActive(true);
        GameManager.isGamePaused = true;
        
    }

    public void ResumeGame()
    {
        notebook.SetActive(false);
        GameManager.isGamePaused = false;
        
    }
}
