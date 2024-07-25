using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public FruitTemplate[] fruitTemplates; 
    public GameObject fruitPrefab; 
    public float spawnInterval = 2f; 
    public GameObject Ground;

    public Transform fruitsHolder;
    public List<FruitEntity> fruits;

    private float timer;

    float minX, maxX, minZ, maxZ;

    private void Awake()
    {
        fruits = new List<FruitEntity>();
        GetSpawningArea();
        for (int i = 0; i < MatchSettings.MatchLenght/2; i++)
        {
            SpawnFruit(true);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnFruit();
            timer = 0f;
        }
    }

    [ContextMenu("Check Fruit Spawn")]

    private void CheckFruitSpawn()
    {
        GetSpawningArea();
        for (int i = 0; i < 25; i++)
        {
            SpawnFruit();
        }
    }
    [ContextMenu("Destroy")]

    private void DestroyFruits()
    {
        
        for (int i = 0; i < fruitsHolder.childCount; i++)
        {
            DestroyImmediate(fruitsHolder.GetChild(i));
        }
    }

    private void GetSpawningArea()
    {
       var  groudCollider = Ground.GetComponent<Collider>();
        minX = groudCollider.bounds.min.x + 0.7f;
        maxX = groudCollider.bounds.max.x - 0.7f;
        minZ = groudCollider.bounds.min.z + 0.7f;
        maxZ = groudCollider.bounds.max.z - 0.7f;
    }
    
    void SpawnFruit(bool inOnAwake = false)
    {
        FruitTemplate selectedFruit = GetRandomFruit();
        Vector3 spawnPosition = GetRandomPosition(inOnAwake);
        GameObject fruitInstance = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity,fruitsHolder);
        fruitInstance.name = selectedFruit.name;

        var fruitEntity = fruitInstance.GetComponent<FruitEntity>();
        var spriteRendrer = fruitInstance.GetComponent<SpriteRenderer>();
        spriteRendrer.sprite = selectedFruit.sprite;
        fruitEntity.spriteRendrer = spriteRendrer;
        fruitEntity.template = selectedFruit;
        
        fruits.Add(fruitEntity);
    }

    private FruitTemplate GetRandomFruit()
    {
        // Calculate total weight using the inverse of points
        float totalWeight = 0f;
        foreach (FruitTemplate fruit in fruitTemplates)
        {
            totalWeight += 1f / fruit.points;
        }

        // Get a random value
        float randomValue = Random.Range(0f, totalWeight);

        // Select fruit based on the random value
        float cumulativeWeight = 0f;
        foreach (FruitTemplate fruit in fruitTemplates)
        {
            cumulativeWeight += 1f / fruit.points;
            if (randomValue < cumulativeWeight)
            {
                return fruit;
            }
        }

        // Fallback in case something goes wrong
        return fruitTemplates[0];
    }

    Vector3 GetRandomPosition(bool isStart = false)
    {
        var yOffset = isStart ? 4f : 20;
        return new Vector3(Random.Range(minX, maxX), Ground.transform.position.y + yOffset, Random.Range(minZ, maxZ));
    }
}
