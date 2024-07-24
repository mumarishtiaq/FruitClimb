using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitEntity : MonoBehaviour
{
    [HideInInspector]public SpriteRenderer spriteRendrer;
    private Rigidbody _rb;
    private AudioSource _source;


    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences();
    }

    private void OnTriggerEnter(Collider other)
    {
        //destroy Rigidbody when fruit hit with ground
        if (other.gameObject.layer == 6)
            DestroyRigidBody();

        //notifying game manager to perform actions, when any player try to collect fruit
        if (other.CompareTag("Player"))
            GameManager.OnFruitCollected?.Invoke(this , other.gameObject);
            
    }

    private void ResolveReferences()
    {
        if(!_rb)
            _rb = GetComponent<Rigidbody>();

        if (!_source)
            _source = GetComponent<AudioSource>();

    }

    private void DestroyRigidBody()
    {
            if (_rb != null)
                Destroy(_rb);   
    }

    public void PlayAudio()
    {
        _source.PlayOneShot(_source.clip);
    }

}
