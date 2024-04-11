using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TomMovement : MonoBehaviour, IDataPersistence
{
    public Transform destination; // Destination where the NPC should move
    private NavMeshAgent navMeshAgent;
    private bool hasReachedDestination = false;
    private bool isMoving = false;
    public float rotationAngle = 145f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);

    }

    void Update()
    {
        if (!hasReachedDestination && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && isMoving)
        {
            // If the NPC has reached the destination, stop its movement
            hasReachedDestination = true;
            navMeshAgent.isStopped = true;
            RotateNPC();
        }
        
    }

     public void MoveToDestination()
    {
        if (destination != null)
        {
            //call animation
            isMoving = true;
            navMeshAgent.SetDestination(destination.position);
            hasReachedDestination = false;
            navMeshAgent.isStopped = false;
        }
        else
        {
            Debug.LogError("Destination not set!");
        }
    }

    void RotateNPC()
    {

        // Calculate the new rotation angle
        Quaternion targetRotation = Quaternion.Euler(0f, -rotationAngle, 0f);
        // Apply the rotation to the NPC
        transform.rotation *= targetRotation;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.tomPosition = transform.position;
        gameData.tomRotation = transform.rotation;
    }

    public void LoadData(GameData gameData)
    {
        transform.position = gameData.tomPosition;
        transform.rotation = gameData.tomRotation;
    }
}