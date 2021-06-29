using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashCDScript : MonoBehaviour
{

    float lastDash;
    float dashTime;
    public float dashCD = 3;
    float timer;
    public Text dashText;

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown("q") && Time.time > lastDash) {
            lastDash = dashCD + Time.time;
            timer = 3;
        }

        if(timer > 0) {
            timer = lastDash - Time.time;
            dashText.text = "Dash: " + timer;
        }

        else {
            timer = 0;
            dashText.text = "Dash: Q";
        }

    }
}
