using UnityEngine;
using UnityEngine.UI;  // Required for UI Text
using TMPro;  
public class PlayerController2D : MonoBehaviour
{
    // Public variables
    public float speed = 5f; // The speed at which the player moves
    public bool canMoveDiagonally = true; // Controls whether the player can move diagonally
    public int health = 100; // Player's health
    public TextMeshProUGUI  healthText; // Reference to the UI Text component

    // Private variables 
    private Rigidbody2D rb; // Reference to the Rigidbody2D component attached to the player
    private Vector2 movement; // Stores the direction of player movement
    private bool useHorizontalPriority = true; // Flag to track if horizontal input has priority

    void Start()
    {
        // Initialize the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        // Prevent the player from rotating
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Ensure that health is displayed at the start
        UpdateHealthText();
    }

    void Update()
    {
        // Get player input from keyboard or controller
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Debugging - Check if inputs are being read

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
        // Check movement before applying it
       

        // Apply movement to the player in FixedUpdate for physics consistency
        rb.velocity = movement * speed;
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player health: " + health);

        // Update the health UI text
        UpdateHealthText();

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
        // Example: Destroy(gameObject);
    }

    // Update the health text UI
    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
            Debug.Log("Updated Health Text: " + healthText.text);  // Debugging line
        }
        else
        {
            Debug.Log("Health Text not assigned!");
        }
    }


    // Collision detection for taking damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }
}
