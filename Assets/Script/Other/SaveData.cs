using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField]
    private int sceneNo;
    [SerializeField]
    private SCENE_TYPE sceneType;

    private string presentvalueLevelKey = "presentvalueLevel";//Level情報
    private string presentvalueEXPStockKey = "presentvalueEXPStock";//所持経験値
    private string presentvaluenextlevelpointKey = "presentvaluenextlevelpoint";//次のレベルまでの値
    private string presentvaluegoldKey = "presentvaluegold";//所持金

    private string presentvaluePlayerHpKey = "presentvaluePlayerHp";//現在HP
    private string presentvaluePlayermaxHpKey = "presentvaluePlayermaxHp";//現在最大HP
    private string presentvaluePlayerSpKey = "presentvaluePlayerSpKey";//現在SP
    private string presentvaluePlayermaxSpKey = "presentvaluePlayermaxSpKey";//現在最大SP

    private string presentvaluePlayerAttackpowerKey = "presentvaluePlayerAttackpower";//攻撃力
    private string presentvaluePlayerDefenseKey = "presentvaluePlayerDefense";//防御力

    private string presentvalueHaveHealItemKey = "presentvalueHaveHealItem";//回復アイテム所持数
    private string presentvalueHaveSpItemKey = "presentvalueHaveSpItem";//SP回復アイテム所持数
    private string presentvalueHaveRescueItemKey = "presentvalueHaveRescueItem";//Rescueアイテム所持数
    private string getSklii1Key = "getSklii1";//Skill1取得
    private string getSklii2Key = "getSklii2";//Skill2取得

    private string currentAreakey = "currentArea";//現在のシーン
    private string currentPositionkey = "currentPosition";//現在の座標

    /// <summary>
    /// UIのButtonでセーブデータセーブ
    /// </summary>
    public void Save()
    {
        PlayerPrefs.SetInt(presentvalueLevelKey,Database.instance.playerStatus.Level);
        PlayerPrefs.SetInt(presentvalueEXPStockKey, Database.instance.playerStatus.ExpStock);
        PlayerPrefs.SetInt(presentvaluenextlevelpointKey, Database.instance.playerStatus.NextLevelPoint);
        PlayerPrefs.SetInt(presentvaluegoldKey, Database.instance.playerStatus.GoldStock);

        PlayerPrefs.SetInt(presentvaluePlayerHpKey, Database.instance.playerStatus.HP);
        PlayerPrefs.SetInt(presentvaluePlayermaxHpKey, Database.instance.playerStatus.MaxHP);
        PlayerPrefs.SetInt(presentvaluePlayerSpKey, Database.instance.playerStatus.SP);
        PlayerPrefs.SetInt(presentvaluePlayermaxSpKey, Database.instance.playerStatus.MaxSP);

        PlayerPrefs.SetInt(presentvaluePlayerAttackpowerKey, Database.instance.playerStatus.AttackPower);
        PlayerPrefs.SetInt(presentvaluePlayerDefenseKey, Database.instance.playerStatus.DefensePower);

        PlayerPrefs.SetInt(presentvalueHaveHealItemKey, Database.instance.playerStatus.getHaveItemList[0].HaveItem);
        PlayerPrefs.SetInt(presentvalueHaveSpItemKey, Database.instance.playerStatus.getHaveItemList[1].HaveItem);
        PlayerPrefs.SetInt(presentvalueHaveRescueItemKey, Database.instance.playerStatus.getHaveItemList[2].HaveItem);

        SetPlayerPrefsBool(getSklii1Key, Database.instance.playerStatus.getSkillList[0].SkillGet);//boolの保存
        SetPlayerPrefsBool(getSklii1Key, Database.instance.playerStatus.getSkillList[1].SkillGet);

        PlayerPrefs.SetInt(currentAreakey, (int)sceneType);//シーンエリアのセーブ
        SetPlayerPrefsVector3(currentPositionkey, Database.instance.SavePointPosition);//シーンエリアの座標
        Database.instance.IsSavePoint = false;
        Debug.Log("Saveしました" + Database.instance.SavePointPosition + "currentPositionkey" + (int)sceneType + " currentAreakey");
        PlayerPrefs.Save();
        Time.timeScale = 1;

        Debug.Log("Saveしました" + "SceneStateType" + sceneNo);
        GameObject.Find("SavePoint").GetComponent<SavePoint>().saveScreen.SetActive(false);
    }
    /// <summary>
    /// UIのButtonでセーブデータロード
    /// </summary>
    public void RoadData()
    {
        Database.instance.IsBossFlag = false;
        Database.instance.playerStatus.Level = PlayerPrefs.GetInt(presentvalueLevelKey);
        Database.instance.playerStatus.ExpStock = PlayerPrefs.GetInt(presentvalueEXPStockKey);
        Database.instance.playerStatus.NextLevelPoint = PlayerPrefs.GetInt(presentvaluenextlevelpointKey);
        Database.instance.playerStatus.GoldStock = PlayerPrefs.GetInt(presentvaluegoldKey);

        Database.instance.playerStatus.HP = PlayerPrefs.GetInt(presentvaluePlayerHpKey);
        Database.instance.playerStatus.MaxHP = PlayerPrefs.GetInt(presentvaluePlayermaxHpKey);
        Database.instance.playerStatus.SP = PlayerPrefs.GetInt(presentvaluePlayerSpKey);
        Database.instance.playerStatus.MaxSP = PlayerPrefs.GetInt(presentvaluePlayermaxSpKey);

        Database.instance.playerStatus.AttackPower = PlayerPrefs.GetInt(presentvaluePlayerAttackpowerKey);
        Database.instance.playerStatus.DefensePower = PlayerPrefs.GetInt(presentvaluePlayerDefenseKey);

        Database.instance.playerStatus.getHaveItemList[0].HaveItem = PlayerPrefs.GetInt(presentvalueHaveHealItemKey);
        Database.instance.playerStatus.getHaveItemList[1].HaveItem = PlayerPrefs.GetInt(presentvalueHaveSpItemKey);
        Database.instance.playerStatus.getHaveItemList[2].HaveItem = PlayerPrefs.GetInt(presentvalueHaveRescueItemKey);

        Database.instance.playerStatus.getSkillList[1].SkillGet = GetPlayerPrefsBool(getSklii1Key, true);
        Database.instance.playerStatus.getSkillList[1].SkillGet = GetPlayerPrefsBool(getSklii1Key, true);

        sceneNo = PlayerPrefs.GetInt(currentAreakey, (int)sceneType);//セーブシーンエリアのロード

        SceneChange.instance.SceneChangeType((SCENE_TYPE)sceneNo);//セーブシーンエリアへの遷移

        GetPlayerPrefsVector3(currentPositionkey, Database.instance.SavePointPosition);//セーブシーンエリアの座標
        Database.instance.IsSavePoint = true;
        Debug.Log("Roadしました" + Database.instance.SavePointPosition + "currentPositionkey" + (int)sceneType + " currentAreakey");
        Debug.Log("ロードしました" + "SceneStateType" + sceneNo);

        Time.timeScale = 1;

    }
    /// <summary>
    /// Vector3の保存
    /// </summary>
    /// <param name="keyName"></param>
    /// <param name="Value"></param>
    public void SetPlayerPrefsVector3(string keyName, Vector3 Value)
    {
        //PlayerPrefsの値をセット
        PlayerPrefs.SetString(keyName, Value.x + "," + Value.y + "," + Value.z);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// Vector3のロード
    /// </summary>
    /// <param name="keyName"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public Vector3 GetPlayerPrefsVector3(string keyName, Vector3 defaultValue)
    {
        //PlayerPrefsのキーが存在するかチェックして、あれば値を返し、無ければキーを作成。
        //PlayerPrefsにVector3型が入れられないのでカンマ区切りでxyzを保存する
        if (!PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetString(keyName,
                defaultValue.x.ToString() + "," +
                defaultValue.y.ToString() + "," +
                defaultValue.z.ToString()
            );
            PlayerPrefs.Save();
        }
        //Splitで分割してベタにVector3のXYZに代入
        string[] Vec3 = PlayerPrefs.GetString(keyName).Split(',');
        return new Vector3(float.Parse(Vec3[0]), float.Parse(Vec3[1]), float.Parse(Vec3[2]));
    }

    public void SetPlayerPrefsBool(string keyName, bool setValue)
    {
        //PlayerPrefsの値をセット
        int boolPara = (setValue == true) ? 0 : -1;
        PlayerPrefs.SetInt(keyName, boolPara);
        PlayerPrefs.Save();
    }
    public static bool GetPlayerPrefsBool(string keyName, bool defaultValue)
    {
        //PlayerPrefsのキーが存在するかチェックして、あれば値を返し、無ければキーを作成。
        //PlayerPrefsにBool値が入れられないので-1はfalse、0はtrue。それ以外はfalseに設定。
        int boolPara = (defaultValue == true) ? 0 : -1;

        if (!PlayerPrefs.HasKey(keyName))
        {
            PlayerPrefs.SetInt(keyName, boolPara);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt(keyName) == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}