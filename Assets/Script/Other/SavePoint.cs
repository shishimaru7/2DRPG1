using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    [SerializeField]
    private ActionPartUIManager actionPartUIManager;
    private SaveData saveData;
    [SerializeField]
    private GameObject signInformation;
    public GameObject saveScreen;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button roadButton;
    [SerializeField]
    private Button saveCancelButton;

    private void Start()
    {
        saveData = GetComponent<SaveData>();
        saveButton.onClick.AddListener(SaveButton);
        roadButton.onClick.AddListener(RoadButton);
        saveCancelButton.onClick.AddListener(CancelButton);
        saveScreen.SetActive(false);
        saveButton.enabled =false;
        roadButton.enabled =false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            signInformation.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
                Database.instance.SavePointPosition = collision.gameObject.transform.position;
                Debug.Log(Database.instance.SavePointPosition);
                signInformation.SetActive(false);
                saveScreen.SetActive(true);
                saveButton.enabled =true;
                roadButton.enabled =true;
                Time.timeScale = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            signInformation.SetActive(false);
        }
    }

    public void CancelButton()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        saveScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void SaveButton()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        saveData.Save();
    }
    public void RoadButton()
    {
        SoundManager.instance.ClickSE(SoundManager.instance.gBGM);
        saveData.RoadData();
    }

}