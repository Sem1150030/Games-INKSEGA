using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootScript : MonoBehaviour
{
    //audio
    // AudioSource audioSource;
    // public float ClipLength = 1f;
    // public AudioClip shootSound;
    
    
    // public Transform Gun;
    public GameObject Bullet;
    public float bulletSpeed;
    public Transform shootPoint;
    private Vector2 direction;
    public float fireRate;
    public int damage = 25;

    private float readyForNextShot;
    
    private void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        // Get mouse position
        // Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // direction = mousePos - (Vector2)Gun.position;
        // FaceMouse();

        // Check if the mouse button is held down (for automatic fire)
        if (Input.GetMouseButton(0)) // Mouse button is held down
        {
            // Only shoot if enough time has passed according to fireRate
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + 1 / fireRate; // Update time for next shot
                Shoot(); // Shoot a bullet
            }
        }
    }

    // void FaceMouse()
    // {
    //     Gun.transform.right = direction; // Rotate the gun to face the mouse
    // }

    void Shoot()
    {
        // Instantiate and shoot the bullet
        GameObject BulletIns = Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
        

        // Ensure Rigidbody2D is present and apply velocity
        Rigidbody2D rb = BulletIns.GetComponent<Rigidbody2D>();
        if (rb == null) 
        {
            rb = BulletIns.AddComponent<Rigidbody2D>(); // Add Rigidbody2D if missing
        }

        // Set velocity to the bullet
        rb.velocity = BulletIns.transform.right * bulletSpeed;  // Apply velocity directly

        Destroy(BulletIns, 3f); // Destroy bullet after 3 seconds
    }

    // Change this to OnTriggerEnter2D
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debugging: Log the name of the object the bullet collided with
        Debug.Log("Bullet collided with: " + collider.gameObject.name);

        // Destroy the bullet if it hits an object tagged "Enemy" or "Obstacle"
        if (collider.gameObject.CompareTag("Enemy") || collider.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Bullet destroyed upon hitting: " + collider.gameObject.name);
            Destroy(gameObject);  // Destroy the bullet
        }
    }
    
    
}
