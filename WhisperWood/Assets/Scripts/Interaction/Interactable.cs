using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//using Microsoft.Unity.VisualStudio.Editor;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Transform centerDotTransform;
    [SerializeField] private Transform offsetTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private ObjectInteract objectInteractable;
    public ItemIndex ItemIndex;

    public GameObject addToInventoryButton;
    public GameObject interactingObject;
    public Image interactableImage;

    public Sprite keyImage;
    public Sprite paintingPieceImage;
    public Sprite playingCardImage;
    public Sprite scrollImage;

    public GameObject inventoryUI;
    public Button useButton;
    public Button slot1Button;
    public Button slot2Button;
    public Button slot3Button;  

    public List<Button> slotButtons;

    public bool addedKey;
    public bool addedPaintingPiece;
    public bool addedPlayingCard;
    public bool addedScroll;
    
    

    private void Start()
    {
        addToInventoryButton.SetActive(false);
        interactableImage.gameObject.SetActive(false);
        inventoryUI.SetActive(false);
        slotButtons = new List<Button>();
        slotButtons.Add(slot1Button);
        slotButtons.Add(slot2Button);
        slotButtons.Add(slot3Button);

        foreach (Button slotButton in slotButtons)
        {
            slotButton.onClick.AddListener(() => OnSlotClick(slotButton));
            slotButton.enabled = false;
            slotButton.gameObject.SetActive(false); 
        }

    }

    public void OnSlotClick(Button slotButton)
    {
        useButton.gameObject.SetActive(true);
        useButton.onClick.AddListener(() => OnUseButtonClick(slotButton.GetComponent<Image>().sprite));
    }

    public void OnUseButtonClick(Sprite Item)
    {
        if (Item == keyImage)
        {
            //Check if the key can be used via GameManager

        }
        useButton.gameObject.SetActive(false);  
    }

    public void OnInventoryButtonClick()
    {
        CheckInteractingObject(interactingObject);
        interactableImage.gameObject.SetActive(false);
        addToInventoryButton.SetActive(false);
        objectInteractable.gameObject.SetActive(false);
        GameManager.EnablePlayer();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !GameManager.isGamePaused)
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
                        //objectInteractable.Grab(offsetTransform);
                        Debug.Log(objectInteractable);
                        GameManager.DisablePlayer();

                        interactableImage.gameObject.SetActive(true);
                        addToInventoryButton.SetActive(true);
                        interactingObject = objectInteractable.gameObject;

                        
                        AddInteractableObjectImage(interactingObject);
                        
                    }

                }
            }
            else //carrying something
            {
                interactableImage.gameObject.SetActive(false);
                objectInteractable = null;
                addToInventoryButton.SetActive(false);

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
                GameManager.DisablePlayer();

            }
            else
            {
                inventoryUI.SetActive(false);
                GameManager.EnablePlayer();
            }

        }


    }

    private void AddInteractableObjectImage(GameObject interactableObject) //Before you press the button to add to inventory (allows for exiting out)
    {
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

    }

    private void CheckInteractingObject(GameObject interactableObject) //After you have pressed the button to add to inventory
    {
        if (interactableObject.CompareTag("LuggageKey"))
        {
            ItemIndex.AddItemToInventory("LuggageKey");
        }
        if (interactableObject.CompareTag("PaintingPiece"))
        {
            ItemIndex.AddItemToInventory("PaintingPiece");
        }
        if (interactableObject.CompareTag("PlayingCard"))
        {
            ItemIndex.AddItemToInventory("PlayingCard");
        }
        if (interactableObject.CompareTag("Scroll"))
        {
            ItemIndex.AddItemToInventory("Scroll");
        }

    }

    private void SetInventory()
    {

        foreach (Button slotButton in slotButtons)
        {
            if (slotButton.enabled == false)
            {
                if (ItemIndex.inventoryItems.ContainsKey("LuggageKey") && !addedKey)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = keyImage;
                    addedKey = true;
                }
                if (ItemIndex.inventoryItems.ContainsKey("PaintingPiece") && !addedPaintingPiece)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = paintingPieceImage;
                    addedPaintingPiece = true;

                }
                if (ItemIndex.inventoryItems.ContainsKey("PlayingCard") && !addedPlayingCard)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = playingCardImage;
                    addedPlayingCard = true;
                }
                if (ItemIndex.inventoryItems.ContainsKey("Scroll") && !addedScroll)
                {
                    slotButton.gameObject.SetActive(true);
                    slotButton.enabled = true;
                    slotButton.GetComponent<Image>().sprite = scrollImage;
                    addedScroll = true;
                }

            }
        }


        
    }
}
