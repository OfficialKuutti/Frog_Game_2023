using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public Rigidbody2D myRB;
    public float speed = 2f;
    public float speedX = 2f;
    public float speedY = 2f;
    private float oldposition;
    public delegate void CollectEvent();
    public event CollectEvent OnCollected;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        oldposition = transform.position.x;
    }

    // FixedUpdate with rigidbody, because player and camera uses it too
    void FixedUpdate()
    {
        myRB.velocity = new Vector2(speedX, speedY);

        if (transform.position.x > oldposition)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
            oldposition = transform.position.x;
        }
        if (transform.position.x < oldposition)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            oldposition = transform.position.x;
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bumper"))
        {
            speedX = -speedX;
            speedY = -speedY;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Disable the collectable object
            gameObject.SetActive(false);

            // Invoke the collect event
            OnCollected?.Invoke();
        }
    }



}