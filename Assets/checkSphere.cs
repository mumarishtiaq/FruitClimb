using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkSphere : MonoBehaviour
{
    public float radius = 3f;
    public Collider[] allCollidersSphere;
    int ignoreItself = 3;

    // Update is called once per frame
    void Update()
    {
      
    }


    public void checker()
    {
        allCollidersSphere = Physics.OverlapSphere(transform.position, radius,ignoreItself);
        

       
    }
}
