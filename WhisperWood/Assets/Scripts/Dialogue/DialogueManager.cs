using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class DialogueManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;

    [SerializeField] private float typingSpeed = 0.02f;
    [SerializeField] private float turnSpeed = 2;

    private List<DialogueString> dialogueList;

    [Header("Player")]
    public GameObject playerBody;
    public GameObject centerPoint;

    private int currentDialogueIndex = 0;

    public GameObject characterCard;
    public TMP_Text nameTextBox;
    public GameObject nameBox;

    public ItemIndex ItemIndex;
    public DrinkingPuzzle DrinkingPuzzle;
    public D20Roll D20Roll;
    public CardPuzzle CardPuzzle;

    //IMAGES
    [SerializeField] private Sprite CoralImage;
    [SerializeField] private Sprite BeetlemastImage;
    [SerializeField] private Sprite ElmoreImage;
    [SerializeField] private Sprite DroranImage;

    //TOM
    [SerializeField] private GameObject TomTrigger1;
    [SerializeField] private GameObject TomTrigger2;
    [SerializeField] private GameObject TomTrigger3;
    [SerializeField] private GameObject TomTrigger4;
    private bool shouldTomDialogue2;
    private bool shouldTomDialogue3;
    private bool shouldTomDialogue4;
    public TomMovement tomMovement;

    //BEETLEMAST
    [SerializeField] private GameObject BeetlemastTrigger1;
    [SerializeField] private GameObject BeetlemastTrigger2;
    [SerializeField] private GameObject BeetlemastTrigger3;
    [SerializeField] private GameObject BeetlemastTrigger4;
    private bool shouldBeetlemastDialogue2;
    private bool shouldBeetlemastDialogue3;
    private bool shouldBeetlemastDialogue4;

    //ELMORE
    [SerializeField] private GameObject ElmoreTrigger1;
    public GameObject ElmoreStartPuzzleTrigger;
    public GameObject ElmoreBadEndTrigger;
    public GameObject ElmoreGoodEndTrigger;
    [SerializeField] private GameObject ElmoreRandomizedTrigger1;
    [SerializeField] private GameObject ElmoreRandomizedTrigger2;
    private bool shouldRandomBeginnerElmoreDialogue;
    private bool shouldRandomEndElmoreDialogue;
    private bool shouldElmorePuzzleBegin;
    private bool shouldElmoreBadPuzzleEnding;
    private bool shouldElmoreGoodPuzzleEnding;

    //CORAL
    [SerializeField] private GameObject CoralTrigger1;
    public GameObject CoralTrigger2;
    [SerializeField] private GameObject CoralRandomizedTrigger1;
    [SerializeField] private GameObject CoralRandomizedTrigger2;
    private bool shouldCoralDialogue2;
    private bool shouldRandomBeginnerCoralDialogue;
    private bool shouldRandomEndCoralDialogue;

    private bool notStartedDialogue;

    //DRORAN
    [SerializeField] private GameObject DroranTrigger1;
    [SerializeField] private GameObject DroranTrigger2;
    [SerializeField] private GameObject DroranRandomizedTrigger;
    [SerializeField] private GameObject DroranEndTrigger;
    private bool shouldRandomDroranDialogue;
    private bool shouldDroranDialogue2;
    private bool shouldDroranEndDialogue;

    public Map Map;

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
        BeetlemastTrigger3.SetActive(false);
        BeetlemastTrigger4.SetActive(false);
        DroranTrigger2.SetActive(false);
        DroranRandomizedTrigger.SetActive(false);
        DroranEndTrigger.SetActive(false);
        CoralTrigger2.SetActive(false);
        CoralRandomizedTrigger1.SetActive(false);
        CoralRandomizedTrigger2.SetActive(false);
        ElmoreRandomizedTrigger1.SetActive(false);
        ElmoreRandomizedTrigger2.SetActive(false);
        ElmoreStartPuzzleTrigger.SetActive(false);
        ElmoreTrigger1.SetActive(true);
        ElmoreGoodEndTrigger.SetActive(false);
        ElmoreBadEndTrigger.SetActive(false);
        notStartedDialogue = true;

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
            if (character.name == "BeetlemastDialogueTrigger2")
            {
                //not sure anything is needed here
                //shouldBeetlemastDialogue2 = true;
            }
            
        }

        if (character.CompareTag("Tom"))
        {
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Tim";
            if (character.name == "TomDialogueTrigger1")
            {
                character.SetActive(false);
                shouldTomDialogue2 = true;
                shouldBeetlemastDialogue3 = true;
                shouldBeetlemastDialogue2 = false;
                BeetlemastTrigger1.SetActive(false);
                BeetlemastTrigger2.SetActive(false);
            }
            if (character.name == "TomDialogueTrigger2")
            {
                character.SetActive(false);
                shouldTomDialogue3 = true;
            }
            if (character.name == "TomDialogueTrigger3")
            {
                character.SetActive(false);
                //shouldTomDialogue4 = true;
            }
        }
        if (character.CompareTag("Droran"))
        {
            characterCard.SetActive(true);
            characterCard.GetComponent<Image>().sprite = DroranImage;
           nameTextBox.GetComponent<TextMeshProUGUI>().text = "Droran";
            if (character.name == "DroranDialogueTrigger1")
            {
                character.SetActive(false);
                shouldRandomDroranDialogue = true;

            }
            if (character.name == "DroranRandomizedDialogueTrigger")
            {
                currentDialogueIndex = Random.Range(0, 3);
            }
            if (character.name == "DroranDialogueTrigger2") //puzzle
            {
                shouldElmorePuzzleBegin = true;
                shouldRandomBeginnerElmoreDialogue = false;
                ElmoreRandomizedTrigger1.SetActive(false);
                Map.DeletePreviousHighlights();
                Map.diningRoomHighlight.SetActive(true);
                Map.hintText.text = "Find more information.";
            }
        }

        if (character.CompareTag("Elmore"))
        {
            characterCard.SetActive(true);
            characterCard.GetComponent<Image>().sprite = ElmoreImage;
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Elmore";
            if (character.name == "ElmoreDialogueTrigger1")
            {
                character.SetActive(false);
                shouldRandomBeginnerElmoreDialogue = true;
            }
            if (character.name == "ElmoreDialoguePuzzleStartTrigger")
            {
                character.SetActive(false);
                if (!DrinkingPuzzle.notRiggedAnymore)
                {
                    shouldElmoreBadPuzzleEnding = true;
                    Map.DeletePreviousHighlights();
                    Map.employeeLoungeHighlight.SetActive(true);
                    Map.hintText.text = "There must be some sort of magical scroll to help you defeat Elmore.";
                }
                else
                {
                    shouldElmoreBadPuzzleEnding = false;
                    shouldElmoreGoodPuzzleEnding = true;
                }
                shouldDroranEndDialogue = true;
                shouldDroranDialogue2 = false;
                
            }
            if (character.name == "ElmoreDialoguePuzzleBadEndTrigger")
            {
                character.SetActive(false);
                shouldElmorePuzzleBegin = true;
                
            }
            if (character.name == "ElmoreDialoguePuzzleGoodEndTrigger")
            {
                character.SetActive(false);
                shouldRandomEndElmoreDialogue = true;
                shouldRandomBeginnerCoralDialogue = false;
                shouldCoralDialogue2 = true;
                Map.DeletePreviousHighlights();
                Map.unnamedRoomHighlight.SetActive(true);
                Map.hintText.text = "You must find the combination for the keypad.";
            }
            if (character.name == "ElmoreRandomizedDialogueTrigger1")
            {
                currentDialogueIndex = Random.Range(0, 2);
                //character.SetActive(false);
            }
            if (character.name == "ElmoreRandomizedDialogueTrigger2")
            {
                currentDialogueIndex = Random.Range(0, 2);
            }
        }

        if (character.CompareTag("Coral"))
        {
            characterCard.SetActive(true);
            characterCard.GetComponent<Image>().sprite = CoralImage;
            nameTextBox.GetComponent<TextMeshProUGUI>().text = "Coral";
            if (character.name == "CoralDialogueTrigger1") 
            {
                character.SetActive(false);
                //shouldCoralDialogue2 = true;
                shouldRandomBeginnerCoralDialogue = true;
            }
            if (character.name == "CoralDialogueTrigger2") 
            {
                //Debug.Log("setting inactive");
                character.SetActive(false);
                
            }
            if (character.name == "CoralRandomizedDialogueTrigger1")
            {
                currentDialogueIndex = Random.Range(0, 3);
                //character.SetActive(false);
                //shouldRandomEndCoralDialogue = true;
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

    private void Update()
    {
        //Gonna be used for checks that need to be constant
        if (ItemIndex.inventoryItems.ContainsKey("Combination")) //Beetlemast puzzle is over
        {
            BeetlemastTrigger1.SetActive(false);
            BeetlemastTrigger2.SetActive(false);
            BeetlemastTrigger3.SetActive(false);
            shouldBeetlemastDialogue2 = false;
            shouldBeetlemastDialogue3 = false;
            shouldBeetlemastDialogue4 = true;
            BeetlemastTrigger4.SetActive(true);

            if (TomTrigger1.activeInHierarchy)
            {
                TomTrigger1.SetActive(false);
                TomTrigger2.SetActive(true);
                TomTrigger3.SetActive(false);
                shouldTomDialogue3 = false;
            }
 

        }

        if (ItemIndex.inventoryItems.ContainsKey("DroranAd"))
        {
            if (DroranTrigger1.activeInHierarchy || DroranRandomizedTrigger.activeInHierarchy)
            {
                shouldDroranDialogue2 = true;
                shouldRandomDroranDialogue = false;
                DroranTrigger2.SetActive(true);
                DroranRandomizedTrigger.SetActive(false);
                DroranTrigger1.SetActive(false);
            }

        }

        if (ItemIndex.inventoryItems.ContainsKey("PlayingCard"))
        {
            shouldCoralDialogue2 = false;
            shouldRandomEndCoralDialogue = true;
            CoralTrigger2.SetActive(false);
            CoralRandomizedTrigger2.SetActive(true);
        }
    }

    public void HandleElmoreDrinkingDialogue(bool isFailure)
    {
        if (isFailure)
        {

            //shouldRandomBeginnerElmoreDialogue = false;
            //StartCoroutine(PrintDialogue());
            StartCoroutine(WaitForBadRoll());
            
        }
        else
        {
            StartCoroutine(WaitForGoodRoll());
        }
    }

    IEnumerator WaitForBadRoll()
    {
        yield return new WaitForSeconds(3f);

        ElmoreBadEndTrigger.GetComponent<DialogueTrigger>().startDialogue = true;
        
    }

    IEnumerator WaitForGoodRoll()
    {
        yield return new WaitForSeconds(3f);

        ElmoreGoodEndTrigger.GetComponent<DialogueTrigger>().startDialogue = true;
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
            //shouldTomDialogue3 = false;
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



        if (shouldBeetlemastDialogue3)
        {
            BeetlemastTrigger3.SetActive(true);
            //shouldBeetlemastDialogue3 = false;
        }

        if (shouldBeetlemastDialogue4)
        {
            BeetlemastTrigger4.SetActive(true);
        }

        if (shouldRandomDroranDialogue)
        {
            DroranRandomizedTrigger.SetActive(true);
            DroranTrigger1.SetActive(false);

        }
        if (shouldDroranDialogue2)
        {
            DroranTrigger1.SetActive(false);
            DroranRandomizedTrigger.SetActive(false);
            DroranTrigger2.SetActive(true);
        }
        if (shouldDroranEndDialogue)
        {
            DroranTrigger2.SetActive(false);
            DroranEndTrigger.SetActive(true);
        }

        if (shouldRandomBeginnerElmoreDialogue)
        {
            ElmoreRandomizedTrigger1.SetActive(true);
            
        }

        if (shouldRandomEndElmoreDialogue)
        {
            ElmoreRandomizedTrigger2.SetActive(true);
        }

        if (shouldElmorePuzzleBegin)
        {
            ElmoreTrigger1.SetActive(false);
            ElmoreStartPuzzleTrigger.SetActive(true);
            shouldElmorePuzzleBegin = false;
        }

        if (shouldElmoreBadPuzzleEnding)
        {
            ElmoreStartPuzzleTrigger.SetActive(false);
            ElmoreBadEndTrigger.SetActive(true);

            shouldElmoreBadPuzzleEnding = false;
        }

        if (shouldElmoreGoodPuzzleEnding)
        {
            ElmoreStartPuzzleTrigger.SetActive(false);
            ElmoreBadEndTrigger.SetActive(false);
            ElmoreGoodEndTrigger.SetActive(true);
            CoralTrigger1.SetActive(false);
            CoralRandomizedTrigger1.SetActive(false);

            shouldElmoreGoodPuzzleEnding = false;
        }

        if (shouldCoralDialogue2) 
        {
            
            CoralTrigger2.SetActive(true);
            CoralRandomizedTrigger1.SetActive(false);
            //shouldCoralDialogue2 = false;
        }

        if (shouldRandomBeginnerCoralDialogue)
        {
            CoralRandomizedTrigger1.SetActive(true);
            //shouldRandomBeginnerCoralDialogue = false;   
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

                option1Button.gameObject.SetActive(true);
                
                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));


                if (!line.oneOption) //checking that there isn't only one option
                {
                    option2Button.gameObject.SetActive(true);
                    option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;
                    option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));
                }
                optionSelected = false;

                yield return new WaitUntil(() => optionSelected);

            }
            if(!line.isQuestion)
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
        if (option1Button.GetComponentInChildren<TMP_Text>().text == "Drink the Dragon's Ember Elixir")
        {
            DialogueStop();
            //drinking game call
            DrinkingPuzzle.DrinkingGameBegin();
        }

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

        GameManager.EnablePlayer();
        centerPoint.SetActive(true);
        characterCard.SetActive(false);
        nameTextBox.enabled = false;
        nameBox.SetActive(false);

        if (TomTrigger2.activeInHierarchy)
        {
            tomMovement.MoveToDestination();
        }
    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
          

            // Save dialogue triggers
            gameData.dialogueTriggers = new bool[]
            {
                shouldTomDialogue2, shouldTomDialogue3, shouldTomDialogue4,
                shouldBeetlemastDialogue2, shouldBeetlemastDialogue3, shouldBeetlemastDialogue4,
                shouldRandomDroranDialogue, shouldDroranDialogue2, shouldDroranEndDialogue,
                shouldRandomBeginnerElmoreDialogue, shouldRandomEndElmoreDialogue, shouldElmorePuzzleBegin,
                shouldElmoreBadPuzzleEnding, shouldElmoreGoodPuzzleEnding,
                shouldCoralDialogue2, shouldRandomBeginnerCoralDialogue, shouldRandomEndCoralDialogue
            };
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData != null)
        {


            // Load dialogue triggers
            if (gameData.dialogueTriggers != null && gameData.dialogueTriggers.Length == 18) // Adjust the length based on the number of dialogue triggers
            {
                shouldTomDialogue2 = gameData.dialogueTriggers[0];
                shouldTomDialogue3 = gameData.dialogueTriggers[1];
                shouldTomDialogue4 = gameData.dialogueTriggers[2];
                shouldBeetlemastDialogue2 = gameData.dialogueTriggers[3];
                shouldBeetlemastDialogue3 = gameData.dialogueTriggers[4];
                shouldBeetlemastDialogue4 = gameData.dialogueTriggers[5];
                shouldRandomDroranDialogue = gameData.dialogueTriggers[6];
                shouldDroranDialogue2 = gameData.dialogueTriggers[7];
                shouldDroranEndDialogue = gameData.dialogueTriggers[8];
                shouldRandomBeginnerElmoreDialogue = gameData.dialogueTriggers[9];
                shouldRandomEndElmoreDialogue = gameData.dialogueTriggers[10];
                shouldElmorePuzzleBegin = gameData.dialogueTriggers[11];
                shouldElmoreBadPuzzleEnding = gameData.dialogueTriggers[12];
                shouldElmoreGoodPuzzleEnding = gameData.dialogueTriggers[13];
                shouldCoralDialogue2 = gameData.dialogueTriggers[14];
                shouldRandomBeginnerCoralDialogue = gameData.dialogueTriggers[15];
                shouldRandomEndCoralDialogue = gameData.dialogueTriggers[16];
            }
        }
    }



}
