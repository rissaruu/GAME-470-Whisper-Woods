using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Button accuseButton;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.DisablePlayer();
        accuseButton.onClick.AddListener(()=> OnAccuseButtonClick());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAccuseButtonClick()
    {
        GameManager.meetingScene = true;
        SceneManager.LoadScene("GatheredScene");
        GameManager.EnablePlayer();
    }
}
