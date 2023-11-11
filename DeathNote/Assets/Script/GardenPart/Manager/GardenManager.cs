using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Garden ����� ��� �ִ� Garden ��ü
public class GardenManager : MonoBehaviour
{
    public List<Soul> souls;
    public List<GardenSoul> moveSouls;
    public Sprite Background;
    public ParticleSystem particles;
    public int location;
  
    [SerializeField] GameObject soulPrefab;
    [SerializeField] Transform area;
    [SerializeField] GameObject BookUI;
    [SerializeField] GameObject[] BookPage;

    void Awake()
    {
        location = 1;

    }


    void Start()
    {

        List<Soul> souls = SoulManager.instance.Souls;
        moveSouls = new List<GardenSoul>();

        foreach (Soul soul in souls)
        {
            if(soul.garden == location)
            {
                
                int random = UnityEngine.Random.Range(-50, 50);
                Vector3 position = new Vector3(transform.position.x + random, transform.position.y + random);
                // ObjectInfo �迭�� ��� ��Ҹ� count��ŭ �����ϰ� ��Ȱ��ȭ �� �� Queue�� �־�д�.
                GameObject gardenSoul = Instantiate(soulPrefab, position, Quaternion.identity);
                gardenSoul.SetActive(false);
                // ������ ����� �θ��� ����
                gardenSoul.transform.SetParent(area);

                GardenSoul script = gardenSoul.GetComponent<GardenSoul>();
                script.soul = soul;
                script.boundaryTransform = area;
                gardenSoul.SetActive(true);

                moveSouls.Add(script);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
