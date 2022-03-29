using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text spText;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private Text turnText;
    [SerializeField]
    private Text healItemNameText;
    [SerializeField]
    private Text healItemStockText;
    [SerializeField]
    private Text spItemNameText;
    [SerializeField]
    private Text spItemStockText;
    [SerializeField]
    private Text rescueItemNameText;
    [SerializeField]
    private Text rescueItemStockText;
    [SerializeField]
    private Text firstSkilTextText;
    [SerializeField]
    private Text secondSkillText;
    [SerializeField]
    private Text UseFirstSkillPowerText;
    [SerializeField]
    private Text UseSecondSkillPowerText;
    [SerializeField]
    private BattleManager battleManager;

    void Start()
    {
        UpdateText();
    }


    void Update()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        hpText.text = "HP:" + Database.instance.playerStatus.HP;
        spText.text = "SP:" + Database.instance.playerStatus.SP;
        levelText.text = "レベル:" + Database.instance.playerStatus.Level;

        turnText.text = battleManager.getTurnCount + "ターン目";

        healItemNameText.text = Database.instance.playerStatus.getHaveItemList[0].getItemName;
        healItemStockText.text = Database.instance.playerStatus.getHaveItemList[0].HaveItem.ToString() + "個";

        spItemNameText.text = Database.instance.playerStatus.getHaveItemList[1].getItemName;
        spItemStockText.text = Database.instance.playerStatus.getHaveItemList[1].HaveItem.ToString() + "個";

        rescueItemNameText.text = Database.instance.playerStatus.getHaveItemList[2].getItemName;
        rescueItemStockText.text = Database.instance.playerStatus.getHaveItemList[2].HaveItem.ToString() + "個";


        firstSkilTextText.text = Database.instance.playerStatus.getSkillList[0].SkillName;
        secondSkillText.text = Database.instance.playerStatus.getSkillList[1].SkillName;

        UseFirstSkillPowerText.text = Database.instance.playerStatus.getSkillList[0].getConsumptionSp.ToString();
        UseSecondSkillPowerText.text = Database.instance.playerStatus.getSkillList[1].getConsumptionSp.ToString();
    }
}