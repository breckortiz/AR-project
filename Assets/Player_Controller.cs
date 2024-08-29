using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float jumpForce = 70f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject trigger;
    public GameObject ball;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float groundCheckRadius = 0.2f;
    public float launchForce;

    void Start()
    {
        Debug.Log("Game Started");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Space button pressed");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchObjectsInHitbox();
        }
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D keys or Left/Right arrows
        float moveVertical = Input.GetAxis("Vertical"); // W/S keys or Up/Down arrows

        // Calculate the movement vector
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Directly set the Rigidbody's velocity based on input
        // Zero out the vertical velocity if you only want to move on the XZ plane
        rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.z * moveSpeed);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("HitGroud");
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Debug.Log("left Groud");
            isGrounded = false;
        }
    }


    private void LaunchObjectsInHitbox()
    {
        // Get the Collider component of the trigger hitbox
        Collider hitboxCollider = trigger.GetComponent<Collider>();

        if (hitboxCollider == null || !hitboxCollider.isTrigger)
        {
            Debug.LogError("Trigger hitbox is not set up correctly.");
            return;
        }

        // Iterate through all colliders within the trigger hitbox
        Collider[] hitColliders = Physics.OverlapBox(hitboxCollider.bounds.center, hitboxCollider.bounds.extents, Quaternion.identity);
        Debug.LogError("HEre");
        foreach (Collider collider in hitColliders)
        {
            // Check if the collider has a Rigidbody component
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate direction to launch (example: away from the player)
                Vector3 launchDirection = (collider.transform.position - transform.position).normalized/2 + new Vector3(0,10,0);

                // Apply force to the Rigidbody to launch it
                rb.AddForce(launchDirection * launchForce);
                Debug.LogError("Force Added");
            }
        }
    }
}

