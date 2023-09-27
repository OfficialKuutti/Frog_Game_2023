using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody2D myRB;
    public float speed = 2f;
    public float speedX = 2f;
    public float speedY = 2f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        //speedX = speed;
        //speedY = speed;
        
    }

    // FixedUpdate with rigidbody, because player and camera uses it too
    void FixedUpdate()
    {
        myRB.velocity = new Vector2(speedX, speedY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bumper"))
        {
            speedX = -speedX;
            speedY = -speedY;
        }
    }
}
