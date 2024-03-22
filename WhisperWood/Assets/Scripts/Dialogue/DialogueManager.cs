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

    //IMAGES
    [SerializeField] private Sprite CoralImage;
    [SerializeField] private Sprite BeetlemastImage;

    //TOM
    [SerializeField] private GameObject TomTrigger2;
    [SerializeField] private GameObject TomTrigger3;
    [SerializeField] private GameObject TomTrigger4;
    private bool shouldTomDialogue2;
    private bool shouldTomDialogue3;
    private bool shouldTomDialogue4;

    //BEETLEMAST
    [SerializeField] private GameObject BeetlemastTrigger2;
    private bool shouldBeetlemastDialogue2;

    //ELMORE
    public GameObject ElmoreTrigger2;

    //CORAL
    [SerializeField] private GameObject CoralTrigger2;
    [SerializeField] private GameObject CoralRandomizedTrigger1;
    [SerializeField] private GameObject CoralRandomizedTrigger2;
    private bool shouldCoralDialogue2;
    private bool shouldRandomBeginnerCoralDialogue;
    private bool shouldRandomEndCoralDialogue;

    private void Start()
    {
        dialogueParent.SetActive(false);
        //playerCamera = Camera.main.transform;
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
        characterCard.SetActive(false);
        nameTextBox.enabled = false;
        nameBox.SetActive(false);
        TomTrigger2.SetActive(false);
        TomTrigger3.SetActive(false);
        TomTrigger4.SetActive(false);
        BeetlemastTrigger2.SetActive(false);
        CoralTrigger2.SetActive(false);
        CoralRandomizedTrigger1.SetActive(false);
        CoralRandomizedTrigger2.SetActive(false);
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
                shouldBeetlemastDialogue2 = true;
            }

            
        }

        if (character.CompareTag("Tom"))
        {
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Tim";
            if (character.name == "TomDialogueTrigger1")
            {
                character.SetActive(false);
                shouldTomDialogue2 = true;
            }
            if (character.name == "TomDialogueTrigger2")
            {
                character.SetActive(false);
                shouldTomDialogue3 = true;
            }
            if (character.name == "TomDialogueTrigger3")
            {
                character.SetActive(false);
                shouldTomDialogue4 = true;
            }
        }

        if (character.CompareTag("Coral"))
        {
            characterCard.SetActive(true);
            characterCard.GetComponent<Image>().sprite = CoralImage;
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Coral";
            if (character.name == "CoralDialogueTrigger1") //this will have to be changed later to if the player has spoken to elmor or not
            {
                character.SetActive(false);
                shouldCoralDialogue2 = true;
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
                character.SetActive(false);
                shouldRandomEndCoralDialogue = true;
            }
            if (character.name == "CoralRandomizedDialogueTrigger2")
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
        if (shouldTomDialogue2)
        {
            TomTrigger2.SetActive(true);
            shouldTomDialogue2 = false;
        }

        if (shouldTomDialogue3)
        {
            TomTrigger3.SetActive(true);
            shouldTomDialogue3 = false;
        }

        if (shouldTomDialogue4)
        {
            TomTrigger4.SetActive(true);
            //shouldTomDialogue4 = false;
        }

        if (shouldBeetlemastDialogue2)
        {
            BeetlemastTrigger2.SetActive(true);
        }

        if (shouldCoralDialogue2) 
        {
            CoralTrigger2.SetActive(true);
            shouldCoralDialogue2 = false;
        }

        if (shouldRandomBeginnerCoralDialogue)
        {
            CoralRandomizedTrigger1.SetActive(true);
            shouldRandomBeginnerCoralDialogue = false;   
        }

        if (shouldRandomEndCoralDialogue)
        {
            CoralRandomizedTrigger2.SetActive(true);
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
