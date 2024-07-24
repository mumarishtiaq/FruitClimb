using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerType _playerType;
    public int MaximumCapacity = 10;
    private int _stairsRemaining;
    private int capacityPoints;

    public int CapacityPoints { get => capacityPoints; set => capacityPoints = value; }

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
            CapacityPoints += value;

        else
            CapacityPoints = 0;

        capacityPointsText.text = CapacityPoints.ToString();
    }
}

public enum PlayerType
{
    Player1,
    Player2,
    Bot
}

