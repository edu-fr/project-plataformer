using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace ProjectPlataformer
{
    public class player : MonoBehaviour
    {
        
        private Rigidbody2D rb;
        private BoxCollider2D boxCollider;
        public bool isRunning;

        public float speed = 7;
        public float playerDefaultSpeed = 7;
        public float pepperBuffSpeed = 10;

        //[Header("Ground Check Variables")]
        public bool isGrounded; //Boolean. if is on ground sets to true
        public bool isFalling; // Boolean. If isn't on ground and his rb.velocity.y is negative, it's falling
        public float checkRadius = 0.2f; //decides how large the radius of the ground posiiton
        private LayerMask whatIsGround; //references the ground layer
        private Transform groundPos; //This is the empty game object attached to the player, which checks for Ground
        private bool _isJumpingDown;

        //[Header("Jumping Variables")]
        private float _jumpTimeCounter; //What does this variable do lol
        [SerializeField] private bool _isJumping; //False by default, sets to true if Z is input
        public float jumpForce = 7.5f; //how much force goes in the jump
        public float jumpTime = 0.2f; //How much time you are in the air
        public float fallSpeed = 0.5f;

        //public bool isGrounded;//the boolean to check if you are on thye ground
        private bool canJump;
        private float pressedJump;
        private float groundRemember;

        //Enemy Variables
        private LayerMask EnemyLayerMask;
        private bool enemyChecker;
        private bool isOnEnemy;

        //animation
        private Animator anim;
        private hungry hungry;
        public int maxHungry = 100;
        public int currentHungry;
        bool isMoving;

        [SerializeField] private Material playerDefaultMaterial;
        
        // Pepper buff
        public bool _isPepperBuffActive = false;
        private float _pepperBuffDuration = 4f;
        private float _pepperBuffCurrentTimer = 0f;
        [SerializeField] private Material pepperBuffMaterial; 
        

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            whatIsGround = LayerMask.GetMask("Platforms");
            groundPos = GetComponentInChildren<Transform>();
            EnemyLayerMask = LayerMask.GetMask("Default");
            anim = GetComponent<Animator>();
            hungry = GameObject.FindGameObjectWithTag("HungryBarHud").GetComponent<hungry>();
        }
        
        void Update()
        {
            if (!isRunning)
            {
                StartRunning();
                return;
            }

            Run();
            Jump();

            if (_isPepperBuffActive)
            {
                _pepperBuffCurrentTimer += Time.deltaTime;
                if (_pepperBuffCurrentTimer >= _pepperBuffDuration)
                {
                    _isPepperBuffActive = false;
                    gameObject.GetComponent<SpriteRenderer>().material = playerDefaultMaterial;
                    speed = playerDefaultSpeed;
                    print("End of Pepper Buff!");
                }
            }
            
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
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        public void Jump()
        {
            isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround); //checks for ground 

            if (isGrounded && Input.GetKeyDown(KeyCode.Z) && Input.GetKey(KeyCode.DownArrow) && !_isJumping) // Down jump
            {
                _isJumping = true;
                _jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.down * jumpForce / 2; // Jump downwards
                boxCollider.enabled = false;
                _isJumpingDown = true;
            } else if (isGrounded && Input.GetKeyDown(KeyCode.Z) && !_isJumping) //Checks to see if is inputing Z, and is geounded, before allowing a jump
            {
                _isJumping = true; 
                _jumpTimeCounter = jumpTime; //times the jump
                rb.velocity = Vector2.up * jumpForce; //This is the actual jump
                boxCollider.enabled = false;
            }

            if (Input.GetKeyUp(KeyCode.Z) && !_isJumpingDown)
            {
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallSpeed);
                }
            }
            
            if (_isJumpingDown && Input.GetKeyUp(KeyCode.DownArrow))
            {
                _isJumpingDown = false;
                boxCollider.enabled = true; 
            }
            
            isFalling = rb.velocity.y < -0.01 && _isJumping && !_isJumpingDown;
            
            if (isFalling)
            {
                print("Is falling!");
                boxCollider.enabled = true;
            }
            
            if (isGrounded && (rb.velocity.y < 0.1 && rb.velocity.y > -0.1) && boxCollider.enabled)
                _isJumping = false;

        }

        public void HungryBar()
        {
            if (true)
            {
                currentHungry -= 1;
                hungry.SetHealth(currentHungry);
            }
        }

        public void ActivatePepperBuff()
        {
            print("Pepper buff activated!");
            _isPepperBuffActive = true;
            _pepperBuffCurrentTimer = 0f;
            gameObject.GetComponent<SpriteRenderer>().material = pepperBuffMaterial;
            speed = pepperBuffSpeed;
        }

    }
}