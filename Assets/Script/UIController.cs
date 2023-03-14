using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject GameObject;
    private bool state;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject.SetActive(false);
        state = false;
    }

    void Update()
    {
       
        
    }

    private void OnDisable()
    {
        state = false;
    }

    public void UISetActive()
    {
        if (state ==false)
        {
            
            GameObject.SetActive(true);
            state = true;
        }

        else if (state == true)
        {
            UISetDeactive();
        }
    }

    public void UISetDeactive()
    {
        GameObject.SetActive(false);
        state = false;
    }

   

    

    


}


