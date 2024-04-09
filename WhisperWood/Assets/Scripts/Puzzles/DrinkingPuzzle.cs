using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrinkingPuzzle : MonoBehaviour
{
    [SerializeField] private Button rollForToleranceButton;
    [SerializeField] private GameObject drinkingUI;
    [SerializeField] private GameObject rollNotHighEnoughText;
    [SerializeField] private GameObject winTextMessage;
    [SerializeField] private GameObject explainText;
    public DialogueManager DialogueManager;
    public D20Roll D20Roll;
    public Interactable Interactable;

    public bool notRiggedAnymore;
    private bool canExit;


    // Start is called before the first frame update
    void Start()
    {
        rollForToleranceButton.gameObject.SetActive(false);
        drinkingUI.SetActive(false);
        rollNotHighEnoughText.SetActive(false);
        winTextMessage.SetActive(false);
        explainText.SetActive(false);
        //rollNotHighEnoughText.enabled = false;
        //failureTextMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K) && rollForToleranceButton.gameObject.activeInHierarchy == true)
        {
            //rollForToleranceButton.gameObject.SetActive(false);
            drinkingUI.SetActive(false);
            GameManager.EnablePlayer();
            canExit = false;
        }
        //if (canExit && DialogueManager.ElmoreTrigger2.activeInHierarchy && Input.GetKeyDown(KeyCode.K)) //something else to check that elmor's dialogue is finished
        //{
        //    //code to start the drinking game
        //    GameManager.DisablePlayer();
        //    rollForToleranceButton.gameObject.SetActive(true);
        //    rollForToleranceButton.onClick.AddListener(() => OnToleranceButtonClick());
        //}
        canExit = true;

        if (Interactable.tryingToUseScroll)
        {
            notRiggedAnymore = true;
        }
    }

    public void DrinkingGameBegin()
    {
        GameManager.DisablePlayer();
        rollForToleranceButton.gameObject.SetActive(true);
        drinkingUI.SetActive(true);
        explainText.SetActive(true);
        rollForToleranceButton.onClick.AddListener(() => OnToleranceButtonClick());



    }

    private void OnToleranceButtonClick()
    {
        explainText.SetActive(false);
        if (notRiggedAnymore == false) //will not be this, something else later
        {
            D20Roll.RollD20(true);
            if (D20Roll.result < 20)
            {
                //code that will start elmore's failure dialogue
                rollForToleranceButton.gameObject.SetActive(false);
                StartCoroutine(WaitForTextToShow(rollNotHighEnoughText));
                //StartCoroutine(ResetText(rollNotHighEnoughText));
                drinkingUI.SetActive(false);
                DialogueManager.HandleElmoreDrinkingDialogue(true); 


            }
        }
        else
        {
            D20Roll.RollD20(false);
            StartCoroutine(WaitForTextToShow(winTextMessage));
            
            rollForToleranceButton.gameObject.SetActive(false);
            drinkingUI.SetActive(false);
            DialogueManager.HandleElmoreDrinkingDialogue(false);

        }

        
    }

    IEnumerator WaitForTextToShow(GameObject textToShow)
    {
        yield return new WaitForSeconds(1.5f);
        textToShow.SetActive(true);
        StartCoroutine(ResetText(textToShow));
    }

    IEnumerator ResetText(GameObject textToReset)
    {
        yield return new WaitForSeconds(.5f);
        textToReset.SetActive(false);
    }
}
