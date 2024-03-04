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


    private void Start()
    {
        dialogueParent.SetActive(false);
        //playerCamera = Camera.main.transform;
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
    }

    public void DialogueStart(List<DialogueString> textToPrint, Transform NPC)
    {
        dialogueParent.SetActive(true);
        GameManager.canPlayer.walk = false;
        GameManager.canPlayer.rotate = false;
        GameManager.canPlayer.jump = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //StartCoroutine(TurnCameraTowardsNPC(NPC));

        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();
        centerPoint.SetActive(false);

        StartCoroutine(PrintDialogue());
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
                yield return StartCoroutine(TypeText(line.text));

                //option1Button.interactable = true;
                //option2Button.interactable = true;

                option1Button.gameObject.SetActive(true);
                option2Button.gameObject.SetActive(true);

                option1Button.GetComponentInChildren<TMP_Text>().text = line.answerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.answerOption2;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.option1IndexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.option2IndexJump));

                yield return new WaitUntil(() => optionSelected);

            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
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

    }
}
