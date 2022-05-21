using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {
    [SerializeField] private bool onFlatGround = true;

    [SerializeField] private float gravity = -10f;
    private Rigidbody rb;
    private GravityAttractor planet;
    private void Start(){
        planet = GravityAttractor.attractor;
        rb = GetComponent<Rigidbody>(); 
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void FixedUpdate(){
        if(!onFlatGround){
            planet.Attract(transform,gravity);
        }
    }
    
}
