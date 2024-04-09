using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("SceneDaniel",LoadSceneMode.Additive); //This will put two scene into one load.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
