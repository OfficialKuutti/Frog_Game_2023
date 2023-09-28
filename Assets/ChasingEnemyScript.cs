using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ChasingEnemyScript : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float distanceToPlayer;
    private float distance;
    public float oldposition;

    
    

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        oldposition = transform.position.x;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        
        if (transform.position.x > oldposition)
        {
            GetComponentInChildren<SpriteRenderer>().flipY = false;
            oldposition = transform.position.x;
        }
        if (transform.position.x < oldposition)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
            GetComponentInChildren<SpriteRenderer>().flipY = true;
            oldposition = transform.position.x;
        }
        


    }
}
