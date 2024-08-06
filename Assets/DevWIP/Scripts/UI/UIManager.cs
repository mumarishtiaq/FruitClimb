using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;

    [SerializeField] private Button _playButton;

    private void Reset()
    {
        ResolveReferences();
    }

    private void Awake()
    {
        ResolveReferences();
    }

    private void OnEnable()
    {
        AddListners();
    }
    private void OnDisable()
    {
        RemoveListners();
    }
    private void ResolveReferences()
    {
        
    }

    private void AddListners()
    {
        _playButton.onClick.AddListener(() => PlayGame());
    }

    private void RemoveListners()
    {
        _playButton.onClick.RemoveAllListeners();
    }

    private void PlayGame()
    {
        if (!_sceneLoader)
            _sceneLoader = FindObjectOfType<SceneLoader>();

        _sceneLoader.LoadAndUnloadScene("GameScene", "GUIScene");
    }
}
