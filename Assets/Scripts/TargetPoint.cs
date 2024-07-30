using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RemoveTargetPointShaderEffect();
            GameManager.OnPlayerReachedAtTargetPoint?.Invoke(other.gameObject,transform);
        }
            
    }

    //TODO : Implement Target area Shader Effect
    private void RemoveTargetPointShaderEffect()
    {

    }
}
