using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CAH.GameSystem.BigNumber;
using Data;

public class BossStageUpgradeScript : MonoBehaviour
{
    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI NameLeveltext;
    [SerializeField]
    private TextMeshProUGUI costtext;
    [SerializeField]
    private TextMeshProUGUI valuetext;

    [SerializeField]
    private BossStageUIControl bossStageUIControl;

    [Header("Upgrade")]
    [SerializeField]
    private int UpgradeLevel_;
    [SerializeField]
    private int UpgradeCost_;
    [SerializeField]
    private float Increasevalue_;


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
    public float Increasevalue
    {
        get { return Increasevalue_; }
        set { Increasevalue_ = value; }
    }

    public string Name = "";


    public void Start()
    {

    }

    void Update()
    {
        NameLeveltext.text = string.Format(Name + " Lv. " + UpgradeLevel.ToString());
        costtext.text = string.Format("ºñ¿ë: " + MoneySizeControl.GetUnit(UpgradeCost));
        valuetext.text = string.Format( "+" + Increasevalue);
    }

    public void AttacDamageUpgrade()
    {
        bossStageUIControl.currentAttackDamage += 10;
        UpgradeLevel_ += 1;
        if (StaticInfo.UserMoney < UpgradeCost) { return; }
        else StaticInfo.UserCoin -= UpgradeCost;
    }

    public void CriticalDamageUpgrade()
    {
        bossStageUIControl.currentCriticlaDamge += 0.5f;
        UpgradeLevel_ += 1;
        if (StaticInfo.UserCoin < UpgradeCost) { return; }
        else StaticInfo.UserCoin -= UpgradeCost;
    }

    public void CriticalPercentUpgrade()
    {
        bossStageUIControl.currentCriticlaPercent += 0.5f;
        UpgradeLevel_ += 1;
        if(StaticInfo.UserCoin < UpgradeCost) { return; }
        else StaticInfo.UserCoin -= UpgradeCost;


    }
}
