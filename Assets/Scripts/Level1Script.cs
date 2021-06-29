using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onCollisionEnter(Collision other) {
        Debug.Log("test");
        print("test");
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene("Level 1");
        }
    }

    void onTriggerEnter(Collider other) {
        Debug.Log("test");
        print("test");
        if(other.gameObject.tag == "Player") {
            SceneManager.LoadScene("Level 1");
        }
    }
    
}
