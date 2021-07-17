using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{

    public LayerMask playerMask;
    bool playerNear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerNear = Physics.CheckSphere(transform.position, 1.5f, playerMask);
        if(playerNear) {
            gameObject.SetActive(false);
        }

    }
}
