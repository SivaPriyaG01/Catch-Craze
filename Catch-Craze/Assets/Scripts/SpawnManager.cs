using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] fallingObjects;
    public float xSpawnRange;
    public float ySpawnPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager.gameOver==true)
        {
            InvokeRepeating("SpawnRandomObjects",2,Random.Range(1,2));
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnRandomObjects()
    {
        float randomSpawnPosX= Random.Range(-xSpawnRange,xSpawnRange); //Get a random x position
        int index = Random.Range(0, fallingObjects.Length); //selecting s random falling object
        Vector2 spawnPos = new Vector2(randomSpawnPosX, ySpawnPos); // creating a random poition to spawn
        
        Instantiate(fallingObjects[index],spawnPos,fallingObjects[index].transform.rotation);
    }
}
