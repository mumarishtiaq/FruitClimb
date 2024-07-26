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
    public NavMeshSurface surface;

    private bool isInTransitOfBuildingStairs = false;

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
    private void Update()
    {
        #region OldWork
        //if (agent.hasPath == false)
        //{
        //    //MoveToNextPoint = true;
        //    if (agent.remainingDistance <= agent.stoppingDistance)
        //    {


        //        distances.Clear();

        //        GetNearestPoint();
        //        BotNavigation();



        //    }

        //}
        #endregion OldWork



        if (playerStats.CapacityPoints < playerStats.MaximumCapacity && !isInTransitOfBuildingStairs)
            CollectFruits();

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

        if (!isInTransitOfBuildingStairs)
        {
            isInTransitOfBuildingStairs = true;
            agent.SetDestination(spawner.BlueStairs[playerStats.TotalPointsCollected].transform.position);
        }

        //if(transform.position ==)
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
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
               

               
                
       
