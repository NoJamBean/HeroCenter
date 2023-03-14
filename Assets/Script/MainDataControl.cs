using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class MainDataContainer
{
    public int UserMoney;
    public int UserCoin;
    public int PlusMoneyBytime;

}
public class MainDataControl : MonoBehaviour
{
    [SerializeField]
    private string Save_Directory;
    private string Save_FileName;

    private void Start()
    {
        Save_FileName = "MainData.json";
        Save_Directory = Path.Combine(Application.persistentDataPath + Save_FileName);

        LoadData();
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();

    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

    public void SaveData()
    {
        MainDataContainer maindataContainer = new MainDataContainer();

        maindataContainer.UserMoney =Data.StaticInfo.UserMoney;
        maindataContainer.UserCoin = Data.StaticInfo.UserCoin;
        maindataContainer.PlusMoneyBytime = Data.StaticInfo.PlusMoneyByTime;

        string data = JsonUtility.ToJson(maindataContainer);
        File.WriteAllText(Save_Directory + Save_FileName, data);

    }

    public void LoadData()
    {
        MainDataContainer mainDataContainer = new MainDataContainer();

        if (File.Exists(Save_Directory + Save_FileName))
        {
            string loadData = File.ReadAllText(Save_Directory + Save_FileName);
            mainDataContainer = JsonUtility.FromJson<MainDataContainer>(loadData);

            Data.StaticInfo.UserMoney = mainDataContainer.UserMoney;
            Data.StaticInfo.UserCoin = mainDataContainer.UserCoin;
            Data.StaticInfo.PlusMoneyByTime = mainDataContainer.PlusMoneyBytime;
        }

        else
            Directory.CreateDirectory(Save_Directory);

    }
}
