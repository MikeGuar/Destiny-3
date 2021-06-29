using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Script : MonoBehaviour
{

    public LayerMask player;
    public Transform playerCheck;

    bool isPlayerIn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerIn = Physics.CheckSphere(playerCheck.position, 2.5f, player);
        if(isPlayerIn) {
            SceneManager.LoadScene("Level 1");
        }

    }
    
}
