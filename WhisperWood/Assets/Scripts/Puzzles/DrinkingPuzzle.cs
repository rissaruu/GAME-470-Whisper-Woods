using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DrinkingPuzzle : MonoBehaviour
{
    [SerializeField] private Button rollForToleranceButton;
    [SerializeField] private Text rollNotHighEnoughText;
    [SerializeField] private Text failureTextMessage;
    public DialogueManager DialogueManager;
    public D20Roll D20Roll;
    public Interactable Interactable;

    private bool notRiggedAnymore;
    private bool canExit;


    // Start is called before the first frame update
    void Start()
    {
        rollForToleranceButton.gameObject.SetActive(false);
        //rollNotHighEnoughText.enabled = false;
        //failureTextMessage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.K) && rollForToleranceButton.gameObject.activeInHierarchy == true)
        {
            rollForToleranceButton.gameObject.SetActive(false);
            GameManager.EnablePlayer();
            canExit = false;
        }
        if (canExit && DialogueManager.ElmoreTrigger2.activeInHierarchy && Input.GetKeyDown(KeyCode.K)) //something else to check that elmor's dialogue is finished
        {
            //code to start the drinking game
            GameManager.DisablePlayer();
            rollForToleranceButton.gameObject.SetActive(true);
            rollForToleranceButton.onClick.AddListener(() => OnToleranceButtonClick());
        }
        canExit = true;

        if (Interactable.tryingToUseScroll)
        {
            notRiggedAnymore = true;
        }
    }

    private void OnToleranceButtonClick()
    {
        if (notRiggedAnymore == false) //will not be this, something else later
        {
            D20Roll.RollD20(true);
        }
        else
        {
            D20Roll.RollD20(false);
        }

        
    }
}
