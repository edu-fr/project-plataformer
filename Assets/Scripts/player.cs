using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    //Horizontal Movement Variables                                                                                                       //misc
    private float moveInput;                                                                                                                    private Rigidbody2D rb;// Reference to the rigidbody2d
    public float speed;                                                                                                                              private Collider2D collider;

    //Vertical Movement Variables
    public LayerMask Ground;//this is the ground layer in unity
    public Transform groundCheck;//the groundCheck object is a child of player
    public float checkRadius;//the size of the check radius
    public float jumpSpeed;//speed of the jump
    public float jumpTime;//time you are in the air
    private bool isJumping;
    public float jumpForce;//total jump force
    public float fallSpeed;
    private float jumpTimeCounter;
    //public bool isGrounded;//the boolean to check if you are on thye ground
    private bool canJump;
    private float pressedJump;
    public float pressedJumpTime;
    public float groundRememberTime;
    private float groundRemember;

    //Enemy Variables
    public Transform enemyCheck;
    public LayerMask Enemy;
    public float enemyJumpSmall;
    public float enemyJumpBig;
    public float enemyCheckRadius;
    private bool enemyChecker;
    private bool isOnEnemy;

    //animation
    public Animator anim;
    public hungry hungry;
    public int maxHungry = 100;
    public int currentHungry;
    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      collider = GetComponent<Collider2D>();
      currentHungry = maxHungry;
      hungry.setMaxHealth(maxHungry);
    //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      Move();//call the move function


    }
    void Update()
    {
      Jump();//Call the jump function
    }

    void Move()
    {
      moveInput = Input.GetAxisRaw("Horizontal");
      if(moveInput == 1 || moveInput == -1)
      {
        isMoving = true;

      }
      else
      {
       isMoving = false;
      }
      if(isMoving)
      {
    //    anim.SetBool("isRunning", true);
      }
      else
      {
    //    anim.SetBool("isRunning", false);
      }
      if(moveInput < 0)
        {
          transform.eulerAngles = new Vector3(0, 180, 0);//if Player input is left, makes them face left
        }

      else if(moveInput > 0)
        {
          transform.eulerAngles = new Vector3(0, 0, 0);//if player input is right makes them face right
        }

      //transform.Translate(new Vector2(moveInput, 0) * speed * Time.deltaTime);//Move left and right
      rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }


  void Jump()
  {
  bool  isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
  groundRemember -= Time.deltaTime;
  if(isGrounded)
  {
    groundRemember = groundRememberTime;
  }
pressedJump -= Time.deltaTime;
if(Input.GetKeyDown(KeyCode.Space))
{
  pressedJump = pressedJumpTime;
}

    if((groundRemember > 0) && (pressedJump > 0))
    {
      pressedJump = 0;
      groundRemember = 0;
      rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    if(Input.GetKeyUp(KeyCode.Space))
      {
      if(rb.velocity.y > 0)
        {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallSpeed);
        }
      }
    }

    public void HungryBar()
    {
      if(true)
      {
      currentHungry -= 1;
      hungry.SetHealth(currentHungry);
    }
    }
  }
