using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField]
    private ActionPartUIManager actionPartUIManager;
    // 販売アイテムの表示(これ)配列使う　アイテム購入所持金から減る(これ)　購入Playerに所持(PlayerStatus)配列番号に入れる
    [SerializeField]
    private string[] shopItemName = new string[3];//アイテムの名前
    [Header("アイテムの種類")]
    [SerializeField]
    private int[] shopItems = new int[3];//アイテムの種類
    [Header("アイテムの価格")]
    public int[] shopItemPrice = new int[3];//アイテムの価格

    [SerializeField]
    private GameObject signInformation;
    public GameObject itemShopScreen;
    public GameObject itemShopListScreen;
    [SerializeField]
    private Button buyButton;//買う
    [SerializeField]
    private Button getOutItemShopButton;//店を出る
    [SerializeField]
    private Button cancelButton;//戻る
    [SerializeField]
    private Button healItemSelectButton;//回復アイテム購入
    [SerializeField]
    private Button spItemSelectButton;//SP回復アイテム購入
    [SerializeField]
    private Button rescueItemSelectButton;//自動蘇生アイテム購入

    [SerializeField]
    private Text goidText;//所持金
    [SerializeField]
    private Text buyText;//購入
    [SerializeField]
    private Text cantBuyText;//持てない
    [SerializeField]
    private Text notHaveText;//お金が足りない
    [SerializeField]
    private Text healItemText;//回復アイテムの名称
    [SerializeField]
    private Text spItemText;//Sp回復アイテムの名称
    [SerializeField]
    private Text rescueItemText;//レスキューアイテムの名称
    [SerializeField]
    private Text healItemPriceText;//回復アイテムの価格テキスト
    [SerializeField]
    private Text spItemPriceText;//Sp回復アイテムの価格テキスト
    [SerializeField]
    private Text rescueItemPriceText;//レスキューアイテムの価格テキスト

    void ItemList()
    {
        shopItemName[0] = "体力回復";
        shopItems[0] = Database.instance.playerStatus.getHaveItemList[0].HaveItem;
        shopItemPrice[0] = 10;
        healItemText.text = shopItemName[0];
        healItemPriceText.text = shopItemPrice[0].ToString() + "GOLD";

        shopItemName[1] = "元気回復";
        shopItems[1] = Database.instance.playerStatus.getHaveItemList[1].HaveItem;
        shopItemPrice[1] = 50;
        spItemText.text = shopItemName[1];
        spItemPriceText.text = shopItemPrice[1].ToString() + "GOLD";

        shopItemName[2] = "自動蘇生";
        shopItems[2] = Database.instance.playerStatus.getHaveItemList[2].HaveItem;
        shopItemPrice[2] = 400;
        rescueItemText.text = shopItemName[2];
        rescueItemPriceText.text = shopItemPrice[2].ToString() + "GOLD";
    }

    void Start()
    {
        ItemList();
        buyButton.onClick.AddListener(BuyItem);
        getOutItemShopButton.onClick.AddListener(GetOutItemShop);
        cancelButton.onClick.AddListener(CancelButton);
        healItemSelectButton.onClick.AddListener(HealItemSelectButton);
        spItemSelectButton.onClick.AddListener(SPItemSelectButton);
        rescueItemSelectButton.onClick.AddListener(RescueItemSelectButton);
        itemShopListScreen.SetActive(false);
        signInformation.SetActive(false);
        itemShopScreen.SetActive(false);
        goidText.text = "所持金" + Database.instance.playerStatus.GoldStock + "GOLD";
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            signInformation.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
                signInformation.SetActive(false);
                itemShopScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            signInformation.SetActive(false);
            itemShopScreen.SetActive(false);
        }
    }

    public void BuyItem()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        itemShopScreen.SetActive(false);
        itemShopListScreen.SetActive(true);
    }

    public void GetOutItemShop()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        itemShopScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void CancelButton()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        itemShopListScreen.SetActive(false);
        itemShopScreen.SetActive(true);
        cantBuyText.enabled = false;
        notHaveText.enabled = false;
        buyText.enabled = false;
    }
    private void BuyItemButton(int itemNo)
    {
        if (Database.instance.playerStatus.getHaveItemList[itemNo].HaveItem >= Database.instance.playerStatus.ItemStock)
        {
            notHaveText.enabled = true;
            notHaveText.text = "持てないよ";
            return;
        }
        else if (Database.instance.playerStatus.GoldStock >= shopItemPrice[itemNo])
        {
            SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
            cantBuyText.enabled = false;

            Database.instance.playerStatus.PossessionHaelItem(this, itemNo);

            buyText.enabled = true;
            buyText.text = shopItemName[itemNo] + Database.instance.playerStatus.getHaveItemList[itemNo].HaveItem + "/" + Database.instance.playerStatus.ItemStock + "所持";
            actionPartUIManager.healItemStockText.text = Database.instance.playerStatus.getHaveItemList[itemNo].HaveItem.ToString();
            goidText.text = "所持金" + Database.instance.playerStatus.GoldStock + "GOLD";
        }
        else
        {
            cantBuyText.enabled = true;
            cantBuyText.text = "お金が足りないよ";
        }
        notHaveText.enabled = false;
    }

    public void HealItemSelectButton()
    {
        int itemNo = 0;
        BuyItemButton(itemNo);
    }

    public void SPItemSelectButton()
    {
        int itemNo = 1;
        BuyItemButton(itemNo);
    }
    public void RescueItemSelectButton()
    {
        int itemNo = 2;
        BuyItemButton(itemNo);
    }
}