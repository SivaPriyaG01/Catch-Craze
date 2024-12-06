using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Fruits"))
        {
            int points = other.gameObject.GetComponent<FallDown>().points;
            gameManager.UpdateScore(points);
            Debug.Log("Collided with fruit");
        }
        else if(other.gameObject.CompareTag("Bomb"))
        {
            gameManager.UpdateLives(1);
            Debug.Log("Collided with Bomb");
        }
    }
}
