using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatronicMovement : MonoBehaviour
{
    [System.Serializable]
    public class Path
    {
        public string name;
        public Transform[] waypoints;
    }

    [SerializeField] private Path[] paths;
    [SerializeField] private float moveInterval = 5f;
    [SerializeField] private float moveChance = 0.5f; // 50% chance to move
    [SerializeField] private int currentPathIndex = 0;
    [SerializeField] private int currentWaypointIndex = 0;
    [SerializeField] private GameManager gameManager; // Reference to game manager for game over
    [SerializeField] private GameObject jumpscareObject; // The jumpscare visual prefab or canvas
    [SerializeField] private AudioSource jumpscareSound;

    [SerializeField] private CameraSystem cameraSystem; // Optional: reference to detect player camera

    private float moveTimer;

    private void Start()
    {
        moveTimer = moveInterval;
        MoveToWaypoint();
    }

    private void Update()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0f)
        {
            moveTimer = moveInterval;

            if (Random.value < moveChance)
            {
                MoveToNextWaypoint();
            }
        }
    }

    void MoveToNextWaypoint()
    {
        Path path = paths[currentPathIndex];
        currentWaypointIndex++;

        if (currentWaypointIndex >= path.waypoints.Length)
        {
            currentWaypointIndex = path.waypoints.Length - 1;
            TriggerJumpscare();
            return;
        }

        MoveToWaypoint();
    }

    void TriggerJumpscare()
    {
        if (jumpscareObject != null)
            jumpscareObject.SetActive(true);

        if (jumpscareSound != null)
            jumpscareSound.Play();

        if (gameManager != null)
            gameManager.GameOver();

        Debug.Log("Jumpscare triggered!");

    }

    void MoveToWaypoint()
    {
        Transform target = paths[currentPathIndex].waypoints[currentWaypointIndex];
        transform.position = target.position;
        transform.rotation = target.rotation;

        Debug.Log($"{gameObject.name} moved to {paths[currentPathIndex].name} waypoint {currentWaypointIndex}");

        // Optionally scare the player if they're watching the current cam
        if (cameraSystem != null && cameraSystem.CurrentCam == currentWaypointIndex)
        {
            // e.g. play jumpscare noise
        }
    }
}
