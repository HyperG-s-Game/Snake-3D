using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class GameManagers : MonoBehaviour {
    
    


    [SerializeField] private UnityEvent onGameStart,onGamePlaying,onGameEnd;

    [SerializeField] private bool isGamePlaying;
    public static GameManagers current;
    private void Awake(){
        current = this;
    }
    private void Start(){
        isGamePlaying = false;
        StartCoroutine(StartGameRoutine());
    }
    private IEnumerator StartGameRoutine(){
        onGameStart?.Invoke();
        while(!isGamePlaying){
            yield return null;
        }
        isGamePlaying = true;
        yield return StartCoroutine(PlayGameRoutiene());
    }
    private IEnumerator PlayGameRoutiene(){
        onGamePlaying?.Invoke();
        while(isGamePlaying){
            yield return null;
        }
        onGameEnd?.Invoke();
    }
    public void PlayGame(){
        isGamePlaying = true;
    }
    public void GameOver(){
        isGamePlaying = false;
    }
    public void Restart(){
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
