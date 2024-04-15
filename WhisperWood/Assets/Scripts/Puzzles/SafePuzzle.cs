using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafePuzzle : MonoBehaviour
{
    private bool canTrigger;

    [SerializeField] private GameObject keypadUI;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private TMP_Text pressEText;
    [SerializeField] private Keypad keypad;
    [SerializeField] private GameObject safe;

    public int totalTime;
    private bool timerSet;
    public bool reachedSafe;

    public Map Map;

    // Start is called before the first frame update
    void Start()
    {
        keypadUI.SetActive(false);
        timer.gameObject.SetActive(false);
        totalTime = 10;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && keypadUI.activeInHierarchy && !keypad.codeSolved)
        {
            keypadUI.SetActive(false);
            canTrigger = false;
            GameManager.EnablePlayer();
            keypad.OnClearClick();
        }
        if (Input.GetKeyDown(KeyCode.E) && canTrigger)
        {
            keypadUI.SetActive(true);
            pressEText.enabled = false;
            canTrigger = false;
            GameManager.DisablePlayer();
        }
        if (keypad.codeSolved && !timerSet)
        {
            keypadUI.SetActive(false);
            canTrigger = false;
            GameManager.EnablePlayer();
            TimerSet();
        }
        if (totalTime == 0 && !reachedSafe) //restart
        {
            keypad.codeSolved = false;
            totalTime = 20;
            timer.text = "20";
            timerSet = false;
            timer.gameObject.SetActive(false);
            keypad.OnClearClick();
        }
        if (reachedSafe)
        {
            safe.GetComponent<BoxCollider>().enabled = false;
            timer.gameObject.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = true;

            pressEText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canTrigger = false;
            pressEText.enabled = false;
        }
    }

    public void TimerSet()
    {
        Map.hintText.text = "Open the safe in time.";
        timer.gameObject.SetActive(true);
        timerSet = true;
        StartCoroutine(Countdown());

    }

    IEnumerator Countdown()
    {
        while(totalTime > 0)
        {
            yield return new WaitForSeconds(1);
            totalTime--;
            timer.text = totalTime.ToString();
        }
    }


}
