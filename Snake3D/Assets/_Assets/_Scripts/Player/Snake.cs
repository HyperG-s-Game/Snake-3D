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
    [SerializeField] private float checkRadius;
    [SerializeField] private Vector3 checkViewOffset;
    [SerializeField] private LayerMask collisionCheckMask,bodyLayer;
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
        int index = 0;
        positionHistory.Insert(0,transform.position);
        foreach(var body in bodyParts){
            Vector3 point = positionHistory[Mathf.Min(index * gap,positionHistory.Count - 1)];
            Vector3 moveDir = (point - body.position).normalized;
            body.position += moveDir * bodyMoveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }
        CheckCollision();

    }
    private void CheckCollision(){
        Collider[] colis = Physics.OverlapSphere(transform.position,checkRadius,collisionCheckMask,QueryTriggerInteraction.Collide);
        for (int i = 0; i < colis.Length; i++){
            if(colis[i].transform.TryGetComponent<Food>(out Food food)){
                Debug.Log("Hit with " + colis[i].transform.name);
                food.DestroyNow();
                GameManagers.current.IncraseFoodAmount();
                GrowSnake();
            }else{
                GameManagers.current.GameOver();

            }
        }
        if(Physics.Raycast(transform.position + checkViewOffset,transform.forward,out RaycastHit hit,checkDistance,bodyLayer)){
            if(hit.transform.TryGetComponent<BodyPart>(out BodyPart body)){
                GameManagers.current.GameOver();
            }
        }
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position + checkViewOffset,transform.forward * checkDistance);
        Gizmos.DrawWireSphere(transform.position ,checkRadius);
    }
    [ContextMenu("GrownSnake")]
    public void GrowSnake(){
        GameObject body = ObjectPoolingManager.current.SpawnFromPool(PoolObjectTag.BodyPart,transform.position,transform.rotation);
        bodyParts.Add(body.transform);
    }

    
}
