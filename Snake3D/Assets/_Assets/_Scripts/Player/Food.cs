using UnityEngine;
using GamerWolf.Utils;
using System.Collections;
using System.Collections.Generic;
public class Food : MonoBehaviour,IPooledObject{
    [SerializeField] private float lifeTime = 3f;
    public void DestroyMySelfWithDelay(float delay = 0){
        Invoke(nameof(DestroyNow),delay);
    }

    public void DestroyNow(){
        gameObject.SetActive(false);
    }

    public void OnObjectReuse(){
        DestroyMySelfWithDelay(lifeTime);
    }

    
}
