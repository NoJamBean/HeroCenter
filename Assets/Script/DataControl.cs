using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Data;

public class DataControl : MonoBehaviour
{
    [SerializeField]
    private UpgradeScript[] Upgrade;
    

    private void Awake()
    {
        for (int i = 0; i < Upgrade.Length; i++)
        {
            Upgrade[i] = Upgrade[i].GetComponent<UpgradeScript>();
        }
        
    }
}
