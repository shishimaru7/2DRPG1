using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //敵がいる状態true いないfalse
    [SerializeField]
    private BattlePlayer battlePlayer;
    [SerializeField]
    private EnemyGenerator enemyGenerator;


    [SerializeField]
    private GameObject itemScreen;
    [SerializeField]
    private GameObject skillScreen;
    [SerializeField]
    private Button attackButton;
    [SerializeField]
    private Button selectSkillButton;
    [SerializeField]
    private Button selectItemButton;
    [SerializeField]
    private Button escapeButton;
    [SerializeField]
    private Button useHealItemButton;
    [SerializeField]
    private Button useSpItemButton;
    [SerializeField]
    private Button useRescueItemButton;
    [SerializeField]
    private Button[] skillSelectButtons = new Button[1];

    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button selectSkillBackButton;
    [SerializeField]
    private Text dontSkillText;//技打てない

    [SerializeField]
    private Image resurrectionImage;//蘇生可能状態
    [SerializeField]
    private Image informationCommandImage;//使用コマンド表示
    [SerializeField]
    private Text informationCommandText;//使用コマンドテキスト表示

    [SerializeField]
    private int turnCount;//ターン数
    public int getTurnCount { get { return turnCount; } }
    private int damage;
    private bool turn = true;//ターン
    private bool battleStart = true;//戦闘開始
    private bool battleEnd = true;//戦闘終了

    private bool resurrection = false;//自動復活状態
    [SerializeField]
    private GameObject commandPanel;
    /// <summary>
    /// 敵のコマンド
    /// </summary>
    public enum EnemyCommandState
    {
        戦う,
        スキル,
    }
    public EnemyCommandState ENEMY_COMMAND_STATE;

    /// <summary>
    /// 戦うとスキル3種類の設定（enumとList型を紐づけ）
    /// </summary>
    /// <param name="enemyCommandState"></param>
    void EnemyCommandLink(EnemyCommandState enemyCommandState)
    {
        switch (enemyCommandState)
        {
            case EnemyCommandState.戦う:
                ENEMY_COMMAND_STATE = EnemyCommandState.戦う;
                StartCoroutine(NormalAttackAnimationTime());
                GetHitPlayerDamage();
                break;
            case EnemyCommandState.スキル:
                ENEMY_COMMAND_STATE = EnemyCommandState.スキル;
               Database.instance.enemyStatus.SkillNo = Random.Range(0, 3);
                Debug.Log(Database.instance.enemyStatus.SkillNo + "enemyStatus.skillNo");
                informationCommandImage.enabled = true;
                informationCommandText.text = Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].getEnemySkillList[Database.instance.enemyStatus.SkillNo].getSkillName;
                informationCommandText.enabled = true;
                StartCoroutine(SkillAttackAnimationTime());
                GetPlayerSkillDamage();
                StartCoroutine(HideCommandText());
                break;
        }
    }

    IEnumerator NormalAttackAnimationTime()
    {
        enemyGenerator.instantiateAnimator.SetTrigger("NormalAttack");
        yield return new WaitForSeconds(3f);
    }

    IEnumerator SkillAttackAnimationTime()
    {
        switch (Database.instance.enemyStatus.SkillNo)
        {
            case 0:
                enemyGenerator.instantiateAnimator.SetTrigger("Skill1");
                SoundManager.instance.EmemySkillSE(SoundManager.instance.gSE);
                yield return new WaitForSeconds(2f);
                break;
            case 1:
                enemyGenerator.instantiateAnimator.SetTrigger("Skill2");
                SoundManager.instance.EmemySkillSE(SoundManager.instance.gSE);
                yield return new WaitForSeconds(2f);
                break;
            case 2:
                enemyGenerator.instantiateAnimator.SetTrigger("Skill3");
                SoundManager.instance.EmemySkillSE(SoundManager.instance.gSE);
                yield return new WaitForSeconds(2f);
                break;
        }

    }
    /// <summary>
    /// 戦うとスキルをランダム設定
    /// </summary>
    void EnemyCommandChange()
    {
        int commandChangeCount = Random.Range(0, 2);
        Debug.Log(commandChangeCount + "commandChangeCount");
        if (commandChangeCount == 0)
        {
            EnemyCommandLink(EnemyCommandState.戦う);
        }
        else if (commandChangeCount == 1)
        {
            EnemyCommandLink(EnemyCommandState.スキル);
        }
    }

    void Start()
    {
        battleStart = true;
        turn = true;
        turnCount = 1;
        commandPanel.SetActive(true);
        itemScreen.SetActive(false);
        skillScreen.SetActive(false);
        dontSkillText.enabled = false;

        resurrectionImage.enabled = false;
        informationCommandImage.enabled = false;
        informationCommandText.enabled = false;
        ENEMY_COMMAND_STATE = EnemyCommandState.戦う;

        attackButton.onClick.AddListener(AttackSelect);
        selectSkillButton.onClick.AddListener(SkillSelect);
        selectItemButton.onClick.AddListener(ItemSelect);
        escapeButton.onClick.AddListener(EscapeSelect);
        useHealItemButton.onClick.AddListener(UseHealItem);
        useSpItemButton.onClick.AddListener(UseSpItem);
        useRescueItemButton.onClick.AddListener(UseRescueItem);
        skillSelectButtons[0].onClick.AddListener(UseFirstSkillSelect);
        skillSelectButtons[1].onClick.AddListener(UseSecondSkillSelect);
        backButton.onClick.AddListener(BackButton);
        selectSkillBackButton.onClick.AddListener(BackButton);
    }

    //戻るボタン
    public void BackButton()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        itemScreen.SetActive(false);
        skillScreen.SetActive(false);
        commandPanel.SetActive(true);
        dontSkillText.enabled = false;
    }

    public void AttackSelect()
    {
        turn = false;
        SoundManager.instance.PlayerNormalAttackSE(SoundManager.instance.gSE);
        battlePlayer.animator.SetTrigger("NormalAttack");
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        turnCount++;

        commandPanel.SetActive(false);

        GetHitEnemyDamage();
        StartCoroutine(EnemyCommand());
    }

    //アイテム選択
    public void ItemSelect()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        commandPanel.SetActive(false);

        itemScreen.SetActive(true);
    }

    IEnumerator HideCommandText()
    {
        if (informationCommandImage.enabled == true)
        {
            yield return new WaitForSeconds(1.5f);
            informationCommandImage.enabled = false;
            informationCommandText.enabled = false;
        }
    }
    //TODO 修正
    private void IsBattleUseItem(int itemNo)
    {
        if (Database.instance.playerStatus.getHaveItemList[itemNo].HaveItem == 0)
        {
            Debug.Log("満タン");
            return;
        }
        turn = false;
        turnCount++;
        SoundManager.instance.ItemeEffectSE(SoundManager.instance.gSE);
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        informationCommandImage.enabled = true;
        informationCommandText.enabled = true;
        informationCommandText.text = Database.instance.playerStatus.getHaveItemList[itemNo].getItemName;
        StartCoroutine(HideCommandText());
        itemScreen.SetActive(false);
        StartCoroutine(EnemyCommand());
    }
    public void UseHealItem()
    {
        int itemNo = 0;
        IsBattleUseItem(itemNo);
        Database.instance.playerStatus.UseItemEffect(itemNo);


        GameObject haelEffect = Instantiate(Database.instance.getHealEffect, new Vector3(4f, -1f, 100f), Quaternion.identity);

        Destroy(haelEffect, 1f);
    }


    public void UseSpItem()
    {
        int itemNo = 1;
        IsBattleUseItem(itemNo);
        Database.instance.playerStatus.UseItemEffect(itemNo);


        GameObject spEffect = Instantiate(Database.instance.getSpEffect, new Vector3(4f, -2f, 100f), Quaternion.identity);

        Destroy(spEffect, 1f);
    }
    public void UseRescueItem()
    {
        int itemNo = 2;
        IsBattleUseItem(itemNo);
        Database.instance.playerStatus.UseItemEffect(itemNo);


        GameObject rescueEffect = Instantiate(Database.instance.getRescueEffect, new Vector3(4f, 0f, 100f), Quaternion.identity);

        Destroy(rescueEffect, 1f);
        resurrection = true;
        resurrectionImage.enabled = true;
    }
    /// <summary>
    /// 自動再生状態
    /// </summary>
    void Resurrection()
    {
        if (Database.instance.playerStatus.HP <= 0 && resurrection == true)
        {
            Database.instance.playerStatus.UseRescueItem();

            resurrection = false;
            resurrectionImage.enabled = false;
        }
    }
    //ボタン操作
    public void SkillSelect()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        commandPanel.SetActive(false);

        skillScreen.SetActive(true);
        for (int i = 0; i < skillSelectButtons.Length; i++)
        {
            if (Database.instance.playerStatus.getSkillList[i].SkillGet)
            {
                skillSelectButtons[i].interactable = true;
                continue;
            }
            skillSelectButtons[i].interactable = false;
        }
    }
    /// <summary>
    /// SkillButton押下で動く処理
    /// </summary>
    /// <param name="skillNo"></param>
    private void UseSkill(int skillNo)
    {

        if (Database.instance.playerStatus.SP < Database.instance.playerStatus.getSkillList[skillNo].getConsumptionSp)
        {
            dontSkillText.enabled = true;
            dontSkillText.text = "技が出ない";
            return;
        }
        else
        {
            dontSkillText.enabled = false;
        }
        turn = false;
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        turnCount++;
        informationCommandImage.enabled = true;
        informationCommandText.enabled = true;
        informationCommandText.text = Database.instance.playerStatus.getSkillList[skillNo].SkillName;

        Database.instance.playerStatus.SP -= Database.instance.playerStatus.getSkillList[skillNo].getConsumptionSp;
        SoundManager.instance.PlayerSkillAttackSE(SoundManager.instance.gSE);
        battlePlayer.animator.SetTrigger("Skill1");
        Debug.Log(Database.instance.playerStatus.SP + "playerStatus.sp");
        StartCoroutine(HideCommandText());
        skillScreen.SetActive(false);
        commandPanel.SetActive(false);

        StartCoroutine(EnemyCommand());
    }
    //ボタン押下でスキル発動
    private void UseFirstSkillSelect()
    {
        int skillNo = 0;
        UseSkill(skillNo);
        PlayerSkillDamage(skillNo);
    }
    //ボタン押下でスキル発動
    private void UseSecondSkillSelect()
    {
        int skillNo = 1;
        UseSkill(skillNo);
        PlayerSkillDamage(skillNo);
    }

    //逃げる選択
    public void EscapeSelect()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gSE);
        SceneChange.instance.SceneChangeType(SCENE_TYPE.ActionStage);
    }

    //敵の攻撃
    IEnumerator EnemyCommand()
    {
        //敵の行動
        if (turn == false)
        {
            turn = true;
            //勝利時
            if (Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP <= 0)
            {
                BattleEnd();
            }
            else
            {//戦闘中
                yield return new WaitForSeconds(4f);
                EnemyCommandChange();
                commandPanel.SetActive(true);
                StartCoroutine(PlayerGameOver());
            }
        }
    }
    /// <summary>
    /// 敵→Playerへの通常攻撃、ダメージの内容
    /// </summary>
    /// <param name="defense"></param>
    /// <returns></returns>
    public int GetPlayerDamageContents(int defense)
    {
        int damage = Mathf.Clamp(Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].AttackPower - defense, 0, Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].AttackPower);
        return damage;

    }
    /// <summary>
    /// 敵→Playerへの通常攻撃、HPダメージ
    /// </summary>
    public void GetHitPlayerDamage()
    {
        battlePlayer.animator.SetTrigger("GetHit");
        int damage = GetPlayerDamageContents(Database.instance.playerStatus.DefensePower);
        Database.instance.playerStatus.HP -= damage;
        Debug.Log("通常攻撃" +damage+"受けるHPは" +Database.instance.playerStatus.HP);
    }
    //敵→Playerへのスキルダメージ計算式、「敵のランダムに選ばれたスキル」の「スキル攻撃力」ープレイヤー守備力　守備高いと0にもなる
    public int GetPlayerSkillDamageContents(int defense)
    {
        int getSkillDamage = Mathf.Clamp(Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].getEnemySkillList[Database.instance.enemyStatus.SkillNo].SkillPower - defense, 0, Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].getEnemySkillList[Database.instance.enemyStatus.SkillNo].SkillPower);
        return getSkillDamage;
    }
    //敵→プレイヤへのスキル攻撃
    public void GetPlayerSkillDamage()
    {
        battlePlayer.animator.SetTrigger("GetHit");
        int getHitSkillDamage = GetPlayerSkillDamageContents(Database.instance.playerStatus.DefensePower);
        Database.instance.playerStatus.HP -= getHitSkillDamage;
        Debug.Log("スキル攻撃" + getHitSkillDamage + "受けるHPは" + Database.instance.playerStatus.HP);
    }

    //プレイヤーのスキル→敵へのダメージ
    private void PlayerSkillDamage(int skillNo)
    {
        Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP -= Database.instance.playerStatus.getSkillList[skillNo].getSkillPower;
        Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP = Mathf.Clamp(Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP, 0, Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP);
    }

    /// <summary>
    /// Player→敵への通常攻撃のダメージ内容
    /// </summary>
    /// <param name="defense"></param>
    /// <returns></returns>
    public int HitEnemyDamageContents(int defense)
    {
        damage = Mathf.Clamp(Database.instance.playerStatus.AttackPower - defense, 0,Database.instance.playerStatus.AttackPower);
        return damage;
    }
    /// <summary>
    /// Player→敵への通常攻撃、HPダメージ
    /// </summary>
    public void GetHitEnemyDamage()
    {
        damage = HitEnemyDamageContents(Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].DefensePower);
        Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP -= damage;
        Debug.Log(Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].getEnemyName +"に"+damage+"与えたHPは"+ Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP);
    }

    /// <summary>
    /// バトル終了と経験値とお金の取得
    /// コルーチンでステージシーンへの遷移
    /// </summary>
    public void BattleEnd()
    {
        if (Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].HP <= 0)
        {
            turn = false;
            battleEnd = true;

            commandPanel.SetActive(false);

            if (Database.instance.IsBossFlag == true)
            {
                StartCoroutine(ChangeClearScene());
            }
            else
            {
                StartCoroutine(Result());
            }
        }
    }

    /// <summary>
    // 経験値とお金をplayerstatusに渡す
    /// </summary>
    IEnumerator Result()
    {

        //スクリタブルのメソッド使う（経験値とお金を貰い保存）
        Database.instance.playerStatus.GetResult(Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].Exp, Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].Gold);

        informationCommandImage.enabled = true;
        informationCommandText.text = "EXP" + Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].Exp + "獲得";
        informationCommandText.enabled = true;
        yield return new WaitForSeconds(1f);
        informationCommandText.text = "GOLD" + Database.instance.enemyStatus.getEnemyList[Database.instance.enemyStatus.EnemyNo].Gold + "金獲得";
        yield return new WaitForSeconds(1f);
        if (Database.instance.playerStatus.getSkillList[0].SkillGet == true && Database.instance.SKillCheckConut == 0)
        {
            Database.instance.SKillCheckConut++;
            informationCommandText.text = Database.instance.playerStatus.getSkillList[0].SkillName + "習得";
        }
        if (Database.instance.playerStatus.getSkillList[1].SkillGet == true && Database.instance.SKillCheckConut == 1)
        {
            Database.instance.SKillCheckConut++;
            informationCommandText.text = Database.instance.playerStatus.getSkillList[1].SkillName + "習得";
        }
        yield return new WaitForSeconds(3f);

        SceneChange.instance.SceneChangeType(SCENE_TYPE.ActionStage);
    }

    IEnumerator ChangeClearScene()
    {
        yield return new WaitForSeconds(3f);

        SceneChange.instance.SceneChangeType(SCENE_TYPE.GameClear);
    }

    /// <summary>
    /// プレイヤー死亡処理後シーン遷移
    /// </summary>
    IEnumerator PlayerGameOver()
    {
        Resurrection();
        if (Database.instance.playerStatus.HP <= 0)
        {
            battlePlayer.animator.SetTrigger("Die");

            commandPanel.SetActive(false);
            Database.instance.IsBossFlag = false;
            yield return new WaitForSeconds(4f);
            SceneChange.instance.SceneChangeType(SCENE_TYPE.GameOver);
        }
    }
}