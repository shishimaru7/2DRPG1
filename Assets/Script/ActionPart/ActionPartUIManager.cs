using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionPartUIManager : MonoBehaviour
{
    [SerializeField]
    private ItemShop itemShop;
    [SerializeField]
    private SavePoint savePoint;


    [SerializeField]
    private GameObject statusScreen;
    [SerializeField]
    private GameObject itemSelectScreen;

    [SerializeField]
    private Button exitButton;//ゲーム終了
    [SerializeField]
    private Button backButton;//戻る
    [SerializeField]
    private Button itemSelectBackButton;//「アイテム」から戻る
    [SerializeField]
    private Button itemSelectButton;//「アイテム」
    [SerializeField]
    private Button useHealItemButton;//回復
    [SerializeField]
    private Button useSPItemButton;//SP回復
    //DDでアサインしないでゲーム開始時に自動でアサインさせる
    [SerializeField]
    private Text currentHpText;
    [SerializeField]
    private Text currentSpText;
    [SerializeField]
    private Text currentLevelText;
    [SerializeField]
    private Text currentGoldText;
    [SerializeField]
    private Text healItemText;
    public Text healItemStockText;
    [SerializeField]
    private Text spItemText;
    public Text spItemStockText;
    //DDでアサインしないでゲーム開始時に自動でアサインさせる
    private const string currentHpTextStr = "currentHpText";
    private const string currentSpTextStr = "currentSpText";
    private const string currentLevelTextStr = "currentLevelText";
    private const string currentGoldTextStr = "currentGoldText ";
    private const string healItemTextStr = "USEHealItemButtonText";
    private const string healItemStockTextStr = "USEHealItemStockText";
    private const string spItemTextStr = "USESPItemButtonText";
    private const string spItemStockTextStr = "USESPItemStockText";

    void Start()
    {
        //DDでアサインしないでゲーム開始時に自動でアサインさせる
        currentHpText = GameObject.Find(currentHpTextStr).GetComponent<Text>();
        currentSpText = GameObject.Find(currentSpTextStr).GetComponent<Text>();
        currentLevelText = GameObject.Find(currentLevelTextStr).GetComponent<Text>();
        currentGoldText = GameObject.Find(currentGoldTextStr).GetComponent<Text>();
        healItemText = GameObject.Find(healItemTextStr).GetComponent<Text>();
        healItemStockText = GameObject.Find(healItemStockTextStr).GetComponent<Text>();
        spItemText = GameObject.Find(spItemTextStr).GetComponent<Text>();
        spItemStockText = GameObject.Find(spItemStockTextStr).GetComponent<Text>();
        //////////////////////////////////////////////////////////////////////////////////////

        exitButton.onClick.AddListener(ExitButton);
        backButton.onClick.AddListener(BackButton);
        itemSelectBackButton.onClick.AddListener(BackButton);
        itemSelectButton.onClick.AddListener(OpenUseItemScreen);
        useHealItemButton.onClick.AddListener(USEHealItemButton);
        useSPItemButton.onClick.AddListener(USESPItemButton);

        UpdateDisplayText();

        statusScreen.SetActive(false);
        itemSelectScreen.SetActive(false);
    }


    void Update()
    {
        OpenStatusScreen();
        UpdateDisplayText();
    }
    /// <summary>
    /// 他のCanvasが開いている時にステータス画面が開かないようにする
    /// </summary>
    public void OpenStatusScreen()
    {
        if (savePoint.saveScreen.activeSelf || itemShop.itemShopScreen.activeSelf || itemShop.itemShopListScreen.activeSelf)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            statusScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;   // UnityEditorの実行を停止する処理
#else
        Application.Quit();                                // ゲームを終了する処理
#endif
    }

    public void BackButton()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        statusScreen.SetActive(false);
        itemSelectScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void OpenUseItemScreen()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        itemSelectScreen.SetActive(true);
        healItemText.text = Database.instance.playerStatus.getHaveItemList[0].getItemName;
        healItemStockText.text = Database.instance.playerStatus.getHaveItemList[0].HaveItem.ToString();
        spItemText.text = Database.instance.playerStatus.getHaveItemList[1].getItemName;
        spItemStockText.text = Database.instance.playerStatus.getHaveItemList[1].HaveItem.ToString();
    }
    private void UpdateDisplayText()
    {
        currentHpText.text = "HP:" + Database.instance.playerStatus.HP + "/" + Database.instance.playerStatus.MaxHP;
        currentSpText.text = "SP:" + Database.instance.playerStatus.SP + "/" + Database.instance.playerStatus.MaxSP;
        currentLevelText.text = "レベル:" + Database.instance.playerStatus.Level;
        currentGoldText.text = "所持金:" + Database.instance.playerStatus.GoldStock;
    }

    public void USEHealItemButton()
    {
        int no = 0;
        Database.instance.playerStatus.UseItemEffect(no);
        healItemStockText.text = Database.instance.playerStatus.getHaveItemList[no].HaveItem.ToString();
    }
    public void USESPItemButton()
    {
        int no = 1;
        Database.instance.playerStatus.UseItemEffect(no);
        spItemStockText.text = Database.instance.playerStatus.getHaveItemList[no].HaveItem.ToString();
    }
}