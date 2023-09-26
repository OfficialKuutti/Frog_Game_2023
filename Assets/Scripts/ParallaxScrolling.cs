using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField] public float speedX = 0.5f;
    [SerializeField] public float speedY = 0.2f;
    [SerializeField] private Transform cameraPosition;

    public float spriteWidth;
    public float startXPos, startYPos;

    // Start is called before the first frame update
    void Start()
    {
        startXPos = transform.position.x;
        startYPos = transform.position.y;
        cameraPosition = Camera.main.transform;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceX = cameraPosition.position.x * speedX;
        float distanceY = cameraPosition.position.y * speedY;
        transform.position = new Vector3(startXPos + distanceX, startYPos + distanceY, transform.position.z);

        float temp = (cameraPosition.transform.position.x * (1 - speedX));

        if (temp > startXPos + spriteWidth)
        {
            startXPos += spriteWidth;
        }

        else if (temp < startXPos - spriteWidth)
        {
            startXPos -= spriteWidth;
        }
        
    }
}
