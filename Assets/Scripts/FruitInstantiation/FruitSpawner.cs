using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public FruitTemplate[] fruitTemplates; // Array of fruit templates
    public GameObject fruitPrefab; // Prefab to instantiate
    public float spawnInterval = 2f; // Time interval between spawns
    public Vector2 planeBounds; // X and Y bounds of the plane

    private float timer;

    //void Update()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= spawnInterval)
    //    {
    //        SpawnFruit();
    //        timer = 0f;
    //    }
    //}

    [ContextMenu("Check Fruit Spawn")]

    private void CheckFruitSpawn()
    {
        for (int i = 0; i < 25; i++)
        {

        }
    }
    
    void SpawnFruit()
    {
        FruitTemplate selectedFruit = GetRandomFruit();
        Vector2 spawnPosition = GetRandomPosition();
        GameObject fruitInstance = Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
        // Set the fruit properties (e.g., sprite, color)
        fruitInstance.GetComponent<SpriteRenderer>().sprite = selectedFruit.sprite;
        fruitInstance.GetComponent<SpriteRenderer>().color = selectedFruit.color;
        // You can add more properties here as needed
    }

    FruitTemplate GetRandomFruit()
    {
        // Calculate total weight
        int totalWeight = 0;
        foreach (FruitTemplate fruit in fruitTemplates)
        {
            totalWeight += fruit.points;
        }

        // Get a random value
        int randomValue = Random.Range(0, totalWeight);

        // Select fruit based on random value
        int cumulativeWeight = 0;
        foreach (FruitTemplate fruit in fruitTemplates)
        {
            cumulativeWeight += fruit.points;
            if (randomValue < cumulativeWeight)
            {
                return fruit;
            }
        }

        // Fallback in case something goes wrong
        return fruitTemplates[0];
    }

    Vector2 GetRandomPosition()
    {
        float x = Random.Range(-planeBounds.x / 2, planeBounds.x / 2);
        float y = Random.Range(-planeBounds.y / 2, planeBounds.y / 2);
        return new Vector2(x, y);
    }
}
