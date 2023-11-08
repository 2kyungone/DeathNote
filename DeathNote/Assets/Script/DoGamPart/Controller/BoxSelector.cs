//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class BoxSelector : MonoBehaviour, IPointerClickHandler
//{
//    public CharacterInfo characterInfo;

//    public Sprite defaultSprite; // �⺻ �ڽ� ��������Ʈ
//    public Sprite selectedSprite; // ���õ� �ڽ� ��������Ʈ

//    private Image imageComponent; // �ڽ��� �̹��� ������Ʈ
//    private BoxManager boxManager; // �ڽ� �Ŵ��� ����

//    public Image characterDisplayArea; // ���� �������� ĳ���͸� ǥ���� ����
//    public Image innerCharacterImage; // �ڽ� ������ ĳ���� �̹��� ����
//    public Text nicknameText; // ĳ���� �г����� ǥ���� �ؽ�Ʈ ������Ʈ
//    public Text skillDescriptionText; // ĳ���� ��ų ������ ǥ���� �ؽ�Ʈ ������Ʈ

//    private Animator animator; // �ִϸ����� ������Ʈ ����

//    private void Awake()
//    {
//        imageComponent = GetComponent<Image>(); // �� ������Ʈ�� �̹��� ������Ʈ ���� ��������
//        boxManager = FindObjectOfType<BoxManager>(); // ������ BoxManager ã��
//        animator = characterDisplayArea.GetComponent<Animator>(); // ĳ���� ǥ�� ������ �ִϸ����� ������Ʈ ��������
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        Select();
//        boxManager.RegisterSelection(this);

//        // UI ������Ʈ
//        characterDisplayArea.sprite = characterInfo.characterSprite;
//        nicknameText.text = characterInfo.nickname;
//        skillDescriptionText.text = characterInfo.skillDescription;

//        // �ִϸ��̼� ���
//        animator.Play("BounceAnimation");
//    }

//    public void Deselect()
//    {
//        // �ڽ��� ��������Ʈ�� �⺻ ��������Ʈ�� ����
//        imageComponent.sprite = defaultSprite;
//    }

//    private void Select()
//    {
//        // �ڽ��� ��������Ʈ�� ���õ� ��������Ʈ�� ����
//        imageComponent.sprite = selectedSprite;
//    }
//}
