using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    public GameObject player;
    public float aggroRange;
    public float speed; 
    public Rigidbody body;

    RaycastHit hit;
    float timer;
    float rand;
    float rand2;
    Vector3 nextMove;
    RaycastHit objectHit;


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
            print("i moving he he");
            rand = Random.Range(-10f, 10f);
            rand2 = Random.Range(-10f, 10f);
            nextMove = new Vector3(rand, 0, rand2);
        }

        /*if (Physics.Raycast(transform.position, Vector3.forward, out objectHit, 5) || Physics.Raycast(transform.position, Vector3.left, out objectHit, 5)
        || Physics.Raycast(transform.position, Vector3.right, out objectHit, 5)) {
            timer = 0;
        }*/

        if (Physics.Raycast(transform.position, Vector3.forward, out objectHit, 5) || Physics.Raycast(transform.position, Vector3.right, out objectHit, 5)
        || Physics.Raycast(transform.position, Vector3.left, out objectHit, 5)) {
            nextMove = new Vector3(-1*rand, 0, -1*rand2);
        }
        
        
        //if close to player go inside block
        if(Vector3.Distance(transform.position, player.transform.position) < aggroRange) {

            if(Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, aggroRange)) {
                
                body.MovePosition(transform.position + player.transform.position * Time.deltaTime * speed);

                if (Vector3.Distance(transform.position, player.transform.position) < 2.0f) {
                }
            }
        }
        else {
            body.MovePosition(transform.position + nextMove * Time.deltaTime * speed);
        }

        
    }
}
