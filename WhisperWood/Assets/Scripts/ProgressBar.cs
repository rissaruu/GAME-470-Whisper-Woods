using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public GameManager gameManager;

    public void SetProgress(int progress)
    {
        progress = gameManager.foundEvidence;
        slider.value = progress;
    }

}
