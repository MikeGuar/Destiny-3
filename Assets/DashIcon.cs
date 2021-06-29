using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIcon : MonoBehaviour
{

    float lastDash;
    float dashTime;
    public float dashCD = 3;
    float timer;
    public Transform image;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q") && Time.time > lastDash) {
            GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            lastDash = dashCD + Time.time;
            timer = 3;
        }

        if(timer > 0.025) {
            timer = lastDash - Time.time;


            GetComponent<RectTransform>().sizeDelta = new Vector2((timer/-3*200)+200, 200);
        }

        else {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            timer = 0;

        }
        
    }
}
