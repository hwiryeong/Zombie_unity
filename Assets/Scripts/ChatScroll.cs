using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatScroll : MonoBehaviour
{
    public ScrollRect scrollRect;         // ScrollRect ������Ʈ
    public Scrollbar verticalScrollbar;  // ScrollRect�� Vertical Scrollbar

    private bool isUserScrolling = false; // ����ڰ� ��ũ�� ������ ����

    private void Start()
    {
        if (verticalScrollbar != null)
        {
            // Scrollbar�� �巡�� �̺�Ʈ�� ����
            EventTrigger trigger = verticalScrollbar.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((data) => { OnScrollbarDragged(); });
            trigger.triggers.Add(dragEntry);
        }

        // ScrollRect�� ��ũ�� ���� �̺�Ʈ ����
        if (scrollRect != null)
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }
    }

    private void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        if (scrollRect != null)
        {
            scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }
    }

    private void Update()
    {
        // ���콺 �� �Է� ����
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f)
        {
            isUserScrolling = true;
            Debug.Log("����ڰ� ���콺 �ٷ� ��ũ�� ��");
        }
    }

    private void OnScrollbarDragged()
    {
        isUserScrolling = true;
        Debug.Log("����ڰ� ��ũ�ѹٸ� �巡�� ��");
    }

    private void OnScrollValueChanged(Vector2 position)
    {
        // ����ڰ� ���� ��ũ���� �� �Ʒ��� �̵��ߴ��� Ȯ��
        if (Mathf.Approximately(scrollRect.verticalNormalizedPosition, 0f))
        {
            isUserScrolling = !Mathf.Approximately(scrollRect.verticalNormalizedPosition, 0f);
        }
    }

    public void AddMessage(string message)
    {
        // �޽����� �߰��Ǿ��� ��
        Canvas.ForceUpdateCanvases(); // UI ���� ����

        // ����ڰ� ��ũ���� �ǵ帮�� �ʾҴٸ� �� �Ʒ��� �̵�
        if (!isUserScrolling)
        {
            scrollRect.verticalNormalizedPosition = 0f;
            Debug.Log("�ڵ� ��ũ�� ����");
        }
    }

    public void ResetScrollState()
    {
        isUserScrolling = false;
    }

}