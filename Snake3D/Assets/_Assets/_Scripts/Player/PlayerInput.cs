using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour {
    
    
    [SerializeField] private bool enableInput,onPc;
    [SerializeField] private Joystick joystick;

    public float Horizontal{get;private set;}
    private void Update(){
        GetInputs();
    }
    
    private void GetInputs(){
        
        Horizontal = onPc ? Input.GetAxisRaw("Horizontal") : joystick.Horizontal;
        
    }
    public void ToggleInput(bool value){
        enableInput = value;
    }
    public bool GetInputEnable(){
        return enableInput;
    }
}
