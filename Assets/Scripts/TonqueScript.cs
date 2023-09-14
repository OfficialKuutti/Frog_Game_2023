using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TonqueScript : MonoBehaviour
{
    public float tongueSpeed = 2;
    public PlayerController pc;
    public GameObject tongue;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.facingRight)
        {
            transform.Translate(Vector2.right * tongueSpeed * Time.deltaTime);
        }

        if (!pc.facingRight)
        {
            transform.Translate(Vector2.left * tongueSpeed * Time.deltaTime);
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }


    }
}

