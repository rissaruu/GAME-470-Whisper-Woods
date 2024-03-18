using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetProgress(int progress)
    {
        progress = GameManager.foundEvidence;
        slider.value = progress;
    }

}
