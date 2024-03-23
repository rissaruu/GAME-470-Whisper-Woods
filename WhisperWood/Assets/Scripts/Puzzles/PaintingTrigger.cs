using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingTrigger : MonoBehaviour
{
    private bool canTrigger;
    public Interactable Interactable;
    public GameObject incompletePicture;
    public GameObject completedPicture;

    private void Start()
    {
        completedPicture.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
        }
    }

    private void Update()
    {
        if (canTrigger && Interactable.tryingToUsePaintingPiece) //needs another condition (the use button)
        {
            //Code for swapping paintings
            incompletePicture.SetActive(false);
            completedPicture.SetActive(true);
        }
        if (!canTrigger && Interactable.tryingToUsePaintingPiece)
        {
            Interactable.tryingToUsePaintingPiece = false;
            //this isn't the issue
        }
    }
}
