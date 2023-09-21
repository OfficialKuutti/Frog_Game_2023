using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetScript : MonoBehaviour
{

    public Transform target;

    public float posY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Can use LateUpdate or FixedUpdate, depends your player moving style
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y , transform.position.z);
    }
}
