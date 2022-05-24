using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagers : MonoBehaviour {
    
    
    [SerializeField] private LevelDataSO levelDataSO;

    [SerializeField] private UnityEvent onGameStart,onGamePlaying,onGameEnd;

    [SerializeField] private bool isGamePlaying;


    [SerializeField] private int foodAmount;
    [SerializeField] private float currentTimer;

    #region Singelton...........
    public static GameManagers current;
    private void Awake(){
        current = this;
    }
    #endregion


    private void Start(){
        isGamePlaying = false;
        StartCoroutine(StartGameRoutine());
    }
    private void Update(){
        if(isGamePlaying){
            currentTimer += Time.deltaTime;
            DispalyTimer(currentTimer);
        }
    }
    private void DispalyTimer(float currentTime){
        float minit = Mathf.FloorToInt(currentTime / 60);
        float second = Mathf.FloorToInt(currentTime % 60);
        
        // string timeString = string.Format("{00:00} {1:00}",minit,second);
        UiManager.current.SetTimerTexts(minit,second);

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
        FinalScore();
    }
    private void FinalScore(){
        if(levelDataSO.IsLongestTimeSurvived(currentTimer)){
            float minint = Mathf.FloorToInt(levelDataSO.GetTimeHighScore() / 60);
            float second = Mathf.FloorToInt(levelDataSO.GetTimeHighScore() % 60);
            UiManager.current.SetHighScoreTime(minint,second);
        }
        if(levelDataSO.IsMostEatenFood(foodAmount)){
            UiManager.current.SetHighScoreFood(levelDataSO.GetFoodHighScore());
        }

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
    public void IncraseFoodAmount(){
        foodAmount++;
        UiManager.current.SetFoodAmount(foodAmount);
    }
}
