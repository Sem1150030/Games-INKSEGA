using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public float speed = 3f;   // Speed at which the AI chases the player
    public float chaseRange = 10f;  // Range within which the AI starts chasing the player

    private float distanceToPlayer;  // Distance between the AI and the player

    // Start is called before the first frame update
    void Start()
    {
        // Check if the player reference is assigned in the inspector
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // Assign player dynamically by tag if not set
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance from the AI to the player
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Check if the player is within chase range
        if (distanceToPlayer <= chaseRange)
        {
            // Move the AI towards the player
            MoveTowardsPlayer();
        }
        else
        {
            // Optional: Implement idle or patrolling behavior if the player is out of range
        }
    }

    // Method to move the AI towards the player
    private void MoveTowardsPlayer()
    {
        // Get direction vector from AI to player
        Vector2 direction = player.transform.position - transform.position;

        // Normalize the direction vector to avoid faster diagonal movement
        direction.Normalize();

        // Move the AI towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}