using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using CAH.GameSystem.BigNumber;
using TMPro;
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

    public int PlusMoneyByTime;
    public int PlusMoney;

    WaitForSeconds wfs1 = new WaitForSeconds(1);

    public void MoneyIncrease()
    {
        StaticInfo.UserMoney += 100 + PlusMoney;

    }

    public IEnumerator MoneyIncreaseByTime()
    {
        StaticInfo.UserMoney += PlusMoneyByTime; 

        yield return wfs1;
    }
}
