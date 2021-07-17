using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerScript : MonoBehaviour
{

    public GameObject player;
    public Rigidbody body;
    float speed = 2;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        body.MovePosition(transform.position + direction * 0.04f * speed);
        
    }
}
