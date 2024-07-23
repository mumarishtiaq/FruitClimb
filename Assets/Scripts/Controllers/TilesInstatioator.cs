using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TilesInstatioator : MonoBehaviour
{

    #region Vars
    public GameObject[] TilesList;

    public GameObject Ground;

    public NavMeshSurface surface;
    

    float minX, maxX, minZ, maxZ;
    Collider groudCollider;

     float radius ;
    public LayerMask groundLayer;

    [Space(10)]
   
   
    [SerializeField] int sphereToSpawn,TotalSphereSpawned, greenSpheresCount,blueSphereCount;

    float x, y;
    [Space(10)]
    [SerializeField] GameObject GreenFirstStair;
    [Space(10)]
    public GameObject BlueFirstStair;
    [Space(10)]
    [SerializeField] GameObject GreenStairPrefab;
    [Space(10)]
    [SerializeField] GameObject BlueStairPrefab;

     List<GameObject> AllStairsGreen = new List<GameObject>();

    [Space(10)]
    [SerializeField] GameObject Blocker;

    GameObject LastStair;

    #endregion

    #region Unity Events


    private void Awake()
    {
        groudCollider = Ground.GetComponent<Collider>();
        minX = groudCollider.bounds.min.x + 0.7f;
        maxX = groudCollider.bounds.max.x - 0.7f;
        minZ = groudCollider.bounds.min.z + 0.7f;
        maxZ = groudCollider.bounds.max.z - 0.7f;



        InstantiateTiles();
        GetAllChilds();
        //InstantiateGreenStairs();
        //InstantiateBlueStairs();
        AllStairsGreen = InstantiateStairs(GreenFirstStair.transform, GreenStairPrefab);
        allBlueStairs = InstantiateStairs(BlueFirstStair.transform, BlueStairPrefab);
        surface.BuildNavMesh();// re-baking after instantiation--- 
        /*foreach (var item in allBlueStairs)
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }*/
       
    }
   
    private void Update()
    {
        if (ScriptReference.instance.pLayerscoreManager.PointsCollected > 0)
        {
            LastStair = AllStairsGreen[ScriptReference.instance.pLayerscoreManager.PointsCollected - 1];

            Blocker.transform.position =
           new Vector3(LastStair.transform.position.x + 0.38f, LastStair.transform.position.y + 0.7f, LastStair.transform.position.z);

        }
    }

    #endregion

    #region Custom Methods

    #region Instantiate Spheres
    void InstantiateTiles()
    {


        
        for (int i = 0; i < sphereToSpawn; i++)
        {
            radius = 1f;
            Vector3 spawnpos = new Vector3(Random.Range(minX, maxX), Ground.transform.position.y + 0.98f, Random.Range(minZ, maxZ));


            if (Checker(spawnpos) < 1)
            {
               // Debug.Log("can SpawnHere");
                GameObject obj = Instantiate(TilesList[Random.Range(0, TilesList.Length)], gameObject.transform);
                obj.transform.position = spawnpos;
                obj.transform.rotation = Quaternion.identity;
            }
            else
            {
                //Debug.Log("cant SpawnHere");
            }





        }

    }


    int Checker(Vector3 pos)
    {
        //radius = 2f;
        Collider[] allColliders = Physics.OverlapSphere(pos,radius,~(groundLayer));
       // Debug.Log("allColliders.Length" + allColliders.Length);
        return allColliders.Length;
        
    }

    #endregion

    #region Getting Spheres

    void GetAllChilds()
    {
        foreach (var item in transform)
        {
            TotalSphereSpawned++;
        }
        GetGreenSpheres();
        GetBlueSpheres();
    }

    void GetGreenSpheres()
    {
        GameObject[] green = GameObject.FindGameObjectsWithTag("GreenSphere");
        greenSpheresCount = green.Length;
    }

    public List<GameObject> AllBlueSpheres = new List<GameObject>();
    void GetBlueSpheres()
    {
        GameObject[] blue = GameObject.FindGameObjectsWithTag("BlueSphere");
        blueSphereCount = blue.Length;
        for (int i = 0; i < blue.Length; i++)
        {
            AllBlueSpheres.Add(blue[i]);
        }
    }

    #endregion


    #region Stairs Instantiation
    //------------Instantiate Green Stairs-------------
    void InstantiateGreenStairs()
    {
        Vector3 firstTilePos = GreenFirstStair.transform.position;
        Quaternion firstTileRot = GreenFirstStair.transform.rotation;

       

        x = firstTilePos.x;
        y = firstTilePos.y;
        
        for (int i = 0; i < greenSpheresCount; i++)
        {
            x += 1.350f;
            y += 0.450f;
            GameObject obj = Instantiate(GreenStairPrefab);
            obj.transform.position = new Vector3(x, y, firstTilePos.z);
            obj.transform.rotation = firstTileRot;
            obj.GetComponent<MeshRenderer>().enabled = false;
            AllStairsGreen.Add(obj);
        }
       
    }

    //------------Instantiate Blue Stairs-------------
    void InstantiateBlueStairs()
    {
        Vector3 firstTilePos = BlueFirstStair.transform.position;
        Quaternion firstTileRot = BlueFirstStair.transform.rotation;



        x = firstTilePos.x;
        y = firstTilePos.y;

        for (int i = 0; i < blueSphereCount; i++)
        {
            x += 1.350f;
            y += 0.450f;
            GameObject obj = Instantiate(BlueStairPrefab);
            obj.transform.position = new Vector3(x, y, firstTilePos.z);
            obj.transform.rotation = firstTileRot;

            //obj.GetComponent<MeshRenderer>().enabled = false;
            allBlueStairs.Add(obj);
            
        }
    }

    public List<GameObject> allBlueStairs = new List<GameObject>();

    private List<GameObject> InstantiateStairs(Transform firstTile,GameObject stairPrefab)
    {
        Vector3 firstTilePos = firstTile.position;
        Quaternion firstTileRot = firstTile.rotation;

        List<GameObject> stairs = new List<GameObject>();

        x = firstTilePos.x;
        y = firstTilePos.y;

        for (int i = 0; i < blueSphereCount; i++)
        {
            x += 1.350f;
            y += 0.450f;
            GameObject obj = Instantiate(stairPrefab);
            obj.transform.position = new Vector3(x, y, firstTilePos.z);
            obj.transform.rotation = firstTileRot;

            obj.GetComponent<MeshRenderer>().enabled = false;
            stairs.Add(obj);
        }
        return stairs;
    }

    #endregion


    #region TestMethods
    public void InstantiateAgain()
    {

        TotalSphereSpawned = 0;
        InstantiateTiles();
        GetAllChilds();

    }
    public void DestroyAllChilds()
    {
        foreach (Transform item in transform)
        {
            Destroy(item.gameObject);
        }
        TotalSphereSpawned = 0;

    }
    #endregion

    #endregion
}
