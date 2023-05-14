using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CAH.GameSystem.BigNumber;
using Data;

public class UpgradeScript : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI NameLeveltext;
    [SerializeField]
    private TextMeshProUGUI costtext;
    [SerializeField]
    private TextMeshProUGUI valuetext;


    [Header("Upgrade")]
    [SerializeField]
    public int UpgradeLevel;
    [SerializeField]
    public int UpgradeCost;
    [SerializeField]
    public int Increasevalue;


    public string Name = "";

    void Update()
    {
        NameLeveltext.text = string.Format(Name + " Lv. " + UpgradeLevel.ToString());
        costtext.text = string.Format("ºñ¿ë : " + MoneySizeControl.GetUnit(UpgradeCost));
        valuetext.text = string.Format("+" + MoneySizeControl.GetUnit(Increasevalue));
    }

    public void ByTimeUpgrade()
    {
        if (StaticInfo.mainDataContainer.UserMoney >= UpgradeCost)
        {
            StaticInfo.mainDataContainer.UserMoney -= UpgradeCost;
            UpgradeLevel += 1;
            UpgradeCost = UpgradeCost * 2;
            StaticInfo.mainDataContainer.PlusMoneyByTime += Increasevalue;
        }
        else return;
    }

    public void TouchUpgrade()
    {
        if (StaticInfo.mainDataContainer.UserMoney >= UpgradeCost)
        {
            StaticInfo.mainDataContainer.UserMoney -= UpgradeCost;
            UpgradeLevel += 1;
            UpgradeCost = UpgradeCost * 2;
            StaticInfo.mainDataContainer.PlusMoney += Increasevalue;
        }

        else return;
    }
}
