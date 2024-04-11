using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//using Microsoft.Unity.VisualStudio.Editor;

public class Interactable : MonoBehaviour, IDataPersistence
{
    [SerializeField] private Transform centerDotTransform;
    [SerializeField] private Transform offsetTransform;
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private TextMeshProUGUI textToPlayer;

    private ObjectInteract objectInteractable;
    public ItemIndex ItemIndex;


    //public GameObject addToInventoryButton;
    public GameObject interactingObject;
    public Image interactableImage;

    public Sprite combinationImage;
    public Sprite keyImage;
    public Sprite paintingPieceImage;
    public Sprite playingCardImage;
    public Sprite playingCardCloseImage;
    public Sprite scrollImage;
    public Sprite DroranAdImage;
    public Sprite OwnerKeyImage;
    public Sprite walletImage;
    public Sprite listImage;

    public GameObject inventoryUI;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button useButton;
    [SerializeField] private Button inspectButton;
    [SerializeField] private Button exitButton;
    public Button slot1Button;
    public Button slot2Button;
    public Button slot3Button;  
    public Button slot4Button;
    public Button slot5Button;  
    public Button slot6Button;  
    public Button slot7Button;
    public Button slot8Button;
    public Button slot9Button;

    public List<Button> slotButtons;

    public bool addedCombinationImage;
    public bool addedTomKey;
    public bool addedPaintingPiece;
    public bool addedPlayingCard;
    public bool addedScroll;
    public bool addedDroranAd;
    public bool addedOwnerKey;
    public bool addedWallet;
    public bool addedList;

    //Trying to use
    public bool tryingToUsePaintingPiece;
    public bool tryingToUseLuggageKey;
    public bool tryingToUseScroll;
    public bool tryingToUseOwnerKey;
    public bool tryingToUseWallet;
    public bool tryingToUseList;

    private void Start()
    {
        //addToInventoryButton.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
        interactableImage.gameObject.SetActive(false);
        inventoryUI.SetActive(false);
        exitButton.gameObject.SetActive(false);
        textToPlayer.GetComponent<TextMeshProUGUI>().text = "";
        slotButtons = new List<Button>();
        slotButtons.Add(slot1Button);
        slotButtons.Add(slot2Button);
        slotButtons.Add(slot3Button);
        slotButtons.Add(slot4Button);
        slotButtons.Add(slot5Button);
        slotButtons.Add(slot6Button);
        slotButtons.Add(slot7Button);
        slotButtons.Add(slot8Button);
        slotButtons.Add(slot9Button);

        foreach (Button slotButton in slotButtons)
        {
            slotButton.onClick.AddListener(() => OnSlotClick(slotButton));
            slotButton.enabled = false;
            slotButton.gameObject.SetActive(false); 
        }
        inventoryButton.onClick.AddListener(() => OnInventoryButtonClick());
    }

    public void OnSlotClick(Button slotButton)
    {
        useButton.gameObject.SetActive(true);
        useButton.onClick.AddListener(() => OnUseButtonClick(slotButton.GetComponent<Image>().sprite));
        inspectButton.gameObject.SetActive(true);
        inspectButton.onClick.AddListener(() => OnInspectButtonClick(slotButton.GetComponent<Image>().sprite));
    }

    public void OnUseButtonClick(Sprite Item)
    {
        Debug.Log("ITEM: " + Item);

        if (Item == keyImage)
        {
            //Check if the key can be used via GameManager
            tryingToUseLuggageKey = true;
            Debug.Log("HELLO");
        }

        if (Item == paintingPieceImage)
        {
            tryingToUsePaintingPiece = true;
        }

        if (Item == scrollImage)
        {
            tryingToUseScroll = true;
        }

        if (Item == OwnerKeyImage)
        {
            tryingToUseOwnerKey = true;
        }

        if (Item == walletImage)
        {
            tryingToUseWallet = true;
        }

        if (Item == listImage)
        {
            tryingToUseList = true;
        }
        
        //useButton.gameObject.SetActive(false);
    }

    private void OnInspectButtonClick(Sprite Item)
    {
        //inspectButton.gameObject.SetActive(false);   
        interactableImage.gameObject.SetActive(true);
        interactableImage.sprite = Item;
        if (Item == playingCardImage)
        {
            interactableImage.sprite = playingCardCloseImage;
        }

        exitButton.gameObject.SetActive(true);
        exitButton.onClick.AddListener(() => OnExitButtonClick());

    }

    private void OnExitButtonClick()
    {
        exitButton.gameObject.SetActive(false);
        interactableImage.gameObject.SetActive(false);
    }

    public void OnInventoryButtonClick()
    {
        Debug.Log("NAME: " + interactingObject.name);
        CheckInteractingObject(interactingObject);
        interactableImage.gameObject.SetActive(false);
        //addToInventoryButton.SetActive(false);
        inventoryButton.gameObject.SetActive(false);
        objectInteractable.gameObject.SetActive(false);
        objectInteractable = null;
        GameManager.EnablePlayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) /* Input.GetMouseButtonDown(0) */&& !GameManager.isGamePaused)
        {
            if (objectInteractable == null) //not carrying an object
            {
                float pickupDistance = 10f;
                Vector3 dotPosition = centerDotTransform.position;
                Ray ray = Camera.main.ScreenPointToRay(dotPosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    //Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objectInteractable))
                    {
                        GameManager.DisablePlayer();

                        interactableImage.gameObject.SetActive(true);
                        //addToInventoryButton.SetActive(true);
                        inventoryButton.gameObject.SetActive(true);
                        interactingObject = objectInteractable.gameObject;

                        
                        AddInteractableObjectImage(interactingObject);
                        //inventoryButton.onClick.AddListener(() => OnInventoryButtonClick());

                    }

                }
            }
            else //carrying something
            {
                interactableImage.gameObject.SetActive(false);
                objectInteractable = null;
                //addToInventoryButton.SetActive(false);
                inventoryButton.gameObject.SetActive(false);

                GameManager.EnablePlayer();

            }

        }


        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryUI.activeInHierarchy)
            {
                SetInventory();
                inventoryUI.SetActive(true);
                useButton.gameObject.SetActive(false);
                inspectButton.gameObject.SetActive(false);
                exitButton.gameObject.SetActive(false);
                GameManager.DisablePlayer();

            }
            else
            {
                inventoryUI.SetActive(false);
                interactableImage.gameObject.SetActive(false);
                exitButton.gameObject.SetActive(false);
                GameManager.EnablePlayer();
            }

        }


    }

    //IEnumerator WaitForText()
    //{
    //    yield return new WaitForSeconds(.8f);
    //    textToPlayer.GetComponent<TextMeshProUGUI>().text = "";
    //    GameManager.EnablePlayer();
    //}

    private void AddInteractableObjectImage(GameObject interactableObject) //Before you press the button to add to inventory (allows for exiting out)
    {
        if (interactableObject.CompareTag("Combination"))
        {
            interactableImage.sprite = combinationImage;
        }

        if (interactableObject.CompareTag("LuggageKey"))
        {
            interactableImage.sprite = keyImage;
        }
        if (interactableObject.CompareTag("PaintingPiece"))
        {
            interactableImage.sprite = paintingPieceImage;
        }
        if (interactableObject.CompareTag("PlayingCard"))
        {
            interactableImage.sprite = playingCardImage;
        }
        if (interactableObject.CompareTag("Scroll"))
        {
            interactableImage.sprite = scrollImage;
        }
        if (interactableObject.CompareTag("DroranAd"))
        {
            interactableImage.sprite = DroranAdImage;
        }
        if (interactableObject.CompareTag("OwnerKey"))
        {
            interactableImage.sprite = OwnerKeyImage;
        }
        
         if (interactableObject.CompareTag("Wallet"))
        {
            interactableImage.sprite = walletImage;
        } 
           if (interactableObject.CompareTag("List"))
        {
            interactableImage.sprite = listImage;
        }
         
    }

    private void CheckInteractingObject(GameObject interactableObject) //After you have pressed the button to add to inventory
    {
        ItemIndex.AddItemToInventory(interactableObject.tag);
        //inventoryButton.onClick.AddListener(() => OnInventoryButtonClick());
        //inventoryButton.onClick.RemoveListener(() => OnInventoryButtonClick());
    }

    private void SetInventory()
    {


        foreach (Button slotButton in slotButtons)
        {
            if (slotButton.enabled == false)
            {
                if (ItemIndex.inventoryItems.ContainsKey("Combination") && !addedCombinationImage)
                {
                    Debug.Log("here");
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = combinationImage;
                    addedCombinationImage = true;
                }
                else if (ItemIndex.inventoryItems.ContainsKey("LuggageKey") && !addedTomKey)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = keyImage;
                    addedTomKey = true;
                }
                else if (ItemIndex.inventoryItems.ContainsKey("PaintingPiece") && !addedPaintingPiece)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = paintingPieceImage;
                    addedPaintingPiece = true;

                }
                else if (ItemIndex.inventoryItems.ContainsKey("PlayingCard") && !addedPlayingCard)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = playingCardImage;
                    addedPlayingCard = true;
                }
                else if (ItemIndex.inventoryItems.ContainsKey("Scroll") && !addedScroll)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = scrollImage;
                    addedScroll = true;
                }
                else if (ItemIndex.inventoryItems.ContainsKey("DroranAd") && !addedDroranAd)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = DroranAdImage;
                    addedDroranAd = true;
                }
                else if (ItemIndex.inventoryItems.ContainsKey("OwnerKey") && !addedOwnerKey)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = OwnerKeyImage;
                    addedOwnerKey = true;
                }
                
                  else if (ItemIndex.inventoryItems.ContainsKey("Wallet") && !addedWallet)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = walletImage;
                    addedWallet = true;
                }
                  else if (ItemIndex.inventoryItems.ContainsKey("List") && !addedList)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = listImage;
                    addedList = true;
                }
                
            }
        }

    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
            gameData.addedCombinationImage = addedCombinationImage;
            gameData.addedTomKey = addedTomKey;
            gameData.addedPaintingPiece = addedPaintingPiece;
            gameData.addedPlayingCard = addedPlayingCard;
            gameData.addedScroll = addedScroll;
            gameData.addedDroranAd = addedDroranAd;
            gameData.addedOwnerKey = addedOwnerKey;
             gameData.addedWallet = addedWallet;
             gameData.addedList = addedList;
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData != null)
        {
            addedCombinationImage = gameData.addedCombinationImage;
            addedTomKey = gameData.addedTomKey;
            addedPaintingPiece = gameData.addedPaintingPiece;
            addedPlayingCard = gameData.addedPlayingCard;
            addedScroll = gameData.addedScroll;
            addedDroranAd = gameData.addedDroranAd;
            addedOwnerKey = gameData.addedOwnerKey;
            addedWallet = gameData.addedWallet;
            addedList = gameData.addedList;

        }
    }


}
