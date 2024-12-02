using System.Collections;
using System.Collections.Generic;
using my_Scrips;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;  // Reference to the player object
    public float speed = 3f;   // Speed at which the AI chases the player
    public float chaseRange = 10f;  // Range within which the AI starts chasing the player
    public int health = 100;  // AI's health
    public int maxHealth = 100;  // Maximum health value
    public EnemyHealthBar healthBar;  // Reference to the health bar
    public int damage = 10;

    private float distanceToPlayer;  // Distance between the AI and the player

    public int expAmount = 100;
    public int goldAmount = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  // Initialize health to maximum value
        healthBar.UpdateHealthBar(health, maxHealth);  // Update the health bar
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
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player health: " + health);

        // Trigger the damage animation
        

        // Update the health bar
        healthBar.UpdateHealthBar(health, maxHealth);

        // Check if the player is dead
        if (health <= 0)
        {
            Die();
        }
    }


    
    private void Die()
    {
        Debug.Log("Enemy has died!");
        ExperienceManager.Instance.AddExperience(expAmount);
        GoldManager.Instance.AddGold(randomGoldAmount());
        Destroy(gameObject);
    }
    
    private int randomGoldAmount()
    {
        int randomGold = Random.Range((goldAmount - 2), (goldAmount + 2));
        return randomGold;
    }

    // Collision detection for taking damage with cooldown
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            BulletHit bullet = collision.gameObject.GetComponent<BulletHit>();
            if (bullet != null)
            {
                int damageBullet = bullet.damage;
                TakeDamage(damageBullet);
                

            }
            // Check if the cooldown period has elapsed since the last damage
            Destroy(collision.gameObject);
            
            
        }
    }

    // Optional: Handle damage for trigger-based interactions
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            // Check if the cooldown period has elapsed since the last damage
            
                TakeDamage(25);
              
        }
    }
}