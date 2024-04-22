using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Flee : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 20f;
    public float safeDistance = 16f;
    public float avoidanceRadius = 15f;
    public Animator TomNPCAnimation;


    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        obstacle = GetComponent<NavMeshObstacle>();
        TomNPCAnimation = GameObject.FindWithTag("TomAnimations").GetComponent<Animator>();
        obstacle.enabled = false; // Disable the obstacle initially
    }

    void Update()
    {
        TomNPCAnimation.SetBool("NPCRun", false);
        if (player != null)
        {
            // Calculate distance between enemy and player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if player is within detection range
            if (distanceToPlayer < detectionRange)
            {
                // Check if enemy is too close to player
                if (distanceToPlayer < safeDistance)
                {
                    // Calculate direction away from player
                    Vector3 moveDirection = transform.position - player.position;
                    moveDirection.Normalize();

                    //call animation
                    TomNPCAnimation.SetBool("NPCRun", true);

                    // Move enemy away from player
                    agent.enabled = true; // Enable NavMeshAgent
                    obstacle.enabled = false; // Disable NavMeshObstacle
                    agent.SetDestination(transform.position + moveDirection);
                }
                
            }
        }
    }
}
