using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Transform centerDotTransform;
    [SerializeField] private Transform offsetTransform;
    [SerializeField] private LayerMask pickupLayerMask;

    private ObjectInteract objectInteractable;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (objectInteractable == null) //not carrying an object
            {
                float pickupDistance = 2f;
                Vector3 dotPosition = centerDotTransform.position;
                Ray ray = Camera.main.ScreenPointToRay(dotPosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, pickupDistance, pickupLayerMask))
                {
                    Debug.Log(raycastHit.transform);
                    if (raycastHit.transform.TryGetComponent(out objectInteractable))
                    {
                        objectInteractable.Grab(offsetTransform);
                        Debug.Log(objectInteractable);
                    }

                }
            }
            else //carrying something
            {
                objectInteractable.Drop();
                objectInteractable = null;
            }

        }
    }
}
