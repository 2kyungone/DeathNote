using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoulGet : MonoBehaviour
{
    [SerializeField] GameObject soulGetUI; // ������ ��� UI
    [SerializeField] GameObject animationUI; // ���� ��� �� �ִϸ��̼� UI
    [SerializeField] GameObject confirmUI; // ���� ��� ���� UI
    [SerializeField] TextMeshProUGUI currentCapacity; // ���� Garden�� Capacity
    [SerializeField] TextMeshProUGUI currentCost; // ���� ������ �̴µ� �ʿ��� ����
    [SerializeField] TextMeshProUGUI purchaseButton; // ���Ű� �������� �ƴ���
    
    [SerializeField] Animator getAnimator;
    [SerializeField] GardenManager gardenManager;
    [SerializeField] Transform soulImage;

    private bool isPurchase = false;
    // [SerializeField] TextMeshProUGUI alert;

    // ��ų : �븻 6��


    // ���� ����, UI�� ����� �Ѵ�. ��ư�� �̿��� UI��.
    public void EnableUI()
    {
        if (!soulGetUI.activeSelf)
        {
            gardenManager.CloseAllUi(); // ��� UI â�� ����
            soulGetUI.SetActive(true);
        }
        else
        {
            soulGetUI.SetActive(false);
        }

        confirmUI.SetActive(true); // �������� �������� �⺻�� ������ true������
        InitUI();
    }

    // UI�� ����� ��, ���� �ϴ� ������ �ʱ�ȭ�ϴ� �޼���
    public void InitUI()
    {
        int cost = gardenManager.capacity * 1000;
        currentCost.text = cost.ToString(); // ���� ���ɼ��� 1000�� ���� ����
        if (gardenManager.capacity == 16) // 16�������
        {
            purchaseButton.text = "�� �̻� ��ȯ�� �� �����ϴ�.";
        }
        else if (UserManager.instance.userData.gold < cost) // ���� ���ٸ�
        {
            purchaseButton.text = "������ �����մϴ�.";
        }
        else
        {
            purchaseButton.text = "��ȯ";
        }
    }
    

    // ������ ��ȯ�ϴ� �޼���
    public void PurchaseSoul()
    {
        int cost = gardenManager.capacity * 1000; // ���ɻ̴µ� ��� ���
        if(UserManager.instance.userData.gold >= cost && gardenManager.capacity < 16){ // ���ǹ�
            UserManager.instance.userData.gold -= cost; // ���� ����

            Soul soul = MakeSoul(); // ���� �����

            confirmUI.SetActive(false); // Ȯ��â�� ������


            gardenManager.NewSoul(soul); // ���ο� ���� ���
            gardenManager.UpdateInspirit(); // UI �ʱ�ȭ

            StartCoroutine(StartAnimation(soul));
        }
    }

    // ���� ȹ�� �ִϸ��̼��� Ʋ��, 2�ʵڿ� �ݴ´�.
    IEnumerator StartAnimation(Soul soul)
    {
        animationUI.SetActive(true);
        getAnimator.SetInteger("body", soul.customizes[0]);
        getAnimator.SetInteger("eyes", soul.customizes[1]);
        getAnimator.SetInteger("bcol", soul.customizes[2]);
        getAnimator.SetInteger("ecol", soul.customizes[3]);
        yield return new WaitForSeconds(2.0f);

        animationUI.SetActive(false);
    }



    // ������ ���Ӱ� ����� �޼���
    public Soul MakeSoul()
    {
        int[] customizes = new int[4]; // �ٹ̴°� 4��
        int[] parameters = new int[4]; // ��ų 3�� + ģ�е�
        int[] emotions = new int[6]; // ���� 6��

        customizes[0] = UnityEngine.Random.Range(1, 7); // 1���� 6���� ����
        customizes[1] = UnityEngine.Random.Range(1, 3); // 1���� 2���� ��


        // ������ ���� ������ �ٲ���
        int chance = UnityEngine.Random.Range(0, 100);

        if (chance > 70)
        {
            customizes[2] = UnityEngine.Random.Range(1, 11);

        }
        chance = UnityEngine.Random.Range(0, 100);

        if (chance > 50)
        {
            customizes[3] = UnityEngine.Random.Range(1, 11);

        }



        // ��ų�̱�
        for(int i = 0; i < 3; i++)
        {
            chance = UnityEngine.Random.Range(0, 100);
            if (chance == 0)
            {
                parameters[i] = 200; // ��ȭ ��ų
            }
            else if (chance < 10)
            {
                parameters[i] = 100 + UnityEngine.Random.Range(0, 3); // ���� ��ų
            }
            else
            {
                parameters[i] = UnityEngine.Random.Range(0, 6); // �Ϲ� ��ų
            }
        }

        parameters[3] = UnityEngine.Random.Range(3, 10);

        // ������ 5���� 20
        for (int i = 0; i < 6; i++)
        {
            emotions[i] = UnityEngine.Random.Range(5, 20);
        }

        //return new Soul(nameInputField.text, -1, parameters, customizes, emotions, 0, gardenManager.location);
        return new Soul("�� ����", -1, parameters, customizes, emotions, 0, gardenManager.location);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
