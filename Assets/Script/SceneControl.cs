using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public string SceneName="";
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        
       
    }
    public void GameStart()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }

    
    
}
