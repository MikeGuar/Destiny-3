using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : Effect {

    private float originalSpeed;

    public StunEffect(float duration, GameObject target): base(duration, target) {
        originalSpeed = target.GetComponent<PlayerMovementScript>().speed;

        while (duration > 0) {
            duration -= Time.deltaTime;
            target.GetComponent<PlayerMovementScript>().speed = 0;
        }

        target.GetComponent<PlayerMovementScript>().speed = originalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
