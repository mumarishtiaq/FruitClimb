using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<FruitEntity,GameObject> OnFruitCollected;
    public static Action<StairEntity,GameObject> OnStairBuild;

    [SerializeField] private List<PlayerStats> _playersPool;
    private StairsSpawner _stairSpawner;

   
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

        if(!_stairSpawner)
            _stairSpawner=FindObjectOfType<StairsSpawner>();
    }
    private void SubscribeEvents(bool doSunscribe)
    {
        if (doSunscribe)
        {
            OnFruitCollected += OnFruitCollectedRequested;
            OnStairBuild += OnBuildStairRequested;
        }
        else
        {
            OnFruitCollected -= OnFruitCollectedRequested;
            OnStairBuild -= OnBuildStairRequested;
        }
    }

    private void OnFruitCollectedRequested(FruitEntity fruit, GameObject player)
    {
        var playerStats = GetPlayerStats(player);

        if(playerStats.CapacityPoints < playerStats.MaximumCapacity)
        {
            var pointstoAdd = Mathf.Min(fruit.template.points,playerStats.MaximumCapacity-playerStats.CapacityPoints);
            Debug.LogWarning("in colecting fruit" + pointstoAdd);
            //adding capacity point
            playerStats.UpdateCapacityPoints(pointstoAdd);

            //Increase Count for total collected fruits
            playerStats.UpdateTotalPoints(pointstoAdd);

            //update Blocker Position
            _stairSpawner.UpdateBlockerPosition(playerStats);


            //play fruit collection audio 
            fruit.PlayAudio();

            //Vanish Fruit
            fruit.spriteRendrer.enabled = false;
            Destroy(fruit.gameObject, 1f);
        }
    }
    private void OnBuildStairRequested(StairEntity stair, GameObject player)
    {
        var playerStats = GetPlayerStats(player);

        if (playerStats.CapacityPoints >= 1)
        {
            //subtracting capacity point
            playerStats.UpdateCapacityPoints(-1);

            //updating stairs remaining
            playerStats.UpdateStairRemaining(-1);

            //play stair created audio 
            stair.PlayAudio();

            //set is Created = true, so this stair will not be considered again if collided 
            stair.IsCreated = true;

            //Display Stair 
            stair.rendrer.enabled = true;

            
        }

    }

    private PlayerStats GetPlayerStats(GameObject player)
    {
       return  _playersPool.FirstOrDefault(playerStat => playerStat.gameObject == player);
    }

}
