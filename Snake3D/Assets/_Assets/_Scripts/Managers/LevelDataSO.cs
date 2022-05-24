using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Level",menuName = "ScriptableObject/Level Data")]
public class LevelDataSO : ScriptableObject{
    public LevelSaveData saveData;


    public bool IsLongestTimeSurvived(float currentTime){
        if(currentTime > saveData.longestTimeSurvived){
            saveData.longestTimeSurvived = currentTime;
            return true;
        }
        return false;
    }
    public bool IsMostEatenFood(int _foodAmount){
        if(_foodAmount > saveData.mostFoodEatenAmount){
            saveData.mostFoodEatenAmount = _foodAmount;
            return true;
        }
        return false;
    }
    public int GetFoodHighScore(){
        return saveData.mostFoodEatenAmount;
    }
    public float GetTimeHighScore(){
        return saveData.longestTimeSurvived;
    }
    
    #region Saving And Loading..............
    
    [ContextMenu("Save")]
    public void Save(){
        string data = JsonUtility.ToJson(saveData,true);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath,"/",saveData,name,".dat"));
        formatter.Serialize(file,data);
        file.Close();
    }

    [ContextMenu("Load")]
    public void Load(){
        if(File.Exists((string.Concat(Application.persistentDataPath,"/",saveData,name,".dat")))){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream Stream = File.Open(string.Concat(Application.persistentDataPath,"/",saveData,name,".dat"),FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(Stream).ToString(),saveData);
            Stream.Close();
        }
    }

    #endregion
}
[System.Serializable]
public class LevelSaveData{
    public float longestTimeSurvived;
    public int mostFoodEatenAmount;
}
