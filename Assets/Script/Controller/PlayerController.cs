using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    CharacterController Controller;
    //walking value
    public float walkingSpeed = 2.0f;
    public float runningSpeed = 6.0f;
    public float rotationSpeed = 15.0f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -19.81f;
    public float jumpHeight = 3f;
    float turnSmoothVelocity;
    public Camera cam;

    //jump value
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    //play animator
    private Animator animator;
            
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //velocity fall
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            animator.SetBool("atGround", true);
        }
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        velocity.y += gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("isJump", true);
            animator.SetBool("atGround", false);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //set speed runningd
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (animator.GetBool("isWalking"))
                {
                    animator.SetBool("isRunning", true);
                }
                else
                {
                    animator.SetBool("isRunning", false);
                }

                Controller.Move(moveDir.normalized * runningSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("isRunning", false);
                Controller.Move(moveDir.normalized * walkingSpeed * Time.deltaTime);
            }

        }
    }
}
