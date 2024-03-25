using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class CombinationLock : MonoBehaviour
{
    

    [SerializeField] private TMP_Text number1;
    [SerializeField] private TMP_Text number2;
    [SerializeField] private TMP_Text number3;
    [SerializeField] private TMP_Text number4;

    

    private List<int> luggageCombination;
    private List<int> drawerCombination;
    private List<int> correctCombination;
    private List<string> inputList;
    private List<int> comparisonList;

    [SerializeField] private GameObject LockUI;

    [SerializeField] private Button number1Up;
    [SerializeField] private Button number1Down;
    [SerializeField] private Button number2Up;
    [SerializeField] private Button number2Down;
    [SerializeField] private Button number3Up;
    [SerializeField] private Button number3Down;
    [SerializeField] private Button number4Up;
    [SerializeField] private Button number4Down;

    public bool solvedLuggageCombination;
    public bool solvedDrawerCombination;

    [SerializeField] private GameObject luggageTopPiece;
    [SerializeField] private GameObject luggageBottomPiece;
    [SerializeField] private GameObject luggageTrigger;
    [SerializeField] private GameObject duplicateKey;
    [SerializeField] private GameObject movingDrawer;
    [SerializeField] private GameObject drawerTrigger;
    private void Start()
    {
        luggageCombination = new List<int>();
        drawerCombination = new List<int>();
        correctCombination = new List<int>();

        inputList = new List<string>();
        comparisonList = new List<int>();
        duplicateKey.SetActive(false);




        luggageCombination.Add(3);
        luggageCombination.Add(8);
        luggageCombination.Add(2);
        luggageCombination.Add(4);

        //Real code is 7390
        drawerCombination.Add(7);
        drawerCombination.Add(3);
        drawerCombination.Add(9);
        drawerCombination.Add(0);

        LockUI.SetActive(false);
        solvedLuggageCombination = false;

        number1Up.onClick.AddListener(() => ChangeNumber(number1, true));
        number1Down.onClick.AddListener(() => ChangeNumber(number1, false));
        number2Up.onClick.AddListener(() => ChangeNumber(number2, true));
        number2Down.onClick.AddListener(() => ChangeNumber(number2, false));
        number3Up.onClick.AddListener(() => ChangeNumber(number3, true));
        number3Down.onClick.AddListener(() => ChangeNumber(number3, false));
        number4Up.onClick.AddListener(() => ChangeNumber(number4, true));
        number4Down.onClick.AddListener(() => ChangeNumber(number4, false));
    }

    public void OpenLuggageUI()
    {
        if (!solvedLuggageCombination)
        {
            correctCombination = luggageCombination;
            LockUI.SetActive(true);
            GameManager.DisablePlayer();
        }

    }

    public void CloseLuggageUI()
    {
        //Debug.Log("closing");
        LockUI.SetActive(false);
        GameManager.EnablePlayer();
        ResetNums();
    }

    public void OpenDrawerUI()
    {
        if (!solvedDrawerCombination)
        {
            correctCombination = drawerCombination;
            LockUI.SetActive(true);
            GameManager.DisablePlayer();
        }

    }

    public void CloseDrawerUI()
    {
        LockUI.SetActive(false);
        GameManager.EnablePlayer();
        ResetNums();
    }

    private void ChangeNumber(TMP_Text numberText, bool isUp)
    {
        if (isUp)
        {
            if (numberText.text == "9")
            {
                numberText.text = "0";
            }
            else
            {
                int number = int.Parse(numberText.text) + 1;
                numberText.text = number.ToString();
            }

        }
        else
        {
            if (numberText.text == "0")
            {
                numberText.text = "9";
            }
            else
            {
                int number = int.Parse(numberText.text) - 1;
                numberText.text = number.ToString();
            }

        }
        CheckCombination();
    }

    public void CheckCombination()
    {
        inputList.Clear();
        comparisonList.Clear();
        inputList.Add(number1.text);
        inputList.Add(number2.text);
        inputList.Add(number3.text);
        inputList.Add(number4.text);

        for (int i = 0; i < inputList.Count; i++)
        {
            comparisonList.Add(int.Parse(inputList[i]));
        }


        if (isCombinationCorrect(correctCombination))
        {
            Debug.Log("YOU SOLVED IT");

            if (correctCombination == luggageCombination)
            {
                solvedLuggageCombination = true;
                luggageTopPiece.transform.position += Vector3.up * 0.008f;
                luggageTopPiece.transform.eulerAngles += new Vector3(0, 0, -34f);
                duplicateKey.SetActive(true);
                ResetNums();

            }
            if (correctCombination == drawerCombination)
            {
                solvedDrawerCombination = true;
                ResetNums();

                //Method that opens instantly

                Vector3 movement = Vector3.right * 1f;

                movingDrawer.transform.Translate(movement);

            }



            CloseLuggageUI();
  
        }
    }

    private void ResetNums()
    {
        number1.text = "0";
        number2.text = "0";
        number3.text = "0";
        number4.text = "0";

    }

    private bool isCombinationCorrect(List<int> correctCombination)
    {

        for (int i = 0; i < correctCombination.Count; i++)
        {
            if (comparisonList[i] != correctCombination[i])
            {
                return false;
            }
        }

        return true;
    }

}
