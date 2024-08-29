using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFloor : MonoBehaviour
{
    public GameObject spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is tagged "Player"
        if (other.CompareTag("hitbox"))
        {
            // Teleport the player to the spawn point's position
            other.transform.position = spawnpoint.transform.position;
        }
    }
}
