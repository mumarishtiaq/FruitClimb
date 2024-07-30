using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerType PlayerType;
    public PlayerUI playerUI;
    private int _maximumCapacity = 10;
    private int _stairsRemaining;
    private int _capacityPoints;
    private int _totalPointsCollected;
    private bool _isPlayerWon;

    public int CapacityPoints
    {
        get => _capacityPoints;
        set
        {
            _capacityPoints = value;
            _capacityPointsText.text = value.ToString();
        }
    }
    public int StairsRemaining
    {
        get => _stairsRemaining;
        set { _stairsRemaining = value;
            playerUI.RemainingStaitsTxt = value.ToString();
        }
    }

    public int MaximumCapacity
    {
        get => _maximumCapacity;
        set
        {
            _maximumCapacity = value;
            playerUI.MaxCapacity = value.ToString();
        }
    }


    public int TotalPointsCollected { get => _totalPointsCollected; set => _totalPointsCollected = value; }

    public bool IsPlayerWon
    {
        get => _isPlayerWon;
        set
        {
            _isPlayerWon = value;
            _capacityPointsText.gameObject.SetActive(!value);
            _crown.SetActive(value);
        }
    }

    private TMP_Text _capacityPointsText;
    private GameObject _crown;

    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences();
    }
    private void ResolveReferences()
    {
        if(!_capacityPointsText)
            _capacityPointsText = GetComponentInChildren<TMP_Text>();
        
        if(!_crown)
            _crown = transform.Find("Crown").gameObject;
    }

    //public void UpdateCapacityPoints(int value = 0, bool isReset = false)
    //{
    //    if (!isReset)
    //        CapacityPoints  += value;

    //    else
    //        CapacityPoints = 0;

    //    _capacityPointsText.text = CapacityPoints.ToString();
    //}

    public void UpdateTotalPoints(int value = 0, bool isReset = false)
    {
        if (!isReset)
            TotalPointsCollected += value;

        else
            TotalPointsCollected = 0;
    }
}

public enum PlayerType
{
    Player1,
    Player2,
    Bot
}

