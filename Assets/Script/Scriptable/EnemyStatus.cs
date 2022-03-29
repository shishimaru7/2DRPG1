using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyStatus", menuName = "Scriptable/EnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField]
    private int enemyNo;
    public int EnemyNo { set { enemyNo = value; } get { return enemyNo; } }
    [SerializeField]
    private int skillNo;
    public int SkillNo { set { skillNo = value; } get { return skillNo; } }
    [Header("boolでScriptableObjectを使用テスト bossオブジェクトに接触でON")]
    public bool isLastBossFlag;

    [SerializeField]
    private List<Enemys> enemyList = new List<Enemys>();
    public List<Enemys> getEnemyList  { get{ return enemyList; } }

    [System.Serializable]
    public class Enemys
    {
        [SerializeField]
        private string enemyName;
        public string getEnemyName { get { return enemyName; } }
        [SerializeField]
        private int hp;
        public int HP { set { hp = value;if (hp<=0) { hp = 0; } } get { return hp; } }
        [SerializeField]
        private int maxHp;
        public int MaxHP { set { maxHp = value; } get { return maxHp; } }
        [SerializeField]
        private int attackPower;
        public int AttackPower { set { attackPower = value; } get { return attackPower; } }
        [SerializeField]
        private int defensePower;
        public int DefensePower { set { defensePower = value; } get { return defensePower; } }
        [SerializeField]
        private int exp;
        public int Exp { set { exp = value; } get { return exp; } }
        [SerializeField]
        private int gold;
        public int Gold { set { gold = value; } get { return gold; } }
        [SerializeField]
        private List<EnemySkill> enemySkillList = new List<EnemySkill>();
        public List<EnemySkill> getEnemySkillList { get { return enemySkillList; } }
    }
    [System.Serializable]
    public class EnemySkill
    {
        [Header("技名")]
        [SerializeField]
        private string skillName;
        public string getSkillName { get { return skillName; } }

        [TextArea(1, 1)]
        [Header("技の説明")]
        [SerializeField]
        private string skillInformation;

        [Header("威力")]
        [SerializeField]
        private int skillPower;
        public int SkillPower { set { skillPower = value; } get { return skillPower; } }
    }
   public void Enemy1Status()
    {
        skillNo = Random.Range(0, 3);
        enemyList[0].MaxHP = 1*Database.instance.playerStatus.Level;
        enemyList[0].HP = enemyList[0].MaxHP;
        enemyList[0].AttackPower = 5 * Database.instance.playerStatus.Level;
        enemyList[0].DefensePower = 3 * Database.instance.playerStatus.Level;
        enemyList[0].Exp = 10 * Database.instance.playerStatus.Level;
        enemyList[0].Gold = 2 * Database.instance.playerStatus.Level;
        enemyList[0].getEnemySkillList[0].SkillPower = 3 * Database.instance.playerStatus.Level;
        enemyList[0].getEnemySkillList[1].SkillPower = 6 * Database.instance.playerStatus.Level;
        enemyList[0].getEnemySkillList[2].SkillPower = 12 * Database.instance.playerStatus.Level;
    }
    public void Enemy2Status()
    {
        skillNo = Random.Range(0, 3);
        enemyList[1].MaxHP = 20 * Database.instance.playerStatus.Level;
        enemyList[1].HP = enemyList[1].MaxHP;
        enemyList[1].AttackPower = 5 * Database.instance.playerStatus.Level;
        enemyList[1].DefensePower = 4 * Database.instance.playerStatus.Level;
        enemyList[1].Exp = 12 * Database.instance.playerStatus.Level;
        enemyList[1].Gold = 20 * Database.instance.playerStatus.Level;
        enemyList[1].getEnemySkillList[0].SkillPower = 6 * Database.instance.playerStatus.Level;
        enemyList[1].getEnemySkillList[1].SkillPower = 12 * Database.instance.playerStatus.Level;
        enemyList[1].getEnemySkillList[2].SkillPower = 24 * Database.instance.playerStatus.Level;
    }
    public void BossStatus()
    {
        skillNo = Random.Range(0, 3);
        enemyList[2].MaxHP = 100 * Database.instance.playerStatus.Level;
        enemyList[2].HP = enemyList[2].MaxHP;
        enemyList[2].AttackPower = 40 * Database.instance.playerStatus.Level;
        enemyList[2].DefensePower = 35 * Database.instance.playerStatus.Level;
        enemyList[2].Exp = 0 * Database.instance.playerStatus.Level;
        enemyList[2].Gold = 0 * Database.instance.playerStatus.Level;
        enemyList[2].getEnemySkillList[0].SkillPower = 45 * Database.instance.playerStatus.Level;
        enemyList[2].getEnemySkillList[1].SkillPower = 55 * Database.instance.playerStatus.Level;
        enemyList[2].getEnemySkillList[2].SkillPower = 60* Database.instance.playerStatus.Level;
    }
}