using DG.Tweening;
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

    public static bool IsMovementLock = false;


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

        if(!IsMovementLock)
            FollowCameraNewApproach();
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

    private void FollowCameraNewApproach()
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

    public void TweenCameraToTargetPosition(Transform target, float duration = 2f)
    {
        // Move the camera to the target position
        var offset = new Vector3(-7f, 5f, -2.5f);
        var newCamPos = target.position + offset;
        transform.DOMove(newCamPos, duration).SetEase(Ease.InOutQuad);

        // Rotate the camera to look at the target
        transform.DORotateQuaternion(Quaternion.LookRotation(target.position - newCamPos), duration).SetEase(Ease.InOutQuad);
    }
    }
