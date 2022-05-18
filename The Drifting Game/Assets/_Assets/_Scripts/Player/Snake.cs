using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Snake : MonoBehaviour {
    
    [SerializeField] private float bodyMoveSpeed = 2f;    
    [SerializeField] private float movespeed;
    [SerializeField] private float steeringAngle;
    [SerializeField] private int gap = 10;


    [SerializeField] private Transform bodyPrefab;
    private List<Transform> bodyParts;
    private List<Vector3> positionHistory  = new List<Vector3>();

    private void Start(){
        bodyParts = new List<Transform>();
        positionHistory = new List<Vector3>();
        for (int i = 0; i < 5; i++){
            GrowSnake();
        }
        
    }
    private void Update(){
        transform.position += transform.forward * movespeed * Time.deltaTime;

        float steeringDirection = Input.GetAxisRaw("Horizontal");
        transform.Rotate(Vector3.up * steeringAngle * steeringDirection * Time.deltaTime);
        positionHistory.Insert(0,transform.position);
        int index = 0;
        foreach(var body in bodyParts){
            Vector3 point = positionHistory[Mathf.Min(index * gap,positionHistory.Count - 1)];
            Vector3 moveDir = point - body.position;
            body.position += moveDir * bodyMoveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
        
        
    }
    private void GrowSnake(){
        Transform body = Instantiate(bodyPrefab);
        bodyParts.Add(body);
    }
}
