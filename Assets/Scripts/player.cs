﻿using UnityEngine;

namespace ProjcetPlataformer
{
    public class Player : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public BoxCollider2D boxCollider;
        public bool isRunning; 
        public float speed;
        //[Header("Ground Check Variables")]
        public bool isGrounded;//Boolean. if is on ground sets to true
        public bool isFalling; // Boolean. If isn't on ground and his rb.velocity.y is negative, it's falling
        public float checkRadius;//decides how large the radius of the ground posiiton
        public LayerMask whatIsGround;//references the ground layer
        public Transform groundPos;//This is the empty game object attached to the player, which checks for Ground

        //[Header("Jumping Variables")]
        private float _jumpTimeCounter;//What does this variable do lol
        private bool _doubleJump;//False by default, is true if isGrounded is false and isJumping is false
        private bool _isJumping;//False by default, sets to true if Z is input
        public float jumpForce;//how much force goes in the jump
        public float jumpTime;//How much time you are in the air

        void Update()
        {
            if (!isRunning)
            {
                StartRunning();
                return;
            }
       
            Run();
            Jump();
            CrossPlataform();
        }

        public void StartRunning()
        {
            if (!isRunning) 
                if (Input.GetKeyDown(KeyCode.Space)) 
                    isRunning = true;
        }

        public void Run()
        {
            if (!isRunning) return;
            rigidbody.velocity = new Vector2(speed, rigidbody.velocity.y);
        }

        public void Jump()
        {
            isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);//checks for ground

            if (isGrounded && Input.GetKeyDown(KeyCode.Z))//Checks to see if is inputing Z, and is geounded, before allowing a jump
            {
                _jumpTimeCounter = jumpTime;//times the jump
                rigidbody.velocity = Vector2.up * jumpForce;//This is the actual jump
            }

            if (isGrounded)//Checks to see if isGrounded
            {
                //isJumping = false;//Sets jumping to false
                _doubleJump = false;//Double jump is false
            }

            if (isGrounded == false && _doubleJump == false && Input.GetKeyDown(KeyCode.Z))//If you aren't grounded, and are inputing Z, and also havent double jumped yet
            {
                _doubleJump = true;//sets double jump to true, so you can't jump again until grounded
                _jumpTimeCounter = jumpTime;//times the jump
                rigidbody.velocity = Vector2.up * jumpForce;//This is the jump
            }

            isFalling = rigidbody.velocity.y < 0 && !isGrounded;
        }
        public void CrossPlataform()
        {
            if(!isGrounded) 
                boxCollider.enabled = isFalling;
        }


    }
}