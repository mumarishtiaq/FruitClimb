using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int stairsRemaining;
    public int capacityPoints;

    public TMP_Text capacityPointsText;

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
}
