using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFollowsMouse : MonoBehaviour
{
    public Transform character;   // Reference to the character's transform
    public float radius = 1.0f;   // Radius of the circular path
    public bool lockRotation = true; // Whether or not to lock the bow's rotation

    void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the Z position is 0 for 2D

        // Calculate the direction from the character to the mouse
        Vector3 direction = mousePosition - character.position;

        // Normalize the direction and multiply by the radius to constrain the bow's movement
        direction = direction.normalized * radius;

        // Set the bow's position relative to the character
        transform.position = character.position + direction;

        // If lockRotation is enabled, rotate the bow towards the mouse
        if (lockRotation)
        {
            // Calculate the angle to face the mouse
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Explicitly set the bow's rotation, ignoring character flipping
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        // Fix local scaling to ensure the bow doesn't flip (based on character's scale)
        transform.localScale = new Vector3(1f, 1f, 1f); // This ensures the bow scale stays unaffected by the character's scale
    }
}