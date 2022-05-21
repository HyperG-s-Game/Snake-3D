using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
public class Snake : MonoBehaviour {
    
    [SerializeField] private float bodyMoveSpeed = 2f;    
    [SerializeField] private float movespeed;
    [SerializeField] private float steeringAngle;
    [SerializeField] private int gap = 10;
    [SerializeField] private float checkDistance;
    [SerializeField] private Vector3 checkViewOffset;
    [SerializeField] private LayerMask collisionCheckMask;
    private List<Transform> bodyParts = new List<Transform>();
    private List<Vector3> positionHistory  = new List<Vector3>();
    private PlayerInput playerInput;
    private float steeringDirection;
    private Rigidbody rb;
    private void Awake(){
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start(){
        bodyParts = new List<Transform>();
        positionHistory = new List<Vector3>();
        if(LevelManager.current.GetIsOnFlatWorld()){
            rb.isKinematic = true;
        }else{
            rb.isKinematic = false;
        }
    }
    private void Update(){
        
        if(playerInput.GetInputEnable()){
            transform.position += transform.forward * movespeed * Time.deltaTime;
            steeringDirection = playerInput.Horizontal;
            transform.Rotate(Vector3.up * steeringAngle * steeringDirection * Time.deltaTime);
        }
        positionHistory.Insert(0,transform.position);
        int index = 0;
        foreach(var body in bodyParts){
            Vector3 point = positionHistory[Mathf.Min(index * gap,positionHistory.Count - 1)];
            Vector3 moveDir = point - body.position;
            body.position += moveDir * bodyMoveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
        CheckCollision();

    }
    private void CheckCollision(){
        if(Physics.Raycast(transform.position + checkViewOffset,transform.forward,out RaycastHit hit,checkDistance,collisionCheckMask,QueryTriggerInteraction.Collide)){
            Debug.Log("Hit with " + hit.transform.name);
            if(hit.transform.TryGetComponent<Food>(out Food food)){
                food.DestroyNow();
                LevelManager.current.InvokeOnFoodEat();
                GrowSnake();
            }else{
                GameManagers.current.GameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision coli){
        if(coli.transform.TryGetComponent<Food>(out Food food)){
            food.DestroyNow();
            LevelManager.current.InvokeOnFoodEat();
            GrowSnake();
        }else{
            if(!LevelManager.current.GetIsOnFlatWorld()){
                GameManagers.current.GameOver();

            }
        }
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + checkViewOffset,transform.forward * checkDistance);
    }
    public void GrowSnake(){
        GameObject body = ObjectPoolingManager.current.SpawnFromPool(PoolObjectTag.BodyPart,transform.position,transform.rotation);
        bodyParts.Add(body.transform);
    }

    
}
