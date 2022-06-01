using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
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
    private bool canJump;
    private float pressedJump;
    public float pressedJumpTime;
    public float groundRememberTime;
    private float groundRemember;

  void Update()
  {
    Jump();
    Walk();
  }

  //This is the code that makes the player move horizontally
  public void Walk()
  {
    float moveInput = Input.GetAxisRaw("Horizontal");//checks for input on the arrow keys
    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);//moves left or right based on input

    if(moveInput < 0)
      {
        transform.eulerAngles = new Vector3(0, 0, 0);//if Player input is left, makes them face left
      }

    else if(moveInput > 0)
      {
        transform.eulerAngles = new Vector3(0, 180, 0);//if player input is right makes them face right
      }
  }

//This is the code which makes the character move vertically
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
}
