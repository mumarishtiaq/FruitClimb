using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshPro>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
