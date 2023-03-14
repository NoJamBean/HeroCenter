using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BossStageDataContainer
{
    public int currentAttackDamage;
    public float currentCriticalDamage;
    public float currentCriticalPercent;

    public int currentStage;
    public int givecoin;

    public List<string> Name = new List<string>();
    public List<int> UpgradeCost = new List<int>();
    public List<int> UpgradeLevel = new List<int>();
    public List<float> IncreaseValue = new List<float>();
}
public class BossStageDataControl : MonoBehaviour
{
    [SerializeField]
    private BossStageUpgradeScript[] BossUpgrade;
    [SerializeField]
    private BossStageUIControl bossStageUIControl;

    private string Save_Directory;
    private string Save_FileName;

    private void Awake()
    {
        bossStageUIControl = GetComponent<BossStageUIControl>();
        for (int i = 0; i < BossUpgrade.Length; i++)
        {
            BossUpgrade[i] = BossUpgrade[i].GetComponent<BossStageUpgradeScript>();
        }
        
    }
    private void Start()
    {
        Save_FileName = "BossSave.json";
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
        BossStageDataContainer BossdataContainer = new BossStageDataContainer();

        BossdataContainer.currentAttackDamage = bossStageUIControl.currentAttackDamage;
        BossdataContainer.currentCriticalDamage = bossStageUIControl.currentCriticlaDamge;
        BossdataContainer.currentCriticalPercent = bossStageUIControl.currentCriticlaPercent;
        BossdataContainer.currentStage = bossStageUIControl.currentStage;
        BossdataContainer.givecoin = bossStageUIControl.givecoin;

        for (int i = 0; i < BossUpgrade.Length; i++)
        {
            if (BossUpgrade[i].Name != null)
            {
                BossdataContainer.Name.Add(BossUpgrade[i].Name);
                BossdataContainer.UpgradeCost.Add(BossUpgrade[i].UpgradeCost);
                BossdataContainer.UpgradeLevel.Add(BossUpgrade[i].UpgradeLevel);
                BossdataContainer.IncreaseValue.Add(BossUpgrade[i].Increasevalue);
            }
            else break;
        }

        string data = JsonUtility.ToJson(BossdataContainer);
        File.WriteAllText(Save_Directory + Save_FileName, data);

    }

    public void LoadData()
    {
        BossStageDataContainer BossdataContainer = new BossStageDataContainer();

        if (File.Exists(Save_Directory + Save_FileName))
        {
            string loadData = File.ReadAllText(Save_Directory + Save_FileName);
            BossdataContainer = JsonUtility.FromJson<BossStageDataContainer>(loadData);

            bossStageUIControl.currentAttackDamage = BossdataContainer.currentAttackDamage;
            bossStageUIControl.currentCriticlaDamge = BossdataContainer.currentCriticalDamage;
            bossStageUIControl.currentCriticlaPercent = BossdataContainer.currentCriticalPercent;
            bossStageUIControl.currentStage = BossdataContainer.currentStage;
            bossStageUIControl.givecoin = BossdataContainer.givecoin;

            for (int i = 0; i < BossdataContainer.UpgradeLevel.Count; i++)
            {
                BossUpgrade[i].Name = BossdataContainer.Name[i];
                BossUpgrade[i].UpgradeCost = BossdataContainer.UpgradeCost[i];
                BossUpgrade[i].UpgradeLevel = BossdataContainer.UpgradeLevel[i];
                BossUpgrade[i].Increasevalue = BossdataContainer.IncreaseValue[i];
            }

        }

        else
            Directory.CreateDirectory(Save_Directory);

    }
}
