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
    [SerializeField] Sprite[] sprites;
    [SerializeField] TextMeshProUGUI menuUI;
    [SerializeField] GameObject[] particles;

    string[] gardenName;
    string[] gardenImage;
    int[] gardenPrice;

    int page = 0;

    // Start is called before the first frame update


    void Awake()
    {
        gardenManager.OpenMenu();
        gardenName = new string[] { "������ ��", "���Ǵ� ����", "������ �ٴ�" };
        gardenImage = new string[] { "0", "1", "2" };
        gardenPrice = new int[] { 0, 30000, 100000 };
        InitUI();

    }

    public void InitUI()
    {

        gardenManager.OpenMenu();
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

        gardenCurrentImage.sprite = Resources.Load<Sprite>("Image/Garden/Background/" + gardenImage[page]);

        if (gardenPrice[page] == 0)
        {
            gardenCurrentPrice.text = "�̵�";
            lockImage.SetActive(false);
        }
        else
        {
            gardenCurrentPrice.text = gardenPrice[page].ToString();
            lockImage.SetActive(true);
        }
    }


    // ����
    public void Purchase()
    {
        if (gardenPrice[page] == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                if (page == i) particles[i].SetActive(true);
                else particles[i].SetActive(false);
            }
            gardenManager.ChangeGarden(page);
        }
        else
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