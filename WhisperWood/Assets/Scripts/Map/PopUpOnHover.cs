using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject popup; // Assign your pop-up UI element in the Inspector
    private bool isHovering;

    private void Start()
    {
        popup.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        popup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        popup.SetActive(false);
    }

    private void Update()
    {
        // Optionally, add logic to keep the pop-up visible if needed
        if (isHovering)
        {
            // Additional logic here, if required
        }
    }
}
