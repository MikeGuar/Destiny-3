using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float speed = 12f; //used to control exactly how fast the player goes
    public float gravity = -9.8f; //how fast the player falls
    public float jumpHeight = 2.5f; //how high the player jumps
    public float dashCD = 3; //time inbetween each dash

    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.59f; 
    public LayerMask groundMask;
    public LayerMask jumpMask;
    public Transform body;
    public AudioSource dashSound;

    float jumpPadDist = 0.5f; 
    float lastDash;
    Vector3 velocity;
    bool isGrounded;
    bool onJumpPad;
    float dashTime = 0.5f;
    public float dashSpeed = 10; //how fast the player dashes
    public float jumpPadHeight = 3; //the multiplier for how much a jump pad will increase a players jump height
    bool doubleJump = true;
    bool didJump = false;
    bool justSprinted = false;
    float defaultSpeed;
    bool isCrouched = false;
    float currentSpeed;
    public float sprintBoost;
    RaycastHit hit;

    void Start() {
        defaultSpeed = speed + 0.15f;
        dashSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        onJumpPad = Physics.CheckSphere(groundCheck.position, jumpPadDist, jumpMask);

        //SPRINT
        if(Input.GetKey(KeyCode.LeftShift) && !isCrouched) {
            if(speed < 15) {
                speed = 15f;   //increases speed from 8 to 15 upon pressing lshift, and while not crouching
            }
            justSprinted = true;
            //changing FOV on sprinting
            if (Camera.main.fieldOfView < 95f || speed>25) {
                Camera.main.fieldOfView += 0.3f;
            }
        }
        else if(justSprinted && speed >= defaultSpeed && isGrounded) {
            speed -= 0.2f;
            justSprinted = false;
        } else {
            if (Camera.main.fieldOfView > 80f) {
                Camera.main.fieldOfView -= 0.3f;
            }
        }

        //MUST HAVE THE "Ground" MASK FOR JUMPS TO WORK
        if(isGrounded && velocity.y<0) {
            velocity.y = -0.5f;
            doubleJump = true; //resetting double jump check upon hitting ground
        }

        //TO MAKE JUMP PADS, SIMPLY PUT THE "JumpPads" MASK ON WHATEVER YOU WANT
        if(onJumpPad && Input.GetButtonDown("Jump")) {
            velocity.y = Mathf.Sqrt((jumpHeight*jumpPadHeight)*-2*gravity); //same as normal jump equation, but multiply jumpheight by how much the pad is boosting by
            didJump = true;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //INCREASES SPEED BY 10 ON Q PRESS
        if(Input.GetKeyDown("q") && Time.time > lastDash && !isCrouched) {
            lastDash = dashCD + Time.time; //sets time since last dash
            dashTime = 0.45f + Time.time; //how long bonus speed lasts (0.45s)
            speed = speed + dashSpeed; //setting actual speed
            dashSound.Play(0);
        }

        //AFTER 0.45s SPEED RETURNS TO NORMAL
        if (Time.time > dashTime && speed >= defaultSpeed) {
            if(isGrounded || speed>23.5f) {
                speed = speed - 0.2f; //resetting speed
            }

            if((Camera.main.fieldOfView > 95 || speed<25) && Camera.main.fieldOfView >80) {
                Camera.main.fieldOfView -= 0.15f;
            }
        }

        //CROUCH
        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if (isCrouched) {
                body.transform.localScale += new Vector3(0f, 0.5f, 0f);
            } else {
                body.transform.localScale += new Vector3(0f, -0.5f, 0f);
            }
            isCrouched = !isCrouched;
        }

        //THE METHOD THAT MOVES THE PLAYER
        controller.Move(move * speed * Time.deltaTime);

        //JUMP - must be grounded or have not double jumped
        if(Input.GetButtonDown("Jump") && (isGrounded || doubleJump) && !didJump) {
            velocity.y = Mathf.Sqrt(jumpHeight*-2*gravity);
            
            if(isGrounded==false) {
                doubleJump=false;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        didJump=false;

        if(Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, groundMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
            }
        }
    }
}
