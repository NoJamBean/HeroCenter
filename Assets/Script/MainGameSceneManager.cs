using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CAH.GameSystem.BigNumber;
using Data;

public class MainGameSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneytext;
    [SerializeField]
    private TextMeshProUGUI cointext;

    void Start()
    {
        StartCoroutine(GameManager._I.MoneyIncreaseByTime());
    }

    // Update is called once per frame
    void Update()
    {
        moneytext.text = MoneySizeControl.GetUnit(StaticInfo.UserMoney);
        cointext.text = StaticInfo.UserCoin.ToString();
    }


    #region Upgrade

    #endregion
}
