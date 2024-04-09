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
    public Vector3 tomLocation;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
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
        tomLocation = transform.position;
    }

    public void SaveData(ref GameData gameData)
    {
        if (gameData != null)
        {
            gameData.tomLocation = tomLocation;
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData != null)
        {
            // Load Tom's location from game data
            tomLocation = gameData.tomLocation;
            // Set Tom's position to the loaded location
            transform.position = tomLocation;
        }
    }
}