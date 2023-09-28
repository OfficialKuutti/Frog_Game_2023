using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float storedSpeed;
    public float platformSpeed;
    public float walkingSpeed = 2f;
    public float airSpeed = 8f;
    public float jumpPower = 10f;
    private float horizontal;
    public float vertical;
    public Rigidbody2D myRB;
    public Animator myAnim;
    public bool facingRight = true;
    public bool onMovingPlatform = false;

    public bool onJumpThruPlatform = false;
    public GameObject activeJumpThruPlatform;


    //Variables for groundcheck! Player cant jump until it is ground LAYER
    public float groundCheckRadius = 0.1f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //References to another scripts:
    [SerializeField] private CameraTargetScript cameraTargetScript;
    [SerializeField] private CameraController camController;

    //Player death values
    public Transform playerStart;

    //Moving platforms physicmaterials etc.
    public Collider2D myCol;
    public PhysicsMaterial2D slide;
    public PhysicsMaterial2D stop;

    //Attack
    public GameObject tongue;
    public Animator tongueanim;

   
    

    

     public void Start()
    {
        cameraTargetScript = GameObject.Find("CameraTarget").GetComponent<CameraTargetScript>();
        camController = GameObject.Find("Basic 2D Camera").GetComponent<CameraController>();
        transform.position = playerStart.position;
        myCol = GetComponent<BoxCollider2D>();
        storedSpeed = speed;
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

        
    //This calls JUMP inputs on Rigidbody AddForce from Unity Input system Editor
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded()) 
        {
            myRB.AddForce(Vector2.up * jumpPower);
            
        }

        if(context.canceled && myRB.velocity.y > 0f) 
        {
            myRB.velocity = new Vector2(myRB.velocity.x, myRB.velocity.y * 0.6f);
        }
    }

    
    
    //This calls MOVE Inputs on X axis from Unity Input system editor
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            tongueanim.Play("Tongue");
            Instantiate(tongue, transform.position, transform.rotation);
        }    
            
        
        
        
    }
    public void Flip() 
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
        //camController.FlipScreenX(facingRight);
    }

    private void FixedUpdate()
    {
        myRB.velocity = new Vector2(horizontal * speed, myRB.velocity.y);
        
        myAnim.SetFloat("yVelocity", myRB.velocity.y);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (horizontal < -0.1f && facingRight)
        {
            Flip();
        }

        if (horizontal > 0.1f && !facingRight)
        {
            Flip();
        }

        if (horizontal != 0f)
        {
            myAnim.SetBool("isWalking", true);
        }

        else if (horizontal == 0f)
        {
            myAnim.SetBool("isWalking", false);
        }

        if (IsGrounded())
        {
            myAnim.SetBool("isGrounded", true);
            cameraTargetScript.posY = transform.position.y;

            if (onMovingPlatform)
            {
                speed = platformSpeed;
            }

            if (!onMovingPlatform)
            {
                speed = walkingSpeed;
            }
                  

        }
        else
        {
            myAnim.SetBool("isGrounded", false);
            speed = airSpeed;
        }

        if (horizontal != 0f && onMovingPlatform) 
        {
            myCol.sharedMaterial = slide;
        }

        if (horizontal == 0f && onMovingPlatform)
        {
            myCol.sharedMaterial = stop;
        }

        if (vertical < -0.1f && onJumpThruPlatform)
        {
            activeJumpThruPlatform.GetComponent<JumpThruPlatform>().StartCoroutine("DropThruPlatform");
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //camController.ScreenShake();
            transform.position = playerStart.position;
        }

        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            //transform.parent = collision.gameObject.transform;
            myCol.sharedMaterial = stop;
            onMovingPlatform = true;

            if(collision.gameObject.GetComponent<MovingPlatform>().speed == 0f)
            {
                return;
            }

            else if (collision.gameObject.GetComponent<MovingPlatform>().speed != 0f)
            {
                platformSpeed = collision.gameObject.GetComponent<MovingPlatform>().speed * 2f;
            }
            

        }
        
        if (collision.gameObject.CompareTag("JumpThruPlatform"))
        {
            onJumpThruPlatform = true;
            activeJumpThruPlatform = collision.gameObject;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            //transform.parent = null;
            myCol.sharedMaterial = slide;
            onMovingPlatform = false;
            speed = storedSpeed;
        }

        if (collision.gameObject.CompareTag("JumpThruPlatform"))
        {
            onJumpThruPlatform = false;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            playerStart.position = collision.transform.position;
        }

        if(collision.gameObject.name == "MusicZone2")
        {
            Camera.main.GetComponent<AudioManager>().musicpoint1 = true;
        }

        if (collision.gameObject.name == "MusicZone3")
        {
            Camera.main.GetComponent<AudioManager>().musicpoint2 = true;
        }

        if (collision.gameObject.name == "MusicZone4")
        {
            Camera.main.GetComponent<AudioManager>().musicpoint2 = true;
        }

    }
}
