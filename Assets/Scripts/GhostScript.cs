using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    public GameObject player;
    public float aggroRange;
    public float speed; 
    public Rigidbody body;
    public RectTransform deathScreen;

    RaycastHit hit;
    float timer;
    float rand;
    float rand2;
    Vector3 nextMove;
    RaycastHit objectHit;
    bool collision;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0) {
            timer = 5;
            rand = Random.Range(-10f, 10f);
            rand2 = Random.Range(-10f, 10f);
            nextMove = new Vector3(rand, 0, rand2);
        }
         
        //if close to player go inside block
        if(Vector3.Distance(transform.position, player.transform.position) < aggroRange) {

            if(Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, aggroRange)) {
                
                Vector3 direction = (player.transform.position - transform.position).normalized;
                body.MovePosition(transform.position + direction * Time.deltaTime * speed * 10);

                if (Vector3.Distance(transform.position, player.transform.position) < 3.0f) {
                    Cursor.lockState = CursorLockMode.None;
                    player.GetComponent<PlayerMovementScript>().speed = 0f;
                    //player.GetComponent<PlayerMovementScript>().addEffect(new StunEffect(5f, player));
                    deathScreen.GetComponent<DeathButtonScript>().onDeath();
                }
            }
        }
        else {
            body.MovePosition(transform.position + nextMove * Time.deltaTime * speed);
        }

        
    }

    void OnCollisionEnter(Collision other) {
        if(collision == false) {
            collision = true;
            nextMove = new Vector3(-1*rand, 0, -1*rand2);
        }   
    }

    void OnCollisionExit(Collision coll) {
        collision = false;
    }
}
