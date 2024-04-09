using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, IDataPersistence
{
    public Slider slider;

    public void SetProgress(int progress)
    {
        progress = GameManager.foundEvidence;
        slider.value = progress;

    }

    void Update()
    {
        SetProgress(GameManager.foundEvidence);
       
    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
            gameData.progressSliderValue = slider.value;
        }
    }

    // Load the state of the progress bar from game data
    public void LoadData(GameData gameData)
    {
        if (gameData != null)
        {
            slider.value = gameData.progressSliderValue;
        }
    }

}
