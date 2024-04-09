using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public List<DialogueString> dialogueStrings = new List<DialogueString>();
    [SerializeField] private Transform NPCTransform;
    [SerializeField] GameObject player;


    private bool hasSpoken = false;
    private bool canTrigger;
    public bool startDialogue;

    private GameObject collidedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//&& !hasSpoken)
        {
            canTrigger = true;
            collidedObject = other.gameObject;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            collidedObject = null;
        }
    }

    private void Update()
    {
        if (canTrigger && Input.GetKeyDown(KeyCode.E) && !startDialogue)
        {
            collidedObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, NPCTransform, gameObject);
            //hasSpoken = true;
            canTrigger = false;
            

        }

        if (startDialogue)
        {
            player.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, NPCTransform, gameObject);
            startDialogue = false;
        }
    }
}

[System.Serializable]

public class DialogueString
{
    public string npcDialogue; //Represent the text that the npc says
    public bool isEnd; //Represent if the line is the final line for the conversation

    [Header("Branch")]
    public bool isQuestion;
    public bool branchBackToMain;
    public bool oneOption;
    public string answerOption1;
    public string answerOption2;
    public int option1IndexJump;
    public int option2IndexJump;
    public int option3IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;


}
