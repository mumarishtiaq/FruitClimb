using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairEntity : MonoBehaviour
{
    [HideInInspector] public MeshRenderer rendrer;
    private AudioSource _source;
    private bool _isCreated = false;
    private GameObject effects;

    public bool IsCreated { get => _isCreated; set => _isCreated = value; }

    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences();
        effects.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //notifying game manager to perform actions, when any player try to create stairs
        if (collision.gameObject.CompareTag("Player") && !_isCreated)
            GameManager.OnStairBuild?.Invoke(this, collision.gameObject);
    }
    private void ResolveReferences()
    {
        if (!_source)
            _source = GetComponent<AudioSource>();

        if (!effects)
            effects = transform.parent.GetComponentInChildren<ParticleSystem>(true).gameObject;

    }
    public void PlayAudio()
    {
        _source.PlayOneShot(_source.clip);
    }

    public void PlayEffectsAndVisualizeStair()
    {
        effects.SetActive(true);
        Invoke(nameof(EnableRendrer), .1f);
    }

    private void EnableRendrer()
    {
        rendrer.enabled = true;
    }
}
