using Unity.VisualScripting;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.down*speed, ForceMode2D.Impulse);
        if(transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }    
    }
}
