using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonTouchScript : MonoBehaviour
{
    

    void Update()
    {
        ButtonDown();
        

    }

    public void ButtonDown()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            Debug.Log(Data.StaticInfo.mainDataContainer.UserMoney);
            Debug.Log("��ư����");

            return;
        }
        else
            return;
    }

   
}
