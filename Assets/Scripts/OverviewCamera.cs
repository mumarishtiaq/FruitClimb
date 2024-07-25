using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverviewCamera : MonoBehaviour
{
    public Transform player;             // Reference to the player
    public float smoothSpeed = 0.125f;   // Smoothing factor
    public float followDistanceX = 7.0f; // Max distance the camera can move along the X axis
    public float additionalFollowDistanceX = 2.0f; // Additional distance to follow beyond the ground

    public float followDistanceY = 7.0f; 
    public float additionalFollowDistanceY = 2.0f; 


    private Vector3 initialPosition;     // Initial position of the camera

    void Start()
    {
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player == null) return;
        //MOveCameraOnOnlyX();
        //MOveCameraOnXandZ();
        aa();
    }

    private void MOveCameraOnOnlyX()
    {
        // Calculate desired camera X position based on player's X position
        float targetX = Mathf.Clamp(player.position.x, initialPosition.x - followDistanceX, initialPosition.x + followDistanceX);

        // Keep the Y and Z positions unchanged
        Vector3 targetPosition = new Vector3(targetX, initialPosition.y, initialPosition.z);

        // Smoothly interpolate camera position towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    private void aa()
    {
        // Calculate desired camera X position based on player's X position
        float targetX = Mathf.Clamp(player.position.x, initialPosition.x - followDistanceX, initialPosition.x + followDistanceX);

        // If the player goes beyond the positive X axis of the ground, follow more closely
        if (player.position.x > initialPosition.x + followDistanceX)
        {
            targetX = player.position.x + additionalFollowDistanceX;
        }

        //// Calculate desired camera X position based on player's X position
        //float targetY = Mathf.Clamp(player.position.y, initialPosition.y - followDistanceY, initialPosition.y + followDistanceY);

        //// If the player goes beyond the positive X axis of the ground, follow more closely
        //if (player.position.y > initialPosition.y + followDistanceY)
        //{
        //    targetY = player.position.y + additionalFollowDistanceY;
        //}

        // Keep the Z position unchanged
        Vector3 targetPosition = new Vector3(targetX, initialPosition.y, initialPosition.z);

        // Smoothly interpolate camera position towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

    }
    }
