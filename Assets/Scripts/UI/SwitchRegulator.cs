using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteAlways]
public class SwitchRegulator : MonoBehaviour
{
    [SerializeField]
    private bool _isOn;  // This will act like the toggle state

    public UnityEvent<bool> onValueChanged;  // Custom event to handle toggle state changes

    private Button _btn;

    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    // Public property to get or set the toggle state from other scripts
    public bool isOn
    {
        get => _isOn;
        set
        {
            if (_isOn != value)
            {
                _isOn = value;
                UpdateButtonAppearance();
                onValueChanged.Invoke(_isOn);  // Trigger the event when state changes
            }
        }
    }

    void Awake()
    {
        ResolveReferences();
        Debug.Log("On awake");
        if (!Application.isPlaying)
        {
            UpdateButtonAppearance();  // Update appearance in the editor
        }
    }

    private void Reset()
    {
        ResolveReferences();
        Debug.Log("On reset");
    }
    void Start()
    {
        _btn.onClick.AddListener(ToggleState);  // Add click listener
    }

    // Method to toggle the state
    public void ToggleState()
    {
        isOn = !isOn;
    }

    // Method to update the button appearance based on the toggle state
    private void UpdateButtonAppearance()
    {
        var sprite = _isOn ? _onSprite : _offSprite;

        if (sprite)
            _btn.image.sprite = sprite;

        else
            Debug.LogWarning("Sprite is null");
        //if (_isOn)
        //{
        //    _btn.image.color = Color.green; 
        //}
        //else
        //{
        //    _btn.image.color = Color.red;  
        //}
    }

    // This method is called when a value is changed in the editor
    private void OnValidate()
    {
        ResolveReferences();
        UpdateButtonAppearance();
    }

    private void ResolveReferences()
    {
        if (!_btn)
            _btn = GetComponent<Button>();
    }

}
