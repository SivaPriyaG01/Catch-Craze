using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D playerRb;
    private Vector2 moveInput;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRb.AddForce(moveInput*moveSpeed);
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
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
