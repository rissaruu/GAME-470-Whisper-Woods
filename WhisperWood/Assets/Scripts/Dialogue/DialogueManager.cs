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

    public GameObject characterCard;
    public TMP_Text nameTextBox;
    public GameObject nameBox;

    [SerializeField] private Sprite CoralImage;
    [SerializeField] private Sprite BeetlemastImage;

    //Bools for checking pace of dialogue
    private bool IsBeetlemastBeginnerDialogueComplete;
    private bool IsCoralBeginnerDialogueComplete;

    private bool shouldRandomBeginnerCoralDialogue;

    public GameObject BeetlemastTrigger2;
    public GameObject CoralTrigger2;

    public GameObject CoralRandomizedTrigger1;


    private void Start()
    {
        dialogueParent.SetActive(false);
        //playerCamera = Camera.main.transform;
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        characterCard.SetActive(false);
        nameTextBox.enabled = false;
        nameBox.SetActive(false);
        BeetlemastTrigger2.SetActive(false);
        CoralTrigger2.SetActive(false);
        CoralRandomizedTrigger1.SetActive(false);
    }

    public void DialogueStart(List<DialogueString> textToPrint, Transform NPC, GameObject character)
    {
        dialogueParent.SetActive(true);
        currentDialogueIndex = 0;
        GameManager.DisablePlayer();

        nameTextBox.enabled = true;
        nameBox.SetActive(true);

        if (character.CompareTag("Beetlemast"))
        {
            characterCard.SetActive(true);
            characterCard.GetComponent<Image>().sprite = BeetlemastImage;
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Beetlemast";
            if (character.name == "BeetleDialogueTrigger1")
            {
                character.SetActive(false);
                IsBeetlemastBeginnerDialogueComplete = true;
            }

            
        }

        if (character.CompareTag("Tom"))
        {
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Tim";
        }

        if (character.CompareTag("Coral"))
        {
            characterCard.SetActive(true);
            characterCard.GetComponent<Image>().sprite = CoralImage;
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Coral";
            if (character.name == "CoralDialogueTrigger1") //this will have to be changed later to if the player has spoken to elmor or not
            {
                character.SetActive(false);
                IsCoralBeginnerDialogueComplete = true;
            }
            if (character.name == "CoralDialogueTrigger2") //this logic is not in correct order!!
            {
                Debug.Log("setting inactive");
                character.SetActive(false);
                shouldRandomBeginnerCoralDialogue = true;
            }
            if (character.name == "CoralRandomizedDialogueTrigger1")
            {
                currentDialogueIndex = Random.Range(0, 3);
            }
        }

        //StartCoroutine(TurnCameraTowardsNPC(NPC));

        dialogueList = textToPrint;
        CheckDialogueConditions();
        DisableButtons();
        centerPoint.SetActive(false);

        StartCoroutine(PrintDialogue());
    }

    private void CheckDialogueConditions()
    {
        if (IsBeetlemastBeginnerDialogueComplete)
        {
            BeetlemastTrigger2.SetActive(true);
        }

        if (IsCoralBeginnerDialogueComplete) //may be called something else instead of coral beginner dialogue complete (may be called isCoralPuzzleTime)
        {
            CoralTrigger2.SetActive(true);
            IsCoralBeginnerDialogueComplete = false;
        }

        if (shouldRandomBeginnerCoralDialogue)
        {
            CoralRandomizedTrigger1.SetActive(true);
               
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
        Debug.Log("Index: " + currentDialogueIndex);
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

        ////enable player here
        //GameManager.canPlayer.walk = true;
        //GameManager.canPlayer.rotate = true;
        //GameManager.canPlayer.jump = true;
        //GameManager.canPause = true;
        //GameManager.canCamera = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        GameManager.EnablePlayer();
        centerPoint.SetActive(true);
        characterCard.SetActive(false);
        nameTextBox.enabled = false;
        nameBox.SetActive(false);

    }
}
