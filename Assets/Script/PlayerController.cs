using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;
    public float climbSpeed;
    public float resetTime;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private Animator myAnim;
    private bool isGround;
    private bool canDoubleJump;
    private bool isOneWayPlatform;
    private float playerGravity;
    
    private bool isLadder;
    private bool isClimbing;
    private bool isJumping;
    private bool isFalling;
    private bool isDoubleJumping;
    private bool isDoubleFalling;
    

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myFeet = GetComponent<BoxCollider2D>();
        playerGravity = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            CheckAirState();
            Run();
            Flip();
            Jump();
            Climb();
            checkGround();
            CheckLadder();
            SwitchAnimation();
            OneWayPlatform();
        }
    }

    void checkGround()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))
                   || myFeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform"))
                   || myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myFeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
    }

    void CheckLadder()
    {
        isLadder = myFeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (myRigidbody.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Run()
    {
        float moveDir = Input.GetAxis("Horizontal");
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVel;
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnim.SetBool("Run", playerHasXAxisSpeed);
    }

    void Jump()
    {
        if (!Input.GetKey(KeyCode.DownArrow) && Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                //myAnim.SetFloat("ValX", h, xDampTime, Time.deltaTime);
                myAnim.SetBool("Jump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                myAnim.SetBool("DoubleJump", true);
                Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
                myRigidbody.velocity = Vector2.up * jumpVel;
            }
        }
    }

    void Climb()
    {
        float moveY = Input.GetAxis("Vertical");
        if(isClimbing)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, moveY * climbSpeed);
            canDoubleJump = false;
        }
        
        if (isLadder)
        {
            if (moveY > 0.5f || moveY < -0.5f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("Climb", true);
                myRigidbody.gravityScale = 0.0f;
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, climbSpeed * moveY);
            }
            else
            {
                if (isJumping || isFalling || isDoubleJumping || isDoubleFalling)
                {
                    myAnim.SetBool("Climb", false);
                }
                else
                {
                    myAnim.SetBool("Climb", false);
                    myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0.0f);
                }
            }
        }
        else
        {
            myAnim.SetBool("Climb", false);
            myRigidbody.gravityScale = playerGravity;
        }
        
        if (isLadder && isGround)
        {
            myRigidbody.gravityScale = playerGravity;
        }
        
        
    }

    void SwitchAnimation()
    {
        myAnim.SetBool("Idle", false);
        if (!Input.GetKey(KeyCode.DownArrow) && myAnim.GetBool("Jump") )
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("Jump", false);
                myAnim.SetBool("Fall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("Fall", false);
            myAnim.SetBool("Idle", true);
        }

        if (myAnim.GetBool("DoubleJump"))
        {
            if (myRigidbody.velocity.y < 0.0f)
            {
                myAnim.SetBool("DoubleJump", false);
                myAnim.SetBool("DoubleFall", true);
            }
        }
        else if (isGround)
        {
            myAnim.SetBool("DoubleFall", false);
            myAnim.SetBool("Idle", true);
        }
    }

    void OneWayPlatform()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        
        //float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && (Input.GetKey(KeyCode.DownArrow) && Input.GetButtonDown("Jump")))
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("ResetorPlayerLayer", resetTime);
        } else if (isOneWayPlatform && (Input.GetKey(KeyCode.DownArrow) && isLadder))
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("ResetorPlayerLayer", resetTime);
        }
    }

    void ResetorPlayerLayer()
    {
        if (!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    void CheckAirState()
    {
        isJumping = myAnim.GetBool("Jump");
        isFalling = myAnim.GetBool("Fall");
        isDoubleJumping = myAnim.GetBool("DoubleJump");
        isDoubleFalling = myAnim.GetBool("DoubleFall");
        isClimbing = myAnim.GetBool("Climb");
    }
}