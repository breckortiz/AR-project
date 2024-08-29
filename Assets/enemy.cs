using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float radius = 5f; // Radius of the circular path
    public float speed = 1f; // Speed of the movement
    public GameObject spawnpoint;
    float original_x;
    float original_z;
    public GameObject ball;
    public GameObject Player;


    private float angle = 0f; // Current angle in radians
    private void Start()
    {
        original_x = transform.position.x;
        original_z = transform.position.z;
    }
    void Update()
    {
        // Calculate new position based on the angle
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Update the cube's position
        transform.position = new Vector3(x + original_x, transform.position.y, z + original_z);

        // Increment the angle based on speed and time
        angle += speed * Time.deltaTime;

        // Optional: Reset angle to avoid floating-point precision issues
        if (angle >= 2 * Mathf.PI)
        {
            angle -= 2 * Mathf.PI;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is tagged "Player"
        if (other.CompareTag("hitbox"))
        {
            // Teleport the player to the spawn point's position
            other.transform.position = spawnpoint.transform.position;
            ball.transform.position = spawnpoint.transform.position;
            Player.transform.position = spawnpoint.transform.position;
        }
    }
}
