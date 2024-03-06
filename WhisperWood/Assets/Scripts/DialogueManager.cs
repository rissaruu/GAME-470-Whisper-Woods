using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float turnSpeed = 2;

    private List<DialogueString> dialogueList;

    [Header("Player")]
    public GameObject playerBody;
    public GameObject centerPoint;

    private int currentDialogueIndex = 0;

    public Image characterCard;
    public TMP_Text nameTextBox;
    public GameObject nameBox;

    //Bools for checking pace of dialogue
    private bool IsBeginnerDialogueComplete;

    public GameObject BeetlemastTrigger2;


    private void Start()
    {
        dialogueParent.SetActive(false);
        //playerCamera = Camera.main.transform;
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        characterCard.enabled = false;
        nameTextBox.enabled = false;
        nameBox.SetActive(false);
        BeetlemastTrigger2.SetActive(false);
    }

    public void DialogueStart(List<DialogueString> textToPrint, Transform NPC, GameObject character)
    {
        dialogueParent.SetActive(true);
        GameManager.canPlayer.walk = false;
        GameManager.canPlayer.rotate = false;
        GameManager.canPlayer.jump = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        nameTextBox.enabled = true;
        nameBox.SetActive(true);

        if (character.CompareTag("Beetlemast"))
        {
            characterCard.enabled = true;
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Beetlemast";
            if (character.name == "BeetleDialogueTrigger1")
            {
                character.SetActive(false);
                IsBeginnerDialogueComplete = true;
            }

            
        }

        if (character.CompareTag("Tom"))
        {
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Tim";
        }

        //StartCoroutine(TurnCameraTowardsNPC(NPC));

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        CheckDialogueConditions();
        DisableButtons();
        centerPoint.SetActive(false);

        StartCoroutine(PrintDialogue());
    }

    private void CheckDialogueConditions()
    {
        if (IsBeginnerDialogueComplete)
        {
            BeetlemastTrigger2.SetActive(true);
        }
    }

    private void DisableButtons()
    {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);

    }

    //private IEnumerator TurnCameraTowardsNPC(Transform NPC)
    //{
    //    //Quaternion startRotation = playerCamera.rotation;
    //    //Quaternion targetRotation = Quaternion.LookRotation(NPC.position - playerCamera.position);

    //    //float elapsedTime = 0;
    //    //while(elapsedTime < 1f)
    //    //{
    //    //    playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
    //    //    elapsedTime += Time.deltaTime * turnSpeed;
    //    //    yield return null;
    //    //}

    //    //playerCamera.rotation = targetRotation;
    //}

    private bool optionSelected = false;

    private IEnumerator PrintDialogue()
    {
        while(currentDialogueIndex < dialogueList.Count)
        {
            DialogueString line = dialogueList[currentDialogueIndex];

            line.startDialogueEvent?.Invoke();


            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.npcDialogue));

                //option1Button.interactable = true;
                //option2Button.interactable = true;



                option1Button.gameObject.SetActive(true);
                
                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));

                if (!line.oneOption) //checking that there isn't only one option
                {
                    option2Button.gameObject.SetActive(true);
                    option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;
                    option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                }
                //else
                //{
                //    RectTransform buttonRectTransform = option1Button.GetComponent<RectTransform>();
                //    Vector3 newPosition = buttonRectTransform.position;
                //    newPosition.x += 113;
                //    buttonRectTransform.position = newPosition;
                //}


                yield return new WaitUntil(() => optionSelected);

            }
            else
            {
                yield return StartCoroutine(TypeText(line.npcDialogue));
            }
            if (line.branchBackToMain)
            {
                currentDialogueIndex = line.option3IndexJump;
            }

            line.endDialogueEvent?.Invoke();

            optionSelected = false;
        }
        DialogueStop();
    }

    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();

        currentDialogueIndex = indexJump;

    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        if (dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }
        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        dialogueParent.SetActive(false);

        //enable player here
        GameManager.canPlayer.walk = true;
        GameManager.canPlayer.rotate = true;
        GameManager.canPlayer.jump = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        centerPoint.SetActive(true);
        characterCard.enabled = false;
        nameTextBox.enabled = false;
        nameBox.SetActive(false);

    }
}
