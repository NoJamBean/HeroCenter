using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CAH.GameSystem.BigNumber;

public class BossStageUIControl : MonoBehaviour
{
    private GameObject GameManager;

    [Header("Text")]
    [SerializeField]
    private TextMeshProUGUI attackDamageText;
    [SerializeField]
    private TextMeshProUGUI criticalDamageText;
    [SerializeField]
    private TextMeshProUGUI criticalPercentText;
    [SerializeField]
    private TextMeshProUGUI bossHPText;
    [SerializeField]
    private TextMeshProUGUI StageText;
    [SerializeField]
    private TextMeshProUGUI timeText;

    [Header("Slider")]
    [SerializeField]
    private Slider bossHPSlider;
    [SerializeField]
    private Slider TimeSlider;
    

    private SceneControl sceneControl;

    private int currentAttackDamage_ = 10;
    private float currentCriticalDamage_ = 200;
    private float currentCriticalPercent_ = 10;

    public int currentStage = 1;
    private int givecoin_ = 1;
    public int givecoin
    {
        get { return givecoin_; }
        set { givecoin_ = value; }
    }

    private float BossMaxHP = 100;
    [SerializeField]
    private float BosscurrentHP;

    private float MaxTime = 30;
    private float currentTime = 0.00f;

    public int currentAttackDamage
    {
        get { return currentAttackDamage_; }
        set { currentAttackDamage_ = value; }
    }

    public float currentCriticlaDamge
    {
        get { return currentCriticalDamage_; }
        set { currentCriticalDamage_ = value; }
    }

    public float currentCriticlaPercent
    {
        get { return currentCriticalPercent_; }
        set { currentCriticalPercent_ = value; }
    }

    private void Awake()
    {
        GameManager = GameObject.Find("GameManager");
    }
    private void Start()
    {
        sceneControl = GetComponent<SceneControl>();

        BosscurrentHP = BossMaxHP;
        currentTime = MaxTime;

        StartCoroutine("Attack", 1);
    }

    private void Update()
    {
        TextOutPut();

        UpdateBossHpSlider();
        NextStage();

        TimeCountDown();

    }

    private void TextOutPut()
    {
        attackDamageText.text = string.Format(MoneySizeControl.GetUnit(currentAttackDamage_).ToString());
        criticalDamageText.text = string.Format(currentCriticlaDamge.ToString() + "%");
        criticalPercentText.text = string.Format(currentCriticlaPercent.ToString() + "%");

        bossHPText.text = string.Format(BosscurrentHP.ToString() + "/" + BossMaxHP.ToString());
        StageText.text = string.Format("Stage : " + currentStage.ToString());

        timeText.text = string.Format("{0:F1}" + "/" + MaxTime.ToString(), currentTime);
    }

    public float AttackDamageCalculate()
    {
        IsCritical();
        if (IsCritical() == true)
        {
            float attackDamage = currentAttackDamage_ * (float)(currentCriticalDamage_ / 100);
            return attackDamage;
        }
        else return currentAttackDamage_;
        
    }

    private bool IsCritical()
    {
        int RandomValue = Random.Range(0, 999);
        float currentPercent = currentCriticalPercent_ * 10;
        if (RandomValue >= 0 || RandomValue < currentPercent)
        {
            return true;
        }
        else if(currentPercent ==1000)
        {
            return true;
        }
        else return false;
    }


    

    private IEnumerator Attack(int delayTime)
    {
        BosscurrentHP -= AttackDamageCalculate();

        yield return new WaitForSeconds(delayTime);
        StartCoroutine("Attack", 1);
    }



    private void NextStage()
    {
        if (isClear() == true)
        {
            BossMaxHP = BossMaxHP + 100;
            BosscurrentHP = BossMaxHP;
            currentStage++;
            Data.StaticInfo.UserMoney += 10000;
            Data.StaticInfo.UserCoin += givecoin_;
            givecoin_ = givecoin_ +3;
            currentTime = MaxTime;
        }
        else return;
    }
    private bool isClear()
    {
        if (BosscurrentHP <= 0)
        {
            return true;
        }
        else return false;
    }

    private bool isFail()
    {
        if (currentTime <= 0 && BosscurrentHP > 0)
        {
            return true;
        }
        else return false;
    }

    private void UpdateBossHpSlider()
    {
        bossHPSlider.value = ((float)BosscurrentHP / (float)BossMaxHP);
    }

    private void TimeCountDown()
    {
        TimeSlider.value = currentTime / MaxTime;
        currentTime -= Time.deltaTime;
    }

    private void BackToMainScene()
    {
        if (isFail() == true)
        {
            sceneControl.GameStart();
        }
        else return;
    }
}
