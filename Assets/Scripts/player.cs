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

        //[Header("Ground Check Variables")]
        public bool isGrounded; //Boolean. if is on ground sets to true
        public bool isFalling; // Boolean. If isn't on ground and his rb.velocity.y is negative, it's falling
        public float checkRadius = 0.2f; //decides how large the radius of the ground posiiton
        private LayerMask whatIsGround; //references the ground layer
        private Transform groundPos; //This is the empty game object attached to the player, which checks for Ground

        //[Header("Jumping Variables")]
        private float _jumpTimeCounter; //What does this variable do lol
        private bool _doubleJump; //False by default, is true if isGrounded is false and isJumping is false
        private bool _isJumping; //False by default, sets to true if Z is input
        public float jumpForce = 7.5f; //how much force goes in the jump
        public float jumpTime = 0.2f; //How much time you are in the air
        public float fallSpeed = 0.5f;

        //public bool isGrounded;//the boolean to check if you are on thye ground
        private bool canJump;
        private float pressedJump;
        public float pressedJumpTime = 0.4f;
        public float groundRememberTime = 0.4f;
        private float groundRemember;

        //Enemy Variables
        public Transform enemyCheck;
        private LayerMask EnemyLayerMask;
        public float enemyJumpSmall;
        public float enemyJumpBig;
        public float enemyCheckRadius;
        private bool enemyChecker;
        private bool isOnEnemy;

        //animation
        private Animator anim;
        private hungry hungry;
        public int maxHungry = 100;
        public int currentHungry;
        bool isMoving;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            whatIsGround = LayerMask.GetMask("Plataforms");
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
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        public void Jump()
        {
            isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround); //checks for ground

            if (isGrounded &&
                Input.GetKeyDown(KeyCode.Z)) //Checks to see if is inputing Z, and is geounded, before allowing a jump
            {
                _jumpTimeCounter = jumpTime; //times the jump
                rb.velocity = Vector2.up * jumpForce; //This is the actual jump
            }

            if (isGrounded) //Checks to see if isGrounded
            {
                //isJumping = false;//Sets jumping to false
                _doubleJump = false; //Double jump is false
            }

            if (isGrounded == false && _doubleJump == false &&
                Input.GetKeyDown(KeyCode
                    .Z)) //If you aren't grounded, and are inputing Z, and also havent double jumped yet
            {
                _doubleJump = true; //sets double jump to true, so you can't jump again until grounded
                _jumpTimeCounter = jumpTime; //times the jump
                rb.velocity = Vector2.up * jumpForce; //This is the jump
            }

            isFalling = rb.velocity.y < 0 && !isGrounded;

            if ((groundRemember > 0) && (pressedJump > 0))
            {
                pressedJump = 0;
                groundRemember = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (rb.velocity.y > 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * fallSpeed);
                }
            }

        }

        public void CrossPlataform()
        {
            if (!isGrounded)
                boxCollider.enabled = isFalling;
        }

        public void HungryBar()
        {
            if (true)
            {
                currentHungry -= 1;
                hungry.SetHealth(currentHungry);
            }
        }
    }
}