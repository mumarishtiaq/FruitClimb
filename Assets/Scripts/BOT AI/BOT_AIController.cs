using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BOT_AIController : MonoBehaviour
{
    
    NavMeshAgent agent;
   public  bool MoveToNextPoint = false;


    public GameObject blueFirstStair;
   public Vector3 posNew;
    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        GetNearestPoint();

        blueFirstStair = ScriptReference.instance.tilesInstatioator.BlueFirstStair;

        posNew = blueFirstStair.transform.position;

    }
   
    private void Update()
    {
        if (agent.hasPath == false)
        {
            //MoveToNextPoint = true;
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                
                
                distances.Clear();
                
                GetNearestPoint();
                BotNavigation();

               

            }
           
        }

        





    }

    
     public List<float> distances = new List<float>();
    public int minDIstIndex;
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
               

               
                
       
