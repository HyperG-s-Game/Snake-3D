using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityAttractor : MonoBehaviour {
    

    public static GravityAttractor attractor;
    private void Awake(){
        attractor = this;
    }
    
    public void Attract(Transform _body,float gravity){
        Vector3 dir = (_body.position - transform.position).normalized;
        Vector3 bodyUp = _body.up;
        _body.rotation = Quaternion.FromToRotation(_body.up,dir) * _body.rotation;
        if(_body.TryGetComponent<Rigidbody>(out Rigidbody rb)){
            rb.AddForce(dir * gravity);
        }
    }
}
