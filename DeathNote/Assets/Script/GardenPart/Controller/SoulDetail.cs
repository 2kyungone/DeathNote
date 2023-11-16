using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoulDetail : MonoBehaviour
{

    [SerializeField] GameObject BookUI; // ��ü UI�� �ǹ�
    [SerializeField] GameObject[] BookPage; // ���������� �ǹ� : ���� / ��ų / ȯ��
    [SerializeField] Animator[] soulImages; // ������ ������� ��Ÿ���� �κ�
    [SerializeField] TMP_InputField nameInputField; // ������ �̸� �Է¹ް� ��Ÿ��
    [SerializeField] TextMeshProUGUI[] emotions; // ������ �� ��ġ�� �˷���

    [SerializeField] TextMeshProUGUI[] tier; // ��ų Ƽ��
    [SerializeField] TextMeshProUGUI[] skillname; // ��ų �̸�
    [SerializeField] TextMeshProUGUI[] skillDescription; // ��ų�� ���� ����
   
    [SerializeField] TextMeshProUGUI revive; // ȯ�� Ƚ��
    [SerializeField] Image[] button; // ȯ�� üũ
    [SerializeField] TextMeshProUGUI reviveCost; // ȯ�� ���� 
    [SerializeField] Sprite notChecked; // üũ �ȵ� ��������Ʈ
    [SerializeField] Sprite isChecked; // üũ �� ��������Ʈ
    
    [SerializeField] GardenManager gardenManager; // ����Ŵ���

    [SerializeField] Image[] selectArea; // �����°�
    [SerializeField] Animator[] mySession; // ����
    [SerializeField] TextMeshProUGUI[] targetEmotions; // ���� ����
    [SerializeField] TextMeshProUGUI[] targetSkills; // ���� ��ų

    public GardenSoul activeSoul;
    public Soul targetSoul;
    public int targetIdx;
    public Soul nowSoul; // ���� ���� �ҿ�
    bool[] ascendSwitch;


    // å UI�� Ȱ��ȭ ���¸� ����
    public void OpenBook(GardenSoul Gsoul)
    {

        BookUI.SetActive(!BookUI.activeSelf); // ���� ���� ��, Ȱ��ȭ �Ǿ��ٸ� ?
        if (BookUI.activeSelf)
        {
            activeSoul = Gsoul;

            InitBook(Gsoul.soul); // �ʱ�ȭ
            ChangeBookPage1(); // å 1��������
        }
        else
        {
            Gsoul.CameraZoom();
        }
    }

    // å �ʱ�ȭ �޼���
    public void InitBook(Soul soul)
    {
        nowSoul = soul;
        nameInputField.text = soul.name; // �̸��� ǥ��
        for (int i = 0; i < 6; i++)
        {
            emotions[i].text = soul.emotions[i].ToString(); // ������ ǥ��
        }


        // ȯ�� Ƚ�� ����
        revive.text = "�� " + soul.revive.ToString() + " ��°";
        // ��ư �ʱ�ȭ
        for(int i  = 0; i < 5; i++)
        {
            // ȯ�� �ɼ� �� ���ٰ�
            button[i].sprite = notChecked;
        }

        ascendSwitch = new bool[6];
        reviveCost.text = CalcuateCost().ToString();

        // ��ų �ؽ�Ʈ ����
        for (int i = 0; i < 3; i++)
        {
            Skill skill = SkillManager.instance.GetSkillInfo(soul.parameters[i]);
            skillname[i].text = skill.name;
            skillDescription[i].text = skill.description;
            string star = "";
            for (int j = 0; j < skill.tier; j++)
            {
                star += "��"; // Ƽ�ŭ ���� ����
            }   
            tier[i].text = star;
        }
    }

    // ���� �������� ����
    public void ChangeBookPage1()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 0) BookPage[i].SetActive(true); // �� �������� Ȱ��ȭ�ϰ� 
            else BookPage[i].SetActive(false); // �ٸ� �������� Ȱ��ȭ���� ����
            Debug.Log(nowSoul.customizes[i]);
        }
        soulImages[0].SetInteger("body", nowSoul.customizes[0]);
        soulImages[0].SetInteger("eyes", nowSoul.customizes[1]);
        soulImages[0].SetInteger("bcol", nowSoul.customizes[2]);
        soulImages[0].SetInteger("ecol", nowSoul.customizes[3]);
    }

    // ��ų �������� ����
    public void ChangeBookPage2()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 1) BookPage[i].SetActive(true);
            else BookPage[i].SetActive(false);
        }
    }

    // ȯ�� �������� ����
    public void ChangeBookPage3()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 2) BookPage[i].SetActive(true);
            else BookPage[i].SetActive(false);
        }
        ascendSwitch = new bool[6];

        soulImages[1].SetInteger("body", nowSoul.customizes[0]);
        soulImages[1].SetInteger("eyes", nowSoul.customizes[1]);
        soulImages[1].SetInteger("bcol", nowSoul.customizes[2]);
        soulImages[1].SetInteger("ecol", nowSoul.customizes[3]);

        for(int i = 0; i < 6; i++)
        {
            button[i].sprite = notChecked;
        }

    }

    public void ChangeBookPage4()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 3) BookPage[i].SetActive(true);
            else BookPage[i].SetActive(false);
        }

        // �ִϸ����͸� ����
        for(int i = 0; i < 6; i++)
        {
            Soul equipSoul = SkillManager.instance.equip[i];
            if(equipSoul != null)
            {
                mySession[i].SetInteger("body", equipSoul.customizes[0]);
                mySession[i].SetInteger("eyes", equipSoul.customizes[1]);
                mySession[i].SetInteger("bcol", equipSoul.customizes[2]);
                mySession[i].SetInteger("ecol", equipSoul.customizes[3]);
            }
            else
            {
                mySession[i].SetInteger("body", 0);
                mySession[i].SetInteger("eyes", -1);
                mySession[i].SetInteger("bcol", -1);
                mySession[i].SetInteger("ecol", 0);
            }

            TargetSelect(0);
        }
    }
    // �� ����
    public void GoUp()
    {
        int length = UserManager.instance.userData.souls.Count;
        int prevIdx = (nowSoul.id + length - 1) % length;
        Soul prevSoul = UserManager.instance.userData.souls[prevIdx];
        GardenSoul prev = prevSoul.gardenSoul;
        prev.CameraZoom();
        InitBook(UserManager.instance.userData.souls[prevIdx]);
    }

    // �Ʒ� ����
    public void GoDown()
    {
        int length = UserManager.instance.userData.souls.Count;
        int nextIdx = (nowSoul.id + 1) % length;
        Soul nextSoul = UserManager.instance.userData.souls[nextIdx];
        GardenSoul next = nextSoul.gardenSoul;
        next.CameraZoom();
        InitBook(UserManager.instance.userData.souls[nextIdx]);
    }


    public void ChangeName()
    {
        string name = nameInputField.text;
        if (name.Length >= 2 && name.Length <= 6) activeSoul.ChangeName(name);
    }

    public void TargetSelect(int idx)
    {
        targetSoul = SkillManager.instance.equip[idx];
        targetIdx = idx;

        for (int i = 0; i < 6; i++)
        {
            if (i != idx)
            {
                selectArea[i].color = new Color(0, 0, 0, 0.4f);
            }
            else selectArea[i].color = new Color(0, 0, 0, 1);
        }

        for (int i = 0; i < 6; i++)
        {
            if(targetSoul == null) targetEmotions[i].text = "-";
            else targetEmotions[i].text = targetSoul.emotions[i].ToString();
            if(i < 3)
            {
                if (targetSoul == null) targetSkills[i].text = "-";
                else targetSkills[i].text = SkillManager.instance.GetSkillInfo(targetSoul.parameters[i]).name;
            }
        }
    }

    public void Equip()
    {
        if (targetSoul != null && targetSoul.Equals(nowSoul)) return;

        int exchangeIdx = -1;

        for (int i = 0; i < 6; i++)
        {
            if (SkillManager.instance.equip[i] != null && SkillManager.instance.equip[i].Equals(nowSoul)) exchangeIdx = i ;
        }

        // ��ȯ
        if(exchangeIdx != -1 && SkillManager.instance.equip[targetIdx] != null)
        {
            SkillManager.instance.equip[exchangeIdx] = SkillManager.instance.equip[targetIdx];
            SkillManager.instance.equip[exchangeIdx].equip = exchangeIdx;
            SkillManager.instance.equip[targetIdx] = nowSoul;
            nowSoul.equip = targetIdx;
        }

        // ���°�
        else if(exchangeIdx != -1 && SkillManager.instance.equip[targetIdx] == null)
        {
            SkillManager.instance.equip[targetIdx] = nowSoul;
            nowSoul.equip = targetIdx;
            SkillManager.instance.equip[exchangeIdx] = null;
        }

        else
        {
            if (SkillManager.instance.equip[targetIdx] != null) SkillManager.instance.equip[targetIdx].equip = -1;
            nowSoul.equip = targetIdx;
            SkillManager.instance.equip[targetIdx] = nowSoul;
        }     

        UserManager.instance.SaveData();
        ChangeBookPage4();
    }

    public void EquipClick1()
    {
        TargetSelect(0);
    }

    public void EquipClick2()
    {
        TargetSelect(1);
    }
    public void EquipClick3()
    {
        TargetSelect(2);
    }
    public void EquipClick4()
    {
        TargetSelect(3);
    }
    public void EquipClick5()
    {
        TargetSelect(4);
    }
    public void EquipClick6()
    {
        TargetSelect(5);
    }


    // üũ�ϱ�
    public void AscendClick1()
    {
        ascendSwitch[0] = !ascendSwitch[0];
        if (button[0].sprite.Equals(isChecked)) button[0].sprite = notChecked;
        else button[0].sprite = isChecked;

        reviveCost.text = CalcuateCost().ToString();
    }

    public void AscendClick2()
    {
        ascendSwitch[1] = !ascendSwitch[1];
        if (button[1].sprite.Equals(isChecked)) button[1].sprite = notChecked;
        else button[1].sprite = isChecked;

        reviveCost.text = CalcuateCost().ToString();
    }

    public void AscendClick3()
    {
        ascendSwitch[2] = !ascendSwitch[2];
        if (button[2].sprite.Equals(isChecked)) button[2].sprite = notChecked;
        else button[2].sprite = isChecked;

        reviveCost.text = CalcuateCost().ToString();
    }

    public void AscendClick4()
    {
        ascendSwitch[3] = !ascendSwitch[3];
        if (button[3].sprite.Equals(isChecked)) button[3].sprite = notChecked;
        else button[3].sprite = isChecked;

        reviveCost.text = CalcuateCost().ToString();
    }

    public void AscendClick5()
    {
        ascendSwitch[4] = !ascendSwitch[4];
        if (button[4].sprite.Equals(isChecked)) button[4].sprite = notChecked;
        else button[4].sprite = isChecked;

        reviveCost.text = CalcuateCost().ToString();
    }

    public void AscendClick6()
    {
        ascendSwitch[5] = !ascendSwitch[4];
        if (button[5].sprite.Equals(isChecked)) button[5].sprite = notChecked;
        else button[5].sprite = isChecked;

        reviveCost.text = CalcuateCost().ToString();
    }

    public int CalcuateCost()
    {
        int cost = 1000 + 50 * nowSoul.revive;
        for(int i = 0; i <6; i++)
        {
            if (ascendSwitch[i]) cost += 1000;
        }

        return cost;
    }

    // �����̺�
    public void Revive()
    {
        int cost = CalcuateCost();
        if(UserManager.instance.userData.gold >= cost)
        {
            UserManager.instance.userData.gold -= cost;
            ReviveSoul();
            activeSoul.ReRender();
            gardenManager.UpdateInspirit();
        }

        gameObject.SetActive(false);
    }

    // �����
    public void ReviveSoul()
    {
        if (!ascendSwitch[0]) nowSoul.customizes[0] = UnityEngine.Random.Range(1, 7); // 1���� 6���� ����
        if (!ascendSwitch[1]) nowSoul.customizes[1] = UnityEngine.Random.Range(1, 3); // 1���� 6���� ����
        if (!ascendSwitch[2])
        {
            int chance = UnityEngine.Random.Range(0, 100);

            if (chance > 70)
            {
                nowSoul.customizes[2] = UnityEngine.Random.Range(1, 11);

            }
            chance = UnityEngine.Random.Range(0, 100);

            if (chance > 50)
            {
                nowSoul.customizes[3] = UnityEngine.Random.Range(1, 11);

            }
        }

        // ��ų�̱�
        for (int i = 0; i < 3; i++)
        {
            if (ascendSwitch[i + 3]) continue;
            int chance = UnityEngine.Random.Range(0, 100);
            if (chance == 0)
            {
                nowSoul.parameters[i] = 200; // ��ȭ ��ų
            }
            else if (chance < 10)
            {
                nowSoul.parameters[i] = 100 + UnityEngine.Random.Range(0, 3); // ���� ��ų
            }
            else
            {
                nowSoul.parameters[i] = UnityEngine.Random.Range(0, 6); // �Ϲ� ��ų
            }
        }

        nowSoul.parameters[3] = UnityEngine.Random.Range(3, 10);
        // ������ 5���� 20
        for (int i = 0; i < 6; i++)
        {
            nowSoul.emotions[i] += UnityEngine.Random.Range(5, 20);
        }

        nowSoul.revive += 1;

        UserManager.instance.SaveData();
    }
}
