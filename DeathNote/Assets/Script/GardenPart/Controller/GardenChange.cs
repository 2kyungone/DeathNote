using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GardenChange : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GardenManager gardenManager;
    [SerializeField] TextMeshProUGUI gardenCurrentName;
    [SerializeField] TextMeshProUGUI gardenCurrentPrice;
    [SerializeField] GameObject lockImage;
    [SerializeField] Animator lessMoney;
    [SerializeField] Button buyButton;
    [SerializeField] Image gardenCurrentImage;
    [SerializeField] GameObject purchaseButton;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TextMeshProUGUI menuUI;
    [SerializeField] GameObject[] particles;
    [SerializeField] Image smallButton;
    [SerializeField] Sprite[] smallSprites;
    [SerializeField] TextMeshProUGUI smallText;

    string[] gardenName;
    int[] gardenPrice;

    bool canGo;
    int page = 0;

    // Start is called before the first frame update


    void Awake()
    {
        gardenName = new string[] { "������ ��", "���Ǵ� ����", "������ �ٴ�" };
        gardenPrice = new int[] { 0, 30000, 100000 };
        InitUI();

    }

    public void InitUI()
    {

        UI.SetActive(true);
        for (int i = 0; i < UserManager.instance.userData.gardens.Count; i++)
        {
            gardenPrice[UserManager.instance.userData.gardens[i].type] = 0;
        }
        ChangeGardenMarket();
    }

    // ���� ��ư�� ������ ���
    public void GoLeftPage()
    {
        page = (page + 2) % 3;
        ChangeGardenMarket();
    }

    // ������ ��ư�� ������ ���
    public void GoRightPage()
    {
        page = (page + 1) % 3;
        ChangeGardenMarket();
    }

    // ���� �׸��� �ٲ�
    public void ChangeGardenMarket()
    {
        gardenCurrentName.text = gardenName[page];
        gardenCurrentImage.sprite = sprites[page];
        canGo = false;

        foreach(Garden garden in UserManager.instance.userData.gardens)
        {
            if(garden.type == page)
            {
                canGo = true;
                purchaseButton.SetActive(false);
                break;
            }
        }
       
        if(!canGo)
        {
            purchaseButton.SetActive(true);
            gardenCurrentPrice.text = gardenPrice[page].ToString();

        }
    }

    public void ChangeGarden()
    {
        if (canGo)
        {
            for (int i = 0; i < 3; i++)
            {
                if (page == i) particles[i].SetActive(true);
                else particles[i].SetActive(false);
            }
            gardenManager.ChangeGarden(page);
            smallButton.sprite = smallSprites[page];
        }

    }

    public void UIOnOff()
    {
        UI.SetActive(!UI.activeSelf);
        if (UI.activeSelf)
        {
            smallText.text = "�ݱ�";
        }
        else
        {
            smallText.text = "��� ����";
        }
    }

    // ����
    public void Purchase()
    {
        UserData data = UserManager.instance.userData;
        if (data.gold >= gardenPrice[page])
        {
            data.gold -= gardenPrice[page];
            data.gardens.Add(new Garden(page));
            gardenManager.UpdateInspirit();
            menuUI.text = gardenName[page];
            InitUI();
            UserManager.instance.SaveData();
            UI.SetActive(false);
        }

        else
        {
            lessMoney.SetTrigger("less");
        }
    }

    public void CloseUI()
    {
        UI.SetActive(false);
    }

    // 

    // Update is called once per frame
    void Update()
    {

    }
}