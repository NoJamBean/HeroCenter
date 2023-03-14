using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class DataContainer
{ 
    public int PlusMoney;
   
    public List<string> Name = new List<string>();
    public List<int> UpgradeCost = new List<int>();
    public List<int> UpgradeLevel = new List<int>();
    public List<int> IncreaseValue = new List<int>();
}
public class DataControl : MonoBehaviour
{
    [SerializeField]
    private UpgradeScript[] Upgrade;
    
    private string Save_Directory;
    private string Save_FileName;

    private void Awake()
    {
        for (int i = 0; i < Upgrade.Length; i++)
        {
            Upgrade[i] = Upgrade[i].GetComponent<UpgradeScript>();
        }
        
    }
    private void Start()
    {
        Save_FileName = "Save.json";
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
        DataContainer dataContainer = new DataContainer();

        dataContainer.PlusMoney = Data.StaticInfo.PlusMoney;

        for (int i =0; i< Upgrade.Length; i++)
        {
            if (Upgrade[i].Name != null)
            {
                dataContainer.Name.Add(Upgrade[i].Name);
                dataContainer.UpgradeCost.Add(Upgrade[i].UpgradeCost);
                dataContainer.UpgradeLevel.Add(Upgrade[i].UpgradeLevel);
                dataContainer.IncreaseValue.Add(Upgrade[i].Increasevalue);
            }
            else break;
        }
        string data = JsonUtility.ToJson(dataContainer);
        File.WriteAllText(Save_Directory+Save_FileName, data);
        
    }

    public void LoadData()
    {
        DataContainer dataContainer = new DataContainer();

        if (File.Exists(Save_Directory + Save_FileName))
        {
            string loadData = File.ReadAllText(Save_Directory + Save_FileName);
            dataContainer = JsonUtility.FromJson<DataContainer>(loadData);

            Data.StaticInfo.PlusMoney = dataContainer.PlusMoney;

            for (int i = 0; i < dataContainer.UpgradeLevel.Count; i++)
            {
                Upgrade[i].Name = dataContainer.Name[i];
                Upgrade[i].UpgradeCost = dataContainer.UpgradeCost[i];
                Upgrade[i].UpgradeLevel = dataContainer.UpgradeLevel[i];
                Upgrade[i].Increasevalue = dataContainer.IncreaseValue[i];
            }
            
        }

        else
            Directory.CreateDirectory(Save_Directory);

    }
}
