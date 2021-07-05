using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect {

    protected float duration;
    protected GameObject target;

    public Effect(float duration, GameObject target) {
        this.duration = duration;
        this.target = target;
    }
}
