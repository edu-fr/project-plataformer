using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
  public Rigidbody2D rb;
  public float speed;
  void Update()
  {
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
}
