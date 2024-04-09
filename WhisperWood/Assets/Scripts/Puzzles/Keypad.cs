using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Keypad : MonoBehaviour
{
    string code = "4816";
    string num = null;
    int numIndex = 0;
    string alpha;
    public TMP_Text UiText = null;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Button button5;
    public Button button6;
    public Button button7;
    public Button button8;
    public Button button9;
    public Button button0;

    public Button enterButton;

    public Button clearButton;

    public bool codeSolved;

    private void Start()
    {
        button1.onClick.AddListener(() => TypeCode("1"));
        button2.onClick.AddListener(() => TypeCode("2"));
        button3.onClick.AddListener(() => TypeCode("3"));
        button4.onClick.AddListener(() => TypeCode("4"));
        button5.onClick.AddListener(() => TypeCode("5"));
        button6.onClick.AddListener(() => TypeCode("6"));
        button7.onClick.AddListener(() => TypeCode("7"));
        button8.onClick.AddListener(() => TypeCode("8"));
        button9.onClick.AddListener(() => TypeCode("9"));
        button0.onClick.AddListener(() => TypeCode("0"));

        enterButton.onClick.AddListener(() => OnEnterClick());
        clearButton.onClick.AddListener(() => OnClearClick());
    }

    public void TypeCode(string numbers)
    {
        if (numIndex <= 3)
        {
            numIndex++;
            num = num + numbers;
            UiText.text = num;
        }

    }

    public void OnEnterClick()
    {
        if(num == code)
        {
            Debug.Log("It's working");
            codeSolved = true;
        }
    }

    public void OnClearClick()
    {
        numIndex = 0;
        num = null;
        UiText.text = num;
    }
}
