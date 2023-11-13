using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoulDetail : MonoBehaviour
{

    [SerializeField] GameObject BookUI; // ��ü UI�� �ǹ�
    [SerializeField] GameObject[] BookPage; // ���������� �ǹ� : ���� / ��ų / ȯ��
    [SerializeField] Animator[] soulImages; // ������ ������� ��Ÿ���� �κ�
    [SerializeField] TMP_InputField nameInputField; // ������ �̸� �Է¹ް� ��Ÿ��
    [SerializeField] TextMeshProUGUI[] emotions; // ������ �� ��ġ�� �˷���

    [SerializeField] TextMeshProUGUI tier; // ��ų Ƽ��
    [SerializeField] TextMeshProUGUI[] skillname; // ��ų �̸�
    [SerializeField] TextMeshProUGUI[] skillDescription; // ��ų�� ���� ����
   
    [SerializeField] TextMeshProUGUI revive; // ȯ�� Ƚ��
    [SerializeField] Image[] button; // ȯ�� ��ư
    [SerializeField] Sprite notChecked; // üũ �ȵ� ��������Ʈ
    [SerializeField] Sprite isChecked; // üũ �� ��������Ʈ

    // å UI�� Ȱ��ȭ ���¸� ����
    public void OpenBook(Soul soul)
    {
        BookUI.SetActive(!BookUI.activeSelf); // ���� ���� ��, Ȱ��ȭ �Ǿ��ٸ� ?
        if (BookUI.activeSelf)
        {
            InitBook(soul); // �ʱ�ȭ
            ChangeBookPage1(); // å 1��������
        } 
    }

    // å �ʱ�ȭ �޼���
    public void InitBook(Soul soul)
    {
        nameInputField.text = soul.name; // �̸��� ǥ��
        for (int i = 0; i < 6; i++)
        {
            emotions[i].text = soul.emotions[i].ToString(); // ������ ǥ��
        }

        // �ִϸ��̼� ����
        for (int i = 0; i < 2; i++)
        {
            soulImages[0].SetInteger("body", soul.customizes[0]);
            soulImages[0].SetInteger("eyes", soul.customizes[1]);
            soulImages[0].SetInteger("bcolor", soul.customizes[2]);
            soulImages[0].SetInteger("ecolor", soul.customizes[3]);
        }

        // ȯ�� Ƚ�� ����
        revive.text = "�� " + soul.revive.ToString() + " ��°";
        // ��ư �ʱ�ȭ
        for(int i  = 0; i < 6; i++)
        {
            // ȯ�� �ɼ� �� ���ٰ�
            button[i].sprite = notChecked;
        }

        // ��ų �ؽ�Ʈ ����
        for (int i = 0; i < 3; i++)
        {
            Skill skill = SkillManager.instance.GetSkillInfo(soul.parameters[i]);
            skillname[i].text = skill.name;
            skillDescription[i].text = skill.description;
            tier.text = skill.tier;
            if (tier.text.Equals("����")) tier.color = Color.magenta;
            if (tier.text.Equals("��ȭ")) tier.color = Color.red;
        }
    }

    // ���� �������� ����
    public void ChangeBookPage1()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 0) BookPage[i].SetActive(true); // �� �������� Ȱ��ȭ�ϰ� 
            else BookPage[i].SetActive(false); // �ٸ� �������� Ȱ��ȭ���� ����
        }
    }

    // ��ų �������� ����
    public void ChangeBookPage2()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 1) BookPage[i].SetActive(true);
            else BookPage[i].SetActive(false);
        }
    }

    // ȯ�� �������� ����
    public void ChangeBookPage3()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == 2) BookPage[i].SetActive(true);
            else BookPage[i].SetActive(false);
        }
    }
}
