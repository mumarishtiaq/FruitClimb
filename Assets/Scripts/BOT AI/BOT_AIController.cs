using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BOT_AIController : MonoBehaviour
{
    #region OldWork
    NavMeshAgent agent;
   public  bool MoveToNextPoint = false;
    


    public GameObject blueFirstStair;
   public Vector3 posNew;
    public List<float> distances = new List<float>();
    public int minDIstIndex;
    #endregion OldWork

    [Space(30)]
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private PlayerStats playerStats;

    public StairsSpawner spawner;
    public FruitSpawner fruitSpawner;
    //public NavMeshSurface surface;

    public bool isInTransitOfBuildingStairs = false;
    public GameObject goalPosition;

  

    private void Start()
    {
        #region OldWork
        agent = GetComponent<NavMeshAgent>();
        
        GetNearestPoint();

        blueFirstStair = ScriptReference.instance.tilesInstatioator.BlueFirstStair;

        posNew = blueFirstStair.transform.position;
        #endregion OldWork
        playerStats = GetComponent<PlayerStats>();
        //surface.BuildNavMesh();

    }

    public static bool IsBotSleeping = true;
    private void Update()
    {
        if(IsBotSleeping)
        {
            //agent.ResetPath();
            agent.isStopped = true;
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













    void GetNearestPoint()
    {
        if (ScriptReference.instance.tilesInstatioator.AllBlueSpheres.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < ScriptReference.instance.tilesInstatioator.AllBlueSpheres.Count; i++)
        {
            
            distances.Add(Vector3.Distance(ScriptReference.instance.tilesInstatioator.AllBlueSpheres[i].transform.position, transform.position));
        }
       
            minDIstIndex = distances.IndexOf(distances.Min());
        
       
    }


   
   
    void BotNavigation()
    {

       
        if(ScriptReference.instance.tilesInstatioator.AllBlueSpheres.Count<=0)
        {
            return;
        }


        if (ScriptReference.instance.BotscoreManager.TempPointsCollected >= 10)
        {
            CreateStairs();
        }

        else
        {


            Vector3 pos = new Vector3(ScriptReference.instance.tilesInstatioator.AllBlueSpheres[minDIstIndex].
                                        transform.position.x,
                                        transform.position.y,
                                        ScriptReference.instance.tilesInstatioator.AllBlueSpheres[minDIstIndex].
                                        transform.position.z);

            agent.SetDestination(pos);
            ///jfhufif
            /*if(agent.SetDestination(pos)==false)
            {
                agent.SetDestination(ScriptReference.instance.tilesInstatioator.AllBlueSpheres[minDIstIndex].transform.position);
            }*/
        }
         
    }


    void CreateStairs()
    {
        

        if(gameObject.transform.position.x == posNew.x && gameObject.transform.position.z == posNew.z)
        {
            agent.SetDestination(ScriptReference.instance.tilesInstatioator.allBlueStairs[5].transform.position);
            Debug.Log("moving to stair 5");
        }

        else
        {
            agent.SetDestination(posNew);
            Debug.Log("AT first tile;");
        }
    }


    

}
               

               
                
       
