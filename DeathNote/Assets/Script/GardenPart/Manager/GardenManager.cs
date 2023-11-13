using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
// Garden ����� ��� �ִ� Garden ��ü
public class GardenManager : MonoBehaviour
{
    public List<GardenSoul> moveSouls; // ���� �����̴� �ҿ�
    public int location; // ������ ���� ��ȣ
    public int capacity; // ���� ���� ����
    public SpriteRenderer background; // ������ ���� ���
    public ParticleSystem particles; // ��ƼŬ...
    
    
    [SerializeField] TextMeshProUGUI inspirit;
    [SerializeField] TextMeshProUGUI maxCapacity;
    [SerializeField] GameObject soulPrefab;
    [SerializeField] Transform area; // Garden�� ����
    [SerializeField] GameObject BookUI;
    [SerializeField] GameObject[] BookPage;

    void Awake()
    {
        location = -1;

    }

    void Start()
    {
        InitSouls();
        UpdateInspirit(); // UI �ʱ�ȭ
        ChangeGarden(0);
    }

    public void InitSouls()
    {
        moveSouls = new List<GardenSoul>();

        for (int i = 0; i < 15; i++)
        {
            int random = UnityEngine.Random.Range(-50, 50);
            Vector3 position = new Vector3(transform.position.x + random, transform.position.y + random);

            // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
            GameObject gardenSoul = Instantiate(soulPrefab, position, Quaternion.identity);
            gardenSoul.SetActive(false);
            // ������ ����� �θ��� ����
            gardenSoul.transform.SetParent(area);
            // GardenSoul ��ũ��Ʈ�� ����
            GardenSoul script = gardenSoul.GetComponent<GardenSoul>();
            script.boundaryTransform = area; 

            moveSouls.Add(script);
        }
    }

    public void ChangeGarden(int location)
    {
        // ���� ��ġ ����
        if (this.location == location) return;
        this.location = location;
        this.background.sprite = Resources.Load<Sprite>("Image/Garden/Background/" + location);
        List<Soul> souls = UserManager.instance.userData.souls;
        // ���� ǥ���� ����
        
        for(int i = 0; i < 15; i++)
        {
            moveSouls[i].gameObject.SetActive(false);
        }

        int idx = 0;

        // souls���� Ȯ��
        foreach (Soul soul in souls)
        {
            if (soul.garden == location)
            {
                // ������ ��ġ�� �������� ����
                int random = UnityEngine.Random.Range(-50, 50);
                Vector3 position = new Vector3(transform.position.x + random, transform.position.y + random);

                // moveSouls�� GameObject
                GameObject gardenSoul = moveSouls[idx++].gameObject;
                GardenSoul script = gardenSoul.GetComponent<GardenSoul>();
                script.soul = soul;
                gardenSoul.SetActive(true);
            }

        }
    }

    public void Cheat()
    {
        UserManager.instance.userData.gold += 100000;
        UpdateInspirit();
    }

    // ���� ������ ������ ǥ���մϴ�.
    public void UpdateInspirit()
    {
        inspirit.text = UserManager.instance.userData.gold.ToString();
    }

    // ������ ������ �ڽ��� ���� ����Ʈ�� ����
    public void NewSoul(Soul soul)
    {
        SoulManager.instance.AddSoul(soul);

        int random = UnityEngine.Random.Range(-50, 50);
        Vector3 position = new Vector3(transform.position.x + random, transform.position.y + random);

        // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
        GameObject gardenSoul = Instantiate(soulPrefab, position, Quaternion.identity);
        gardenSoul.SetActive(false);
        // ������ ����� �θ��� ����
        gardenSoul.transform.SetParent(area);
        // GardenSoul ��ũ��Ʈ�� ����
        GardenSoul script = gardenSoul.GetComponent<GardenSoul>();
        script.boundaryTransform = area;
        script.soul = soul;

        gardenSoul.SetActive(true);
        moveSouls.Add(script);
    }
    // Update is called once per frame
    void Update()
    {

    }

    
}
