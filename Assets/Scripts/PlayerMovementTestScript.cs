using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMovementTestScript : MonoBehaviour
{
    public float moveSpeed;
    public FixedJoystick joystick;
    private Rigidbody rb;

    private void Awake()
    {
        rb =  GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical* moveSpeed);
    }
}
