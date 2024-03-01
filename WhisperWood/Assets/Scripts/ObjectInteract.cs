using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteract : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform offsetTransform;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }



    public void Grab(Transform offsetTransform)
    {
        this.offsetTransform = offsetTransform;
        objectRigidbody.constraints = RigidbodyConstraints.None;
        objectRigidbody.useGravity = false;
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
