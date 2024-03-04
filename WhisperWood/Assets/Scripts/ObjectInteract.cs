using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform offsetTransform;
    public GameObject dialogueTrigger1;
    public GameObject dialogueTrigger2;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        dialogueTrigger1.SetActive(true);
        dialogueTrigger2.SetActive(false);
    }

    public void Grab(Transform offsetTransform)
    {
        this.offsetTransform = offsetTransform;
        objectRigidbody.constraints = RigidbodyConstraints.None;
        objectRigidbody.useGravity = false;

        //dialogue testing
        dialogueTrigger1.SetActive(false);
        dialogueTrigger2.SetActive(true);
    }

    public void Drop()
    {
        this.offsetTransform = null;
        objectRigidbody.useGravity = true;
        objectRigidbody.constraints = RigidbodyConstraints.FreezeAll & ~RigidbodyConstraints.FreezePositionY;
       
    }

    private void FixedUpdate()
    {
        if (offsetTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, offsetTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
