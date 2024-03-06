using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CombinationLock : MonoBehaviour
{
    

    [SerializeField] private TMP_Text number1;
    [SerializeField] private TMP_Text number2;
    [SerializeField] private TMP_Text number3;

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

    private bool solvedCombination;

    [SerializeField] private GameObject luggageTopPiece;


    private void Start()
    {
        correctCombination = new List<int>();
        inputList = new List<string>();
        comparisonList = new List<int>();

        correctCombination.Add(1);
        correctCombination.Add(2);
        correctCombination.Add(4);

        LockUI.SetActive(false);
        solvedCombination = false;

        number1Up.onClick.AddListener(() => ChangeNumber(number1, true));
        number1Down.onClick.AddListener(() => ChangeNumber(number1, false));
        number2Up.onClick.AddListener(() => ChangeNumber(number2, true));
        number2Down.onClick.AddListener(() => ChangeNumber(number2, false));
        number3Up.onClick.AddListener(() => ChangeNumber(number3, true));
        number3Down.onClick.AddListener(() => ChangeNumber(number3, false));
    }

    public void OpenLuggageUI()
    {
        Debug.Log("In other script");
        if (!solvedCombination)
        {
            LockUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            GameManager.canPlayer.walk = false;
            GameManager.canPlayer.rotate = false;
            GameManager.canPlayer.jump = false;
        }

    }

    public void CloseLuggageUI()
    {
        LockUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.canPlayer.walk = true;
        GameManager.canPlayer.rotate = true;
        GameManager.canPlayer.jump = true;
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

        for (int i = 0; i < inputList.Count; i++)
        {
            comparisonList.Add(int.Parse(inputList[i]));
        }

        if (isCombinationCorrect())
        {
            Debug.Log("YOU SOLVED IT");
            solvedCombination = true;

            Quaternion target = new Quaternion(luggageTopPiece.transform.rotation.x, luggageTopPiece.transform.rotation.y, luggageTopPiece.transform.rotation.z - 122f, 0f);
            float smooth = 5.0f;
            luggageTopPiece.transform.rotation = Quaternion.Slerp(luggageTopPiece.transform.rotation, target, Time.deltaTime * smooth);

            CloseLuggageUI();
  
        }
    }

    private bool isCombinationCorrect()
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
