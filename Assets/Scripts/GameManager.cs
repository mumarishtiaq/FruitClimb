using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<FruitEntity,GameObject> OnFruitCollected;

    [SerializeField] private List<PlayerStats> _playersPool;

   
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
        SubscribeEvents(true);
    }
    private void OnDisable()
    {
        SubscribeEvents(false);
    }

    private void ResolveReferences()
    {
        if (_playersPool.Count != 2)
            _playersPool = FindObjectsOfType<PlayerStats>().ToList();
    }
    private void SubscribeEvents(bool doSunscribe)
    {
        if (doSunscribe)
        {
            OnFruitCollected += OnFruitCollectedRequested;
        }
        else
        {
            OnFruitCollected -= OnFruitCollectedRequested;
        }
    }

    private void OnFruitCollectedRequested(FruitEntity fruit, GameObject player)
    {
        var playerStats = GetPlayerStats(player);

        if(playerStats.CapacityPoints < playerStats.MaximumCapacity)
        {
            //adding capacity point
            playerStats.UpdateCapacityPoints(1);

            //play fruit collection audio 
            fruit.PlayAudio();

            //Vanish Fruit
            fruit.spriteRendrer.enabled = false;
            Destroy(fruit.gameObject, 1f);
        }


    }

    private PlayerStats GetPlayerStats(GameObject player)
    {
       return  _playersPool.FirstOrDefault(playerStat => playerStat.gameObject == player);
    }

}
