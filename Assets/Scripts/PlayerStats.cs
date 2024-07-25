using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
   public PlayerType PlayerType;
    public int MaximumCapacity = 10;
    private int _stairsRemaining;
    private int capacityPoints;
    private int _totalPointsCollected;

    public int CapacityPoints { get => capacityPoints; set => capacityPoints = value; }
    public int StairsRemaining { get => _stairsRemaining; set => _stairsRemaining = value; }

    public int TotalPointsCollected { get => _totalPointsCollected; set => _totalPointsCollected = value; }


    private TMP_Text capacityPointsText;

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
        if(!capacityPointsText)
            capacityPointsText = GetComponentInChildren<TMP_Text>();
    }

    public void UpdateStairRemaining(int value = 0, bool isReset = false)
    {
        if (!isReset)
            _stairsRemaining += value;

        else
            _stairsRemaining = 0;
    }

    public void UpdateCapacityPoints(int value = 0, bool isReset = false)
    {
        if (!isReset)
            CapacityPoints  += value;

        else
            CapacityPoints = 0;

        capacityPointsText.text = CapacityPoints.ToString();
    }

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

