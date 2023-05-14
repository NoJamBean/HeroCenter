using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBossStageScript : MonoBehaviour
{
    public string NextSceneName = "";
    public static LoadingBossStageScript instance;

    [SerializeField]
    private Slider loadingBar;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(instance);
    }

    private void Start()
    {
        loadingBar.value = 0;
        StartCoroutine(SaveAndLoadAsyncScene(NextSceneName, DataLoadAction));
    }

    private void DataLoadAction()
    {
        //GameManager._I.SaveBossData();
        //GameManager._I.SaveMainData();
        //GameManager._I.LoadBossData();
        //GameManager._I.LoadMainData(Upgrade);
    }
   

    private IEnumerator SaveAndLoadAsyncScene(string targetSceneName, Action action)
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(targetSceneName);
        asyncScene.allowSceneActivation = false;
        float timeC = 0;
        while (!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;
            if (asyncScene.progress >= 0.9f)
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1, timeC);
                if (loadingBar.value == 1.0f)
                {
                    asyncScene.allowSceneActivation = true;
                }
            }

            else
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, asyncScene.progress, timeC);
                if (loadingBar.value >= asyncScene.progress)
                {
                    timeC = 0f;
                }
            }
        }
        action();
        gameObject.SetActive(false);
    }

}
