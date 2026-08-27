using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [SerializeField] private string _prompt;
    [SerializeField] private GameObject cubePrefab; // Reference to the cube prefab you want to spawn

    private bool cubeSpawned = false; // Flag to track whether the cube has been spawned

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with chest");
        audioManager.PlaySFX(audioManager.chest);
        // Check if the cube has already been spawned
        if (!cubeSpawned)
        {
            SpawnCube();
            cubeSpawned = true; // Set the flag to true after spawning the cube
        }

        return true;
    }

    private void SpawnCube()
    {
        // Make sure the cubePrefab is set in the Inspector
        if (cubePrefab != null)
        {
            // Calculate the position in front of the chest
            Vector3 spawnPosition = transform.position + transform.forward * 2f; // 2f is the distance in front of the chest

            // Spawn the cube at the calculated position
            GameObject spawnedCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

            // Optionally, you can do additional setup for the spawned cube if needed
            // For example, you might want to parent it to the chest or apply some specific behavior.

            // Example: Parenting the cube to the chest
            spawnedCube.transform.parent = transform;

            // Example: Applying a force to the spawned cube (requires Rigidbody component)
            Rigidbody cubeRigidbody = spawnedCube.GetComponent<Rigidbody>();
            if (cubeRigidbody != null)
            {
                cubeRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogError("Cube prefab not assigned in the inspector!");
        }
    }
}
