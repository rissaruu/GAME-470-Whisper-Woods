using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPuzzle : MonoBehaviour
{
    [SerializeField] private GameObject aceCard;


    public DialogueManager DialogueManager;

    

    private void Start()
    {
        aceCard.layer = LayerMask.NameToLayer("Default");

    }

    //Code logic that I need: 
    //In order for the ace card to become interactable, the player must have sat through Coral's dialogue (this makes it unskippable), will check if coral dialogue trigger 2 is active
    private void Update()
    {
        if (DialogueManager.CoralTrigger2.activeInHierarchy)
        {
            aceCard.layer = LayerMask.NameToLayer("Interactable");
            aceCard.tag = "PlayingCard";
        }
    }
}
