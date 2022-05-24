using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UiManager : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI[] timerTexts,timeHighScoreTexts;
    [SerializeField] private TextMeshProUGUI[] foodAmountText,foodHighScoreTexts;

#region Singelton...........
    public static UiManager current{get;private set;}

    private void Awake(){
        current = this;
    }
#endregion

    public void SetTimerTexts(float minit,float second){
        string timeString = string.Format("{00:00}:{1:00}",minit,second);
        for (int i = 0; i < timerTexts.Length; i++){
            timerTexts[i].SetText(timeString);
        }
    }
    public void SetFoodAmount(int _foodAmount){
        for (int i = 0; i < foodAmountText.Length; i++){
            foodAmountText[i].SetText(_foodAmount.ToString());
        }
    }
    public void SetHighScoreTime(float minit,float second){
        for (int i = 0; i < timeHighScoreTexts.Length; i++){
            string timeString = string.Format("{00:00}:{1:00}",minit,second);
            timeHighScoreTexts[i].SetText(timeString);
        }
    }
    public void SetHighScoreFood(int foodAmount){
        for (int i = 0; i < foodHighScoreTexts.Length; i++){
            foodHighScoreTexts[i].SetText(foodAmount.ToString());
        }

    }
    
}
