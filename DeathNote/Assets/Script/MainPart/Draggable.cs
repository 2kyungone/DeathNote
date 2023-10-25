using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �ʿ��� ��� �巡�� ���� �� ������ �۾��� ���⿡ �߰�
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPosition = new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �ʿ��� ��� �巡�� ���� �� ������ �۾��� ���⿡ �߰�
    }
}
