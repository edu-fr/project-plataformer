using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
  public Rigidbody2D rb;
  public float speed;
  //[Header("Ground Check Variables")]
  public bool isGrounded;//Boolean. if is on ground sets to true
  public float checkRadius;//decides how large the radius of the ground posiiton
  public LayerMask whatIsGround;//references the ground layer
  public Transform groundPos;//This is the empty game object attached to the player, which checks for Ground

  //[Header("Jumping Variables")]
  private float jumpTimeCounter;//What does this variable do lol
  private bool doubleJump;//False by default, is true if isGrounded is false and isJumping is false
  private bool isJumping;//False by default, sets to true if Z is input
  public float jumpForce;//how much force goes in the jump
  public float jumpTime;//How much time you are in the air
  void Update()
  {
    Jump();
    Walk();
  }
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
  public void Jump()
  {
    isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);//checks for ground

    if(isGrounded == true && Input.GetKeyDown(KeyCode.Z))//Checks to see if is inputing Z, and is geounded, before allowing a jump
    {
      jumpTimeCounter = jumpTime;//times the jump
      rb.velocity = Vector2.up * jumpForce;//This is the actual jump
    }

    if(isGrounded == true)//Checks to see if isGrounded
    {
      //isJumping = false;//Sets jumping to false
      doubleJump = false;//Double jump is false
    }

    if(isGrounded == false && doubleJump == false && Input.GetKeyDown(KeyCode.Z))//If you aren't grounded, and are inputing Z, and also havent double jumped yet
    {
      doubleJump = true;//sets double jump to true, so you can't jump again until grounded
      jumpTimeCounter = jumpTime;//times the jump
      rb.velocity = Vector2.up * jumpForce;//This is the jump
    }

  }
}
