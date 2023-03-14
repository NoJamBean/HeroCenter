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
    private BossStageDataControl bossDataControl;
    private MainDataControl mainDataControl;

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
        StartCoroutine(SaveAndLoadAsyncScene());
    }

    private IEnumerator SaveAndLoadAsyncScene()
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(NextSceneName);
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
        bossDataControl = FindObjectOfType<BossStageDataControl>();
        mainDataControl = FindObjectOfType<MainDataControl>();
        bossDataControl.SaveData();
        mainDataControl.SaveData();
        bossDataControl.LoadData();
        mainDataControl.LoadData();
        gameObject.SetActive(false);
    }

}
