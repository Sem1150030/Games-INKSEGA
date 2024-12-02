using System;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class PlayerController2D : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    public int health = 100; // Player's health
    public int maxHealth = 100; // Maximum health value
    [SerializeField] public int currentExp;
    [SerializeField] public int maxExp;
    [SerializeField] public int level;
    [SerializeField] public int goldAmount;
    public float damageCooldown = 0.6f; // Cooldown time in seconds between taking damage
    private Animator animator; // Reference to the Animator on the child GameObject

    // Private variables
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool useHorizontalPriority = true; // Flag to track if horizontal input has priority
    private float lastDamageTime = 0f; // Tracks the last time the player took damage

    public HealthBar healthBar;
    public ExpBar expBar;
    [SerializeField] TMP_Text GoldCounter;    
    
    //Shop stats
    public int healthUpgradeCost = 100;
    public int speedUpgradeCost = 250;
    public int damageUpgradeCost = 200;
    public int fireRateUpgradeCost = 150;

    public int maxHealthUpgrades = 5;
    public int maxSpeedUpgrades = 4;
    public int maxDamageUpgrades = 10;
    public int maxFireRateUpgrades = 7;

    public int currentHealthUpgrades = 0;
    public int currentSpeedUpgrades = 0;
    public int currentDamageUpgrades = 0;
    public int currentFireRateUpgrades = 0;
    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Set up health bar
        healthBar.SetMaxHealth(maxHealth);

        // Find the child GameObject named "player_0" and get its Animator component
        Transform child = transform.Find("Player_0");
        if (child != null)
        {
            animator = child.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Child GameObject 'player_0' not found!");
        }

        // Initialize the timer for the last damage
        lastDamageTime = -damageCooldown; // Ensures player can take damage immediately if needed
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Flip the character sprite based on horizontal input
        if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1); // Flip horizontally
        }
        else if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1); // Normal orientation
        }

        // Check if diagonal movement is allowed
        if (canMoveDiagonally)
        {
            movement = new Vector2(horizontalInput, verticalInput);
            if (movement.sqrMagnitude > 1)
            {
                movement = movement.normalized;
            }
        }
        else
        {
            if (horizontalInput != 0)
            {
                useHorizontalPriority = true;
            }
            else if (verticalInput != 0)
            {
                useHorizontalPriority = false;
            }

            if (useHorizontalPriority)
            {
                movement = new Vector2(horizontalInput, 0);
            }
            else
            {
                movement = new Vector2(0, verticalInput);
            }
        }
    }

    void FixedUpdate()
    {
        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = movement * speed;

        // Update the Animator with both x and y velocity components
        if (animator != null)
        {
            // Use both x and y velocity to determine if the player is moving
            float horizontalVelocity = rb.velocity.x;
            float verticalVelocity = rb.velocity.y;

            // Update horizontal velocity parameter for running animation
            animator.SetFloat("xVelocity", Mathf.Abs(horizontalVelocity));
            
            if(horizontalVelocity == 0){
            // Optional: You could set yVelocity for any specific vertical animation (e.g., jump or fall)
            animator.SetFloat("xVelocity", Mathf.Abs(verticalVelocity));
            }

            // Debugging: log the velocity values
        }
    }


    // Method to take damage
   
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player health: " + health);

        // Trigger the damage animation
        if (animator != null)
        {
            animator.ResetTrigger("TakeDamage");
            animator.SetTrigger("TakeDamage");
        }


        // Update the health bar
        healthBar.setHealth(health);

        // Check if the player is dead
        if (health <= 0)
        {
            Die();
        }
    }


    // Handle player death
    private void Die()
    {
        Debug.Log("Player has died!");
        // Add death logic here, e.g., disable movement, trigger animation, or restart the level
    }

    // Collision detection for taking damage with cooldown
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyAi enemy = collision.gameObject.GetComponent<EnemyAi>();
            if (enemy )
            {
                int damage = enemy.damage;
                
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    TakeDamage(damage);
                    lastDamageTime = Time.time; // Update the last damage time
                }
            }
            // Check if the cooldown period has elapsed since the last damage
            
        }
    }

    // Optional: Handle damage for trigger-based interactions
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Access the EnemyAi script to get the damage value
            EnemyAi enemy = other.GetComponent<EnemyAi>();
            if (enemy != null)
            {
                int damage = enemy.damage; // Get the enemy's damage value

                // Apply damage if cooldown period has elapsed
                if (Time.time >= lastDamageTime + damageCooldown)
                {
                    TakeDamage(damage);
                    lastDamageTime = Time.time; // Update the last damage time
                }
            }
        }
    }

    private void OnEnable()
    {
        ExperienceManager.Instance.OnExperienceChange += HandleExperienceChange;
        GoldManager.Instance.OnGoldChange += HandleGoldChange;
    }

    private void OnDisable()
    {
        ExperienceManager.Instance.OnExperienceChange -= HandleExperienceChange;
        GoldManager.Instance.OnGoldChange -= HandleGoldChange;
    }

    private void HandleExperienceChange(int newExp)
    {
        currentExp += newExp;
        expBar.UpdateExpBar(currentExp, maxExp);
        
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }
    
    private void LevelUp()
    {
        level++;
        maxExp = level * 100;
        currentExp = 0;
        maxHealth += 10;
        health = health + (5 * level);
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        
        healthBar.SetMaxHealth(maxHealth);
        healthBar.setHealth(health);
        
        expBar.SetMaxExp(maxExp);
        expBar.UpdateExpBar(currentExp, maxExp);
        
    }
    
    
    
    private void HandleGoldChange(int newGold)
    {
        goldAmount += newGold;
        GoldCounter.text =  (goldAmount.ToString());

    }
    
}
