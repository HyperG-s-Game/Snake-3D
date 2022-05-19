using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {
    [SerializeField] private bool onFlatGround = true;
    [SerializeField] private float gravity = -10f;
    private void FixedUpdate(){
        if(!onFlatGround){
            GravityAttractor.attractor.Attract(this.transform,gravity);
        }
    }
    
}
