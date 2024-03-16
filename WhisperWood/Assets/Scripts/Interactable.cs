using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Transform centerDotTransform;
    [SerializeField] private Transform offsetTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private ObjectInteract objectInteractable;

    public GameObject addToInventoryButton;
    public GameObject interactingObject;
    

    private void Start()
    {
        addToInventoryButton.SetActive(false);
        interactingObject = null;
    }

    public void OnInventoryButtonClick()
    {
        interactingObject.SetActive(false);
        addToInventoryButton.SetActive(false);
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
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objectInteractable))
                    {
                        objectInteractable.Grab(offsetTransform);
                        Debug.Log(objectInteractable);
                        addToInventoryButton.SetActive(true);
                        interactingObject = objectInteractable.gameObject;
                    }

                }
            }
            else //carrying something
            {
                objectInteractable.Drop();
                Debug.Log("reaching");
                objectInteractable = null;
                addToInventoryButton.SetActive(false);
            }

        }
    }
}
