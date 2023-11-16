using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Garden ����� ��� �ִ� Garden ��ü
public class GardenManager : MonoBehaviour
{
    public List<GardenSoul> moveSouls; // ���� �����̴� �ҿ�
    public int location; // ������ ���� ��ȣ
    public int capacity; // ���� ���� ����
    
    public SpriteRenderer background; // ������ ���� ���
    public ParticleSystem particles; // ��ƼŬ...

    Coroutine coroutine;

    // ��� UI
    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject soulUI;
    [SerializeField] GameObject wikiUI;
    [SerializeField] GameObject gardenPurchaseUI;
    [SerializeField] GameObject soulPurchaseUI;

    private float speed = 80000.0f;
    private bool open;

    [SerializeField] TextMeshProUGUI inspirit;
    [SerializeField] TextMeshProUGUI maxCapacity;
    [SerializeField] GameObject soulPrefab;
    [SerializeField] Transform area; // Garden�� ����
    [SerializeField] GameObject composeButton; // �۰� ��ư(��Ȱ����)

    void Awake()
    {
        location = -1;
    }

    // �ʱ�ȭ �޼���
    void Start()
    {
        InitSoul(); // ������ �ʱ�ȭ
    }

    // ������ �ʱ�ȭ �ϴ� �޼���
    public void InitSoul()
    {
       
        moveSouls = new List<GardenSoul>(); // �����̴� ���ɵ��� ��ũ��Ʈ ����Ʈ
        List<Soul> souls = UserManager.instance.userData.souls; // ������ ��� ����
        capacity = souls.Count; // ������ �� ���� ��
        Debug.Log("���� �� ���� �� :" + capacity);
        for (int i = 0; i < souls.Count; i++)
        {
            ActiveSoul(souls[i]); // ���� Ȱ��ȭ
        }
        UpdateInspirit(); // UI �ʱ�ȭ
        ChangeGarden(0); // ��� ����
    }

    // ���� ��ü�� Ȱ��ȭ ���Ѽ� �ʿ� ���ƴٴϰ� �Ѵ�.
    public void ActiveSoul(Soul soul) 
    {
        int random = UnityEngine.Random.Range(-50, 50); // ù ��ġ ����ȭ
        Vector3 position = new Vector3(transform.position.x + random, transform.position.y + random);

        GameObject gardenSoul = Instantiate(soulPrefab, position, Quaternion.identity); // ���ƴٴϴ� ������Ʈ ����
        gardenSoul.SetActive(false);

        // ������ ����� �θ��� ����
        gardenSoul.transform.SetParent(area);

        // GardenSoul ��ũ��Ʈ�� ����
        GardenSoul script = gardenSoul.GetComponent<GardenSoul>();
        script.boundaryTransform = area;
        // Soul ��ü�� ����
        script.soul = soul;
        script.soulDetail = soulUI.GetComponent<SoulDetail>();
        soul.gardenSoul = script;
        // �ѹ��� �����ϱ� ����, gardenSoul ��ũ��Ʈ�� ����
        moveSouls.Add(script);

        // Ȱ��ȭ
        gardenSoul.SetActive(true);
    }

    // ������ ������ �ڽ��� ���� ����Ʈ�� ����
    public void NewSoul(Soul soul)
    {
        SoulManager.instance.AddSoul(soul);
        ActiveSoul(soul);
    }


    IEnumerator MoveToPosition(Transform transform, Vector3 position)
    {
        while (Vector3.Distance(transform.localPosition, position) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, position, speed * Time.deltaTime);
            yield return null;
        }

        transform.localPosition = position; // ���� ��ġ�� ��Ȯ�ϰ� ����
    }




    public void ChangeGarden(int location)
    {
        // ���� ��ġ ����
        if (this.location == location) return;
        this.location = location;
        this.background.sprite = Resources.Load<Sprite>("Image/Garden/Background/" + location);

    }

    public void Cheat()
    {
        UserManager.instance.userData.gold += 100000;
        UpdateInspirit();
    }

    // ���� ������ ������ ǥ���ϰ�, capacity�� �����մϴ�.
    public void UpdateInspirit()
    {
        capacity = UserManager.instance.userData.souls.Count; // UI �ʱ�ȭ
        maxCapacity.text = UserManager.instance.userData.souls.Count.ToString() + "/16";
        inspirit.text = UserManager.instance.userData.gold.ToString();
    }


    // soul�� wiki, ���� UI�� ��� �ݽ��ϴ�.
    public void CloseAllUi()
    {
        soulUI.SetActive(false);
        wikiUI.SetActive(false);
        gardenPurchaseUI.SetActive(false);
        soulPurchaseUI.SetActive(false);
    }

    public void GoMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    
}
