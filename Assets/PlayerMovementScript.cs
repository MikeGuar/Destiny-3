using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float speed = 12f;
    public float gravity = -9.8f;
    public float jumpHeight = 2.5f;
    public float dashCD = 3;

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
    public float dashSpeed = 10;
    public float jumpPadHeight = 3;
    bool doubleJump = true;
    bool didJump = false;
    bool didDash = false;
    bool justSprinted = false;
    float defaultSpeed;
    bool isCrouched = false;

    RaycastHit hit;

    void Start() {
        defaultSpeed = speed;
        dashSound = GetComponent<AudioSource>();
    }

    void FixedUpdate() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -5) {
            Vector3 newPos = this.transform.position;
			newPos.y = 1;
			this.transform.position = newPos;
        }

        if(Input.GetKey(KeyCode.LeftShift) && !isCrouched) {
            if(speed < 35) {
                speed = 15;   
            }
            justSprinted = true;
            if (Camera.main.fieldOfView < 95f) {
                Camera.main.fieldOfView += 0.3f;
            }
        }
        else if(justSprinted) {
            speed = defaultSpeed;
            justSprinted = false;
        } else {
            if (Camera.main.fieldOfView > 80f) {
                Camera.main.fieldOfView -= 0.3f;
            }
        }
        

        //MUST HAVE THE "Ground" MASK FOR JUMPS TO WORK
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y<0 ) {
            velocity.y = -0.5f;
            doubleJump = true; //resetting double jump check upon hitting ground
        }

        //TO MAKE JUMP PADS, SIMPLY PUT THE "JumpPads" MASK ON WHATEVER YOU WANT
        onJumpPad = Physics.CheckSphere(groundCheck.position, jumpPadDist, jumpMask);
        if(onJumpPad && Input.GetButtonDown("Jump")) {
            velocity.y = Mathf.Sqrt((jumpHeight*jumpPadHeight)*-2*gravity); //same as normal jump equation, but multiply jumpheight by how much the pad is boosting by
            didJump = true;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        //INCREASES SPEED BY 10 ON Q PRESS
        if(Input.GetKeyDown("q") && Time.time > lastDash) {
            lastDash = dashCD + Time.time; //sets time since last dash
            dashTime = 0.5f + Time.time; //how long bonus speed lasts (0.5s)
            speed = speed + dashSpeed; //setting actual speed
            didDash = true;
            dashSound.Play(0);
        }

        //AFTER 0.5s SPEED RETURNS TO NORMAL
        if (Time.time > dashTime && didDash) {
            speed = speed - dashSpeed; //resetting speed
            didDash=false;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)) {
            if (isCrouched) {
                body.transform.localScale += new Vector3(0f, 0.5f, 0f);
            } else {
                body.transform.localScale += new Vector3(0f, -0.5f, 0f);
            }

            isCrouched = !isCrouched;
        }

        controller.Move(move * speed * Time.deltaTime);


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
