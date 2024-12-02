using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{

    public int damage = 25;
    public int defaultDamage = 25;
    // This function will be called when the bullet collides with any other object
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with an object tagged "Enemy" or "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the bullet when it hits an enemy or obstacle
            Destroy(gameObject);  // Destroy the bullet
        }
    }
    
    void Awake()
    {
        ResetDamage();
    }

    public void ResetDamage()
    {
        damage = defaultDamage; // Reset to default damage
    }
}
