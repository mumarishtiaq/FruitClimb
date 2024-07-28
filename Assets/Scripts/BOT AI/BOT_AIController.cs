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
    public NavMeshSurface surface;

    public bool isInTransitOfBuildingStairs = false;

    private void Start()
    {
        #region OldWork
        agent = GetComponent<NavMeshAgent>();
        
        GetNearestPoint();

        blueFirstStair = ScriptReference.instance.tilesInstatioator.BlueFirstStair;

        posNew = blueFirstStair.transform.position;
        #endregion OldWork
        playerStats = GetComponent<PlayerStats>();
        surface.BuildNavMesh();

    }
    private Vector3 currentStairDestination;

    public bool isBotSleeping = true;
    private void Update()
    {
        if(isBotSleeping)
        {
            agent.ResetPath();
            agent.isStopped = true;
            return;
        }

        agent.isStopped = false;


        if (playerStats.CapacityPoints < playerStats.MaximumCapacity && !isInTransitOfBuildingStairs)
        {
            //CollectFruits();
            CollectFruitsNewApproach();
        }


        else
            BuildStairs();


    }

    public Collider[] publicColliders;
    void CollectFruits()
    {
        Debug.Log("Bot in Collecting Fruit");
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        publicColliders = hitColliders;
        Transform closestFruit = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Fruit"))
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                closestDistance = distance;
                closestFruit = hitCollider.transform;
            }
        }

        if (closestFruit != null)
        {
            agent.SetDestination(closestFruit.position);
        }
    }

    private void BuildStairs()
    {
        Debug.Log("Aheading towards build stairs");
        var targetStairIndex = Mathf.Min(playerStats.TotalPointsCollected - 1, spawner.BlueStairs.Count -1);
        if (spawner.BlueStairs.Count > targetStairIndex)
        {
            var targetStair = spawner.BlueStairs[targetStairIndex].transform.position;

            if (!isInTransitOfBuildingStairs)
            {
                isInTransitOfBuildingStairs = true;
                agent.SetDestination(targetStair);
            }
        }
        


        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isInTransitOfBuildingStairs = false;
            //agent.SetDestination(spawner.BlueStairs[0].transform.position);
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
               

               
                
       
