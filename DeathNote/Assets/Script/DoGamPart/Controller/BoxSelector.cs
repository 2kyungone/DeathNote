using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // UI ��ҿ� ���� ���ӽ����̽� �߰�

public class BoxSelector : MonoBehaviour, IPointerClickHandler
{
    public CharacterInfo characterInfo; // CharacterInfo Ŭ������ �����ϰ� �ùٸ��� ���ǵǾ� �ִ��� Ȯ��

    public Sprite defaultSprite; 
    public Sprite selectedSprite; 

    private Image imageComponent; 
    private BoxManager boxManager; 

    public Image characterDisplayArea; 
    public Image innerCharacterImage;
    public Text nicknameText; 
    public Text skillDescriptionText; 

    private Animator animator;

    private void Awake()
    {
        imageComponent = GetComponent<Image>(); 
        boxManager = FindObjectOfType<BoxManager>(); 
        animator = characterDisplayArea.GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        Select(); // �޼��� �̸� �빮�� S
        boxManager.RegisterSelection(this); 

        // UI ������Ʈ
        nicknameText.text = characterInfo.nickname;
        skillDescriptionText.text = characterInfo.skillDescription;

        // �ִϸ��̼� ���
        animator.Play("BounceAnimation");
    }

    public void Deselect()
    {
        // �ڽ��� ��������Ʈ�� �⺻ ��������Ʈ�� ����
        imageComponent.sprite = defaultSprite;
    }

    private void Select()
    {
        // �ڽ��� ��������Ʈ�� ���õ� ��������Ʈ�� ����
        imageComponent.sprite = selectedSprite;
    }
}
