using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BOT_AIController : MonoBehaviour
{
    #region OldWork








    public int minDIstIndex;

    #endregion OldWork

    NavMeshAgent agent;
    [Space(30)]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private PlayerStats playerStats;

    public StairsSpawner spawner;
    public FruitSpawner fruitSpawner;
    //public NavMeshSurface surface;

    public bool isInTransitOfBuildingStairs = false;
    public GameObject goalPosition;

  

  
    private void Awake()
    {
        playerStats = GetComponentInChildren<PlayerStats>();
        agent = GetComponent<NavMeshAgent>();
    }

    public static bool IsBotSleeping = true;
    private void Update()
    {
        if(IsBotSleeping)
        {
            //agent.ResetPath();
            //agent.isStopped = true;
            return;
        }

        agent.isStopped = false;

        if (playerStats.CapacityPoints < playerStats.MaximumCapacity && !isInTransitOfBuildingStairs &&
            playerStats.CapacityPoints <= playerStats.StairsRemaining)
        {
                CollectFruitsNewApproach();
        }

        else
            BuildStairs();


    }

   

    private void BuildStairs()
    {
        Debug.Log("Aheading towards build stairs");


        if (!isInTransitOfBuildingStairs)
        {
            var targetStairIndex = Mathf.Min(playerStats.TotalPointsCollected - 1, spawner.BlueStairs.Count - 1);
            var targetStair = spawner.BlueStairs[targetStairIndex].transform.position;
            isInTransitOfBuildingStairs = true;

            if(targetStairIndex == (spawner.BlueStairs.Count-1))
            {
                agent.SetDestination(goalPosition.transform.position);
                Debug.Log("in goal Position");
            }
            else
            {
                agent.SetDestination(targetStair);
                Debug.Log("in target stair");
            }
            
            //Debug.Log("in setting destination");
        }




        if (agent.remainingDistance <= agent.stoppingDistance )
        {
            isInTransitOfBuildingStairs = false;
        }
            
        

       
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void CollectFruitsNewApproach()
    {
        // Get the nearest Fruit

        
        Debug.Log("In Collecting fruit");
        if(agent.remainingDistance <= agent.stoppingDistance && !agent.hasPath && fruitSpawner.Fruits.Count>0)
        {
            var nearestFruit = FindNearestFruit();
            Debug.Log($"Agent path : {agent.hasPath}");
            if (nearestFruit != null)
            {
                agent.SetDestination(nearestFruit.transform.position);
            }
        }

    }
    private FruitEntity FindNearestFruit()
    {
        Debug.Log("In Finding nearest point");
        FruitEntity nearestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (FruitEntity fruit in fruitSpawner.Fruits)
        {
            float distance = Vector3.Distance(transform.position, fruit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestObject = fruit;
            }
        }

        return nearestObject;
    }
}
               

               
                
       
