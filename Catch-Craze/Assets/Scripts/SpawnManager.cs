using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] fallingObjects;
    public float xSpawnRange;
    public float ySpawnPos;
    private bool isSpawning = false; // Track if spawning is already started

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Check if gameStart is true and spawning hasn't started
        if (gameManager.gameStart == true && gameManager.gameOver == false && !isSpawning)
        {
            StartSpawning();
        }

        // Stop spawning if gameOver becomes true
        if (gameManager.gameOver == true && isSpawning)
        {
            StopSpawning();
        }
    }

    void StartSpawning()
    {
        isSpawning = true;
        InvokeRepeating("SpawnRandomObjects", 1, Random.Range(1, 2)); // Start spawning objects
    }

    void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnRandomObjects"); // Stop spawning objects
    }
    void SpawnRandomObjects()
    {
        float randomSpawnPosX= Random.Range(-xSpawnRange,xSpawnRange); //Get a random x position
        int index = Random.Range(0, fallingObjects.Length); //selecting s random falling object
        Vector2 spawnPos = new Vector2(randomSpawnPosX, ySpawnPos); // creating a random poition to spawn
        
        Instantiate(fallingObjects[index],spawnPos,fallingObjects[index].transform.rotation);
    }
}
