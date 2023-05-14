using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using CAH.GameSystem.BigNumber;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager Instance = null;
    void Awake()
    {
        if (null == Instance)
        {
            Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameManager _I
    {
        get
        {
            if (null == Instance)
            {
                return null;
            }
            return Instance;
        }
    }
    #endregion

    public enum DataType { Save, Load };
    public enum StageType { Main, Boss };
    WaitForSeconds wfs1 = new WaitForSeconds(1);

    #region Public Function
    public void Exit()
    {
        Application.Quit();
    }

    public void MoneyIncrease()
    {
        StaticInfo.mainDataContainer.UserMoney += StaticInfo.mainDataContainer.PlusMoney;
    }

    public void UIOnOff(GameObject gameObject)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
        else if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    public IEnumerator MoneyIncreaseByTime()
    {
        while (true)
        {
            StaticInfo.mainDataContainer.UserMoney += StaticInfo.mainDataContainer.PlusMoneyByTime;

            yield return wfs1;
        }
    }

    public void LoadMain()
    {
        LoadScene("MainScene");
    }

    public void LoadBoss()
    {
        LoadScene("BossScene");
    }


    public void SaveLoadData(DataType dataType, StageType stageType, UpgradeScript[] Upgrade)
    {
        if (dataType == DataType.Save)
        {
            SaveData(stageType, Upgrade);
        }
        else if (dataType == DataType.Load)
        {
            LoadData(stageType, Upgrade);
        }
    }
    #endregion

    #region Private Function
    private void LoadScene(string NextSceneName)
    {
        StaticInfo.SceneName = NextSceneName;
        SceneManager.LoadScene("LoadingScene");
    }
    private bool CheckFileExist()
    {
        bool exist = false;
        if (File.Exists(StaticInfo.SAVE_DIRECTORY + StaticInfo.SAVE_FILE_NAME))
        {
            exist = true;
        }
        else
        {
            Directory.CreateDirectory(StaticInfo.SAVE_DIRECTORY);
            exist = true;
        }

        return exist;
    }

    #region SaveLoadFunction
    private void SaveData(StageType stageType, UpgradeScript[] Upgrade)
    {
        if (stageType == StageType.Main)
        {
            SaveMainData(Upgrade);
        }
        else if (stageType == StageType.Boss)
        {
            //SaveBossData();
        }
    }

    private void LoadData(StageType stageType, UpgradeScript[] Upgrade)
    {
        if (stageType == StageType.Main)
        {
            LoadMainData(Upgrade);
        }
        else if (stageType == StageType.Boss)
        {
            //LoadsBossData();
        }
    }
    #region Main
    private void SaveMainData(UpgradeScript[] Upgrade)
    {
        DataFormat.MainDataContainer maindataContainer = new DataFormat.MainDataContainer();
        maindataContainer.UserMoney = StaticInfo.mainDataContainer.UserMoney;
        maindataContainer.UserCoin = StaticInfo.mainDataContainer.UserCoin;
        maindataContainer.PlusMoney = StaticInfo.mainDataContainer.PlusMoney;
        maindataContainer.PlusMoneyByTime = StaticInfo.mainDataContainer.PlusMoneyByTime;

        for (int i = 0; i < Upgrade.Length; i++)
        {
            if (Upgrade[i].Name != null)
            {
                maindataContainer.commonStageData.Name.Add(Upgrade[i].Name);
                maindataContainer.commonStageData.UpgradeCost.Add(Upgrade[i].UpgradeCost);
                maindataContainer.commonStageData.UpgradeLevel.Add(Upgrade[i].UpgradeLevel);
                maindataContainer.commonStageData.IncreaseValue.Add(Upgrade[i].Increasevalue);
            }
            else break;
        }

        string data = JsonUtility.ToJson(maindataContainer);
        File.WriteAllText(StaticInfo.SAVE_DIRECTORY + StaticInfo.SAVE_FILE_NAME, data);
    }

    private void LoadMainData(UpgradeScript[] Upgrade)
    {
        if (CheckFileExist())
        {
            DataFormat.MainDataContainer mainDataContainer = new DataFormat.MainDataContainer();

            string loadData = File.ReadAllText(StaticInfo.SAVE_DIRECTORY + StaticInfo.SAVE_FILE_NAME);
            mainDataContainer = JsonUtility.FromJson<DataFormat.MainDataContainer>(loadData);

            StaticInfo.mainDataContainer.UserMoney = mainDataContainer.UserMoney;
            StaticInfo.mainDataContainer.UserCoin = mainDataContainer.UserCoin;
            StaticInfo.mainDataContainer.PlusMoneyByTime = mainDataContainer.PlusMoneyByTime;

            StaticInfo.mainDataContainer.PlusMoney = mainDataContainer.PlusMoney;

            for (int i = 0; i < mainDataContainer.commonStageData.UpgradeLevel.Count; i++)
            {
                Upgrade[i].Name = mainDataContainer.commonStageData.Name[i];
                Upgrade[i].UpgradeCost = mainDataContainer.commonStageData.UpgradeCost[i];
                Upgrade[i].UpgradeLevel = mainDataContainer.commonStageData.UpgradeLevel[i];
                Upgrade[i].Increasevalue = mainDataContainer.commonStageData.IncreaseValue[i];
            }
        }

        else Debug.Log("LoadMainData Failed");
    }
    #endregion

    #region Boss
    public void SaveBossData(BossStageUpgradeScript[] BossUpgrade)
    {
        DataFormat.BossDataContainer BossdataContainer = new DataFormat.BossDataContainer();

        BossdataContainer.currentAttackDamage = StaticInfo.bossDataContainer.currentAttackDamage;
        BossdataContainer.currentCriticalDamage = StaticInfo.bossDataContainer.currentCriticalDamage;
        BossdataContainer.currentCriticalPercent = StaticInfo.bossDataContainer.currentCriticalPercent;
        BossdataContainer.currentStage = StaticInfo.bossDataContainer.currentStage;
        BossdataContainer.givecoin = StaticInfo.bossDataContainer.givecoin;

        for (int i = 0; i < BossUpgrade.Length; i++)
        {
            if (BossUpgrade[i].Name != null)
            {
                BossdataContainer.commonStageData.Name.Add(BossUpgrade[i].Name);
                BossdataContainer.commonStageData.UpgradeCost.Add(BossUpgrade[i].UpgradeCost);
                BossdataContainer.commonStageData.UpgradeLevel.Add(BossUpgrade[i].UpgradeLevel);
                BossdataContainer.commonStageData.IncreaseValue.Add(BossUpgrade[i].Increasevalue);
            }
            else break;
        }

        string data = JsonUtility.ToJson(BossdataContainer);
        File.WriteAllText(StaticInfo.SAVE_DIRECTORY + StaticInfo.SAVE_BOSS_FILE_NAME, data);

    }

    public void LoadBossData(BossStageUpgradeScript[] BossUpgrade)
    {
        DataFormat.BossDataContainer BossdataContainer = new DataFormat.BossDataContainer();

        if (File.Exists(StaticInfo.SAVE_DIRECTORY + StaticInfo.SAVE_BOSS_FILE_NAME))
        {
            string loadData = File.ReadAllText(StaticInfo.SAVE_DIRECTORY + StaticInfo.SAVE_BOSS_FILE_NAME);
            BossdataContainer = JsonUtility.FromJson<DataFormat.BossDataContainer>(loadData);

            StaticInfo.bossDataContainer.currentAttackDamage = BossdataContainer.currentAttackDamage;
            StaticInfo.bossDataContainer.currentCriticalDamage = BossdataContainer.currentCriticalDamage;
            StaticInfo.bossDataContainer.currentCriticalPercent = BossdataContainer.currentCriticalPercent;
            StaticInfo.bossDataContainer.currentStage = BossdataContainer.currentStage;
            StaticInfo.bossDataContainer.givecoin = BossdataContainer.givecoin;

            for (int i = 0; i < BossdataContainer.commonStageData.UpgradeLevel.Count; i++)
            {
                BossUpgrade[i].Name = BossdataContainer.commonStageData.Name[i];
                BossUpgrade[i].UpgradeCost = BossdataContainer.commonStageData.UpgradeCost[i];
                BossUpgrade[i].UpgradeLevel = BossdataContainer.commonStageData.UpgradeLevel[i];
                BossUpgrade[i].Increasevalue = BossdataContainer.commonStageData.IncreaseValue[i];
            }

        }

        else
            Directory.CreateDirectory(StaticInfo.SAVE_DIRECTORY);

    }
    #endregion
    #endregion
    #endregion
}
