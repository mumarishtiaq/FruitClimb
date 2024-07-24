using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitEntity : MonoBehaviour
{
    public Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyRigidBody(other);
    }

    private void DestroyRigidBody(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (rb != null)
                Destroy(rb);
        }
    }
}
