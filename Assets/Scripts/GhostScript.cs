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

    // Start is called before the first frame update
    void Start()
    {
        timer = 7;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0) {
            timer = 7;
            print("i moving he he");
            rand = Random.Range(-15f, 15f);
            rand2 = Random.Range(-15f, 15f);


            nextMove = new Vector3(rand, 0, rand2);
        }

        body.MovePosition(transform.position + nextMove * Time.deltaTime * speed);
        

        if(Vector3.Distance(transform.position, player.transform.position) < aggroRange) {

            if(Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, aggroRange)) {

                if(hit.transform == player) {


                }
            }
        }

        
    }
}
