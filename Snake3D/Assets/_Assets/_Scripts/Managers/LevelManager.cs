using System;
using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
public class LevelManager : MonoBehaviour {

    [Header("Spherical World Variables")]
    [SerializeField] private Transform planet;
    [SerializeField] private float spawnRadius;

    [Header("Flat World Varibales")]
    [SerializeField] private Vector3 gridSpawnPositionOffset;
    [SerializeField] private Vector2 grid;

    [Header("Spawn Checks")]
    [SerializeField] private bool onFlatWorld;
    [SerializeField] private LayerMask hitMask;
    [Space(30)]

    private Vector3 radomPoint;
    private Vector3 gridPosition;

    #region Events.............

    public Action OnSnakeEatFood;

    #endregion


    public static LevelManager current{get;private set;}
    private void Awake(){
        current = this;
    }
    private void Start(){
        OnSnakeEatFood += SpawnFood;
    }
    
    public void SpawnFood(){
        if(onFlatWorld){
            SpawnFoodOnFlatWorld();
        }else{
            SpawnFoodOnSphericalWorld();
        }
    }
    private void SpawnFoodOnFlatWorld(){
        gridPosition = transform.position + gridSpawnPositionOffset;
        radomPoint = new Vector3(UnityEngine.Random.Range(-grid.x / 2,grid.x / 2f),0,UnityEngine.Random.Range(-grid.y / 2,grid.y / 2)) + gridPosition;
        if(Physics.Raycast(radomPoint,Vector3.down,out RaycastHit hit,2000f,hitMask,QueryTriggerInteraction.Collide)){
            ObjectPoolingManager.current.SpawnFromPool(PoolObjectTag.GravityLessFood,hit.point,Quaternion.identity);
        }else{
            SpawnFoodOnFlatWorld();
        }
    }
    private void SpawnFoodOnSphericalWorld(){
        radomPoint = UnityEngine.Random.insideUnitSphere * spawnRadius;
        Vector3 checkDir = (planet.position - radomPoint).normalized;
        if(Physics.Raycast(radomPoint,checkDir,out RaycastHit hit,2000f,hitMask,QueryTriggerInteraction.Collide)){
            ObjectPoolingManager.current.SpawnFromPool(PoolObjectTag.GravityFood,hit.point,Quaternion.identity);
        }else{
            SpawnFoodOnSphericalWorld();
        }
    }

    public void InvokeOnFoodEat(){
        OnSnakeEatFood?.Invoke();
    }

    public bool GetIsOnFlatWorld(){
        return onFlatWorld;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        gridPosition = transform.position + gridSpawnPositionOffset;
        Gizmos.DrawWireCube(gridPosition,new Vector3(grid.x,0.5f,grid.y));
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere(radomPoint,0.2f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(planet.position,spawnRadius);
    }
}
