using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
public enum FoodType{
    WithGravity,WithoutGravity,
}
public class Food : MonoBehaviour,IPooledObject{
    [SerializeField] private float spawnForce;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private FoodType foodType;
    private Rigidbody rb;
    private void Awake(){
        rb = GetComponent<Rigidbody>();
        
    }
    public void DestroyMySelfWithDelay(float delay = 0){
        Invoke(nameof(DestroyNow),delay);
    }

    public void DestroyNow(){
        CancelInvoke(nameof(DestroyNow));
        LevelManager.current.InvokeOnFoodDistroyed();
        gameObject.SetActive(false);
    }

    public void OnObjectReuse(){
        switch(foodType){
            case FoodType.WithoutGravity:
                rb.AddForce(Vector3.up * spawnForce,ForceMode.Impulse);
                rb.AddTorque(Vector3.right * 4f,ForceMode.Impulse);

            break;
        }
        DestroyMySelfWithDelay(lifeTime);
    }

    
}
