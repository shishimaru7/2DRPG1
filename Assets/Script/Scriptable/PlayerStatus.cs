using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// プレイヤー情報クラス
/// </summary>
[CreateAssetMenu(fileName = "PlayerStatus", menuName = "Scriptable/PlayerStatus")]
public class PlayerStatus : ScriptableObject
{
    //TODO 確認

    [SerializeField]
    private int hp;
    public int HP { set { hp = value; hp = Mathf.Clamp(hp, 0, maxHp);  }get { return hp; } }
    [SerializeField]
    private int maxHp;
    public int MaxHP { set { maxHp = value; } get { return maxHp; } }
    [SerializeField]
    private int sp;
    public int SP { set {sp = value; sp = Mathf.Clamp(sp, 0, maxSp); } get { return sp; } }
    [SerializeField]
    private int maxSp;
    public int MaxSP { set { maxSp = value; }get{ return maxSp; } }
    [SerializeField]
    private int attackPower;
    public int AttackPower { set { attackPower = value; } get { return attackPower; } }
    [SerializeField]
    private int defensePower;
    public int DefensePower { set { defensePower = value; } get { return defensePower; } }
    private int itemStock = 99;
    public int ItemStock { set { itemStock = Mathf.Clamp(value,0,itemStock); itemStock = value; } get { return itemStock; } }

    [SerializeField]
    private int level;
    public int Level { set { level = Mathf.Clamp(value,1,99);level = value; } get { return level; } }
    private int nextLevelPoint;
    public int NextLevelPoint { set { nextLevelPoint = value; } get { return nextLevelPoint; } }
    [SerializeField]
    private int expStock;
    public int ExpStock { set { expStock = value; } get { return expStock; } }
    [SerializeField]
    private int goldStock;
    public int GoldStock { set {goldStock = Mathf.Clamp(value,0,9999);  goldStock = value; } get { return goldStock; } }

    [Header("スキル")]
    [SerializeField]
    private List<Skill> skillList = new List<Skill>();
    public List<Skill> getSkillList { get { return skillList; } }
    [Header("所持アイテム")]
    [SerializeField]
    private List<Items> haveItemList = new List<Items>();
    public List<Items> getHaveItemList { get { return haveItemList; } }

    [Serializable]
    public class Skill
    {
        [Header("技名")]
        [SerializeField]
        private string skillName;
        public string SkillName { set { skillName = value; } get { return skillName; } }

        [Header("スキル修得中かどうか")]
        [SerializeField]
        private bool skillGet;
        public bool SkillGet { set { skillGet = value; } get { return skillGet; } }

        [Header("消費SP")]
        [SerializeField]
        private int consumptionSp;
        public int getConsumptionSp { get { return consumptionSp; } }

        [Header("威力")]
        [SerializeField]
        private int skillPower;
        public int getSkillPower { get { return skillPower;  } }
    }

    [Serializable]
    public class Items
    {
        [Header("アイテム名称")]
        [SerializeField]
        private string itemName;
        public string getItemName { get { return itemName; } }
        [Header("アイテム所持数")]
        [SerializeField]
        private int haveItem;
        public int HaveItem { set { haveItem = value; Mathf.Clamp(value, 0, 99);  } get { return haveItem; } }
        [Header("アイテム効果、回復量")]
        [SerializeField]
        private int effectPoint;
        public int EffectPoint { set { effectPoint = value; } get { return effectPoint; } }
    }

    public void UseItemEffect(int itemNo)
    {
        if (haveItemList[itemNo].HaveItem == 0)
        {
            Debug.Log("満タン");
            return;
        }
        if (maxHp >= hp&&itemNo == 0)
        {
            haveItemList[0].HaveItem --;
            haveItemList[0].EffectPoint = maxHp/2;
            Debug.Log("hp" + haveItemList[0].EffectPoint + "回復");
             hp += haveItemList[0].EffectPoint;
            hp = Mathf.Clamp(hp,0,maxHp);//無いと範囲外回復
        }
        if (maxSp >= sp &&itemNo == 1)
        {
            haveItemList[1].HaveItem --;
            haveItemList[1].EffectPoint = 50;
            Debug.Log("sp" + haveItemList[1].EffectPoint + "回復");
            sp += haveItemList[1].EffectPoint;
            sp = Mathf.Clamp(sp,0,maxSp);
        }
        if (itemNo == 2)
        {
            haveItemList[2].HaveItem--;
        }
        SoundManager.instance.ItemeEffectSE(SoundManager.instance.gBGM);
    }


    public void UseRescueItem()
    {
        haveItemList[2].EffectPoint = maxHp;
        Debug.Log("再生hp" + haveItemList[2].EffectPoint + "回復");
        hp += haveItemList[2].EffectPoint;
    }

    /// <summary>
    /// BattleManagerから経験値とお金貰う
    /// </summary>
    /// <param name="exp"></param>
    /// <param name="goid"></param>
    public void GetResult(int exp, int goid)
    {
        expStock += exp;
        goldStock += goid;

        LevelUp();
    }
    /// <summary>
    ///レベルアップの能力値
    /// </summary>
    public void LevelUp()
    {
        if (nextLevelPoint <= expStock)
        {
            level += 1;
            nextLevelPoint = Mathf.RoundToInt(nextLevelPoint * 2f);
            maxHp = Mathf.RoundToInt(maxHp * 1.3f);
            maxSp = Mathf.RoundToInt(maxSp * 1.3f);
            attackPower = Mathf.RoundToInt(attackPower * 1.5f);
            defensePower = Mathf.RoundToInt(defensePower * 1.5f);
            GetSkill();
        }
    }

    //スキル修得
    public void GetSkill()
    {
        switch (level)
        {
            case 5:
                skillList[0].SkillGet = true;
                Debug.Log(skillList[0].SkillName + "修得");
                break;
            case 10:
                skillList[1].SkillGet = true;
                Debug.Log(skillList[1].SkillName + "修得");
                break;
        }
    }
    public void PossessionHaelItem(ItemShop itemShop,int itemNo)
    {
        haveItemList[itemNo].HaveItem += 1;
        goldStock -= itemShop.shopItemPrice[itemNo];
    }
}