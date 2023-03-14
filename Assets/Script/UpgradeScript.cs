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
    private int UpgradeLevel_;
    [SerializeField]
    private int UpgradeCost_;
    [SerializeField]
    private int Increasevalue_;


    public int UpgradeLevel
    {
        get { return UpgradeLevel_; }
        set { UpgradeLevel_ = value; }
    }
    public int UpgradeCost
    {
        get { return UpgradeCost_; }
        set { UpgradeCost_ = value; }
    }
    public int Increasevalue
    {
        get { return Increasevalue_; }
        set { Increasevalue_ = value; }
    }

    public string Name = "";



    void Update()
    {
        NameLeveltext.text = string.Format(Name + " Lv. " + UpgradeLevel.ToString());
        costtext.text = string.Format("ºñ¿ë : " + MoneySizeControl.GetUnit(UpgradeCost));
        valuetext.text = string.Format("+" + MoneySizeControl.GetUnit(Increasevalue));
    }

    public void ByTimeUpgrade()
    {
        if (StaticInfo.UserMoney >= UpgradeCost_)
        {
            StaticInfo.UserMoney -= UpgradeCost_;
            UpgradeLevel_ += 1;
            UpgradeCost_ = UpgradeCost_ * 2;
            Data.StaticInfo.PlusMoneyByTime += Increasevalue_;
        }
        else return;
    }

    public void TouchUpgrade()
    {
        if (StaticInfo.UserMoney >= UpgradeCost_)
        {
            StaticInfo.UserMoney -= UpgradeCost_;
            UpgradeLevel_ += 1;
            UpgradeCost_ = UpgradeCost_ * 2;
            Data.StaticInfo.PlusMoney += Increasevalue_;
        }

        else return;
    }
}
