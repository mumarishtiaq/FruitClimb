using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static Action<FruitEntity,GameObject> OnFruitCollected;
    public static Action<StairEntity,GameObject> OnStairBuild;
    public static Action<GameObject,Transform> OnPlayerReachedAtTargetPoint;

    [SerializeField] private List<PlayerStats> _playersPool;
    private StairsSpawner _stairSpawner;
    private FruitSpawner _fruitSpawner;
    private OverviewCamera _overviewCamera;
    private NavMeshSurface _navmeshSurface;

    [SerializeField] private MatchType _matchType;
   
    private void Reset()
    {
        ResolveReferences();
    }
    private void Awake()
    {
        ResolveReferences();
        //InitializeMatch();

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
        
        if(!_fruitSpawner)
            _fruitSpawner=FindObjectOfType<FruitSpawner>();
        
        if(!_overviewCamera)
            _overviewCamera = FindObjectOfType<OverviewCamera>();

        if (!_navmeshSurface)
            _navmeshSurface = FindObjectOfType<NavMeshSurface>();
    }

    public void InitializeMatch()
    {
        //setting Players setting and stats to default
        foreach (var playerStat in _playersPool)
        {
            playerStat.StairsRemaining = MatchSettings.MatchLenght;
            playerStat.CapacityPoints = 0;
            playerStat.IsPlayerWon = false;
        }

        //SpawnStairs
        _stairSpawner.SpawmBothStairs();

        //spawn initial fruits
        _fruitSpawner.SpawnInitialFruits();

        //Bake the environment and startBot
        //start BOT
        if (_matchType == MatchType.QuickPlay || _matchType == MatchType.Story)
        {
            _navmeshSurface.BuildNavMesh();
            BOT_AIController.IsBotSleeping = false;
        }
            

        //Camera movement unlock
        OverviewCamera.IsMovementLock = false;

        //player(s) movement unlock
        PlayerMovement.isMovementLock = false;

        

    }
    private void SubscribeEvents(bool doSunscribe)
    {
        if (doSunscribe)
        {
            OnFruitCollected += OnFruitCollectedRequested;
            OnStairBuild += OnBuildStairRequested;
            OnPlayerReachedAtTargetPoint += OnPlayerReachedAtTargetPointRequested;
        }
        else
        {
            OnFruitCollected -= OnFruitCollectedRequested;
            OnStairBuild -= OnBuildStairRequested;
            OnPlayerReachedAtTargetPoint -= OnPlayerReachedAtTargetPointRequested;
        }
    }

    private void OnFruitCollectedRequested(FruitEntity fruit, GameObject playerGO)
    {
        var playerStats = GetPlayerStats(playerGO);

        if(playerStats.CapacityPoints < playerStats.MaximumCapacity)
        {
            var pointstoAdd = Mathf.Min(fruit.template.points,playerStats.MaximumCapacity-playerStats.CapacityPoints);
            //adding capacity point
            playerStats.CapacityPoints +=pointstoAdd;

            //Increase Count for total collected fruits
            playerStats.UpdateTotalPoints(pointstoAdd);

            //update Blocker Position ----------TODO structuring and reduce referencing
            _stairSpawner.UpdateBlockerPosition(playerStats);


            //play fruit collection audio 
            fruit.PlayAudio();

            //Remove fruit from list
            _fruitSpawner.Fruits.Remove(fruit);

            ////Vanish Fruit
            //fruit.spriteRendrer.enabled = false;
            //Destroy(fruit.gameObject, 1f);
            fruit.VanishFruit();
        }
    }
    private void OnBuildStairRequested(StairEntity stair, GameObject playerGO)
    {
        var playerStats = GetPlayerStats(playerGO);

        if (playerStats.CapacityPoints >= 1)
        {
            //subtracting capacity point
            playerStats.CapacityPoints -=1;

            //updating stairs remaining
            //playerStats.UpdateStairRemaining(1);
            playerStats.StairsRemaining -= 1;

            //play stair created audio 
            stair.PlayAudio();

            //set is Created = true, so this stair will not be considered again if collided 
            stair.IsCreated = true;

            //Display Stair 
            stair.rendrer.enabled = true;

        }

    }

    private void OnPlayerReachedAtTargetPointRequested(GameObject playerGO,Transform goalTransform)
    {
        var playerStats = GetPlayerStats(playerGO);

        //Stop Camera Movement 
        OverviewCamera.IsMovementLock = true;

        //Tween  Camera to Render won player
        _overviewCamera.TweenCameraToTargetPosition(goalTransform);

        //Stop Player Movement
        PlayerMovement.isMovementLock = true;

        //Display player Crown and hide player points
        playerStats.IsPlayerWon = true;

        //Start player won animation

        // display game over UI
    }

    private PlayerStats GetPlayerStats(GameObject player)
    {
       return  _playersPool.FirstOrDefault(playerStat => playerStat.gameObject == player);
    }

}
