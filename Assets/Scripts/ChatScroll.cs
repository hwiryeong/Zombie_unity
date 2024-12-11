using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatScroll : MonoBehaviour
{
    public ScrollRect scrollRect;         // ScrollRect 컴포넌트
    public Scrollbar verticalScrollbar;  // ScrollRect의 Vertical Scrollbar

    private bool isUserScrolling = false; // 사용자가 스크롤 중인지 여부

    private void Start()
    {
        if (verticalScrollbar != null)
        {
            // Scrollbar의 드래그 이벤트를 감지
            EventTrigger trigger = verticalScrollbar.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry dragEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Drag
            };
            dragEntry.callback.AddListener((data) => { OnScrollbarDragged(); });
            trigger.triggers.Add(dragEntry);
        }

        // ScrollRect의 스크롤 변경 이벤트 감지
        if (scrollRect != null)
        {
            scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
        }
    }

    private void OnDestroy()
    {
        // 이벤트 리스너 제거
        if (scrollRect != null)
        {
            scrollRect.onValueChanged.RemoveListener(OnScrollValueChanged);
        }
    }

    private void Update()
    {
        // 마우스 휠 입력 감지
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f)
        {
            isUserScrolling = true;
            Debug.Log("사용자가 마우스 휠로 스크롤 중");
        }
    }

    private void OnScrollbarDragged()
    {
        isUserScrolling = true;
        Debug.Log("사용자가 스크롤바를 드래그 중");
    }

    private void OnScrollValueChanged(Vector2 position)
    {
        // 사용자가 직접 스크롤을 맨 아래로 이동했는지 확인
        if (Mathf.Approximately(scrollRect.verticalNormalizedPosition, 0f))
        {
            isUserScrolling = !Mathf.Approximately(scrollRect.verticalNormalizedPosition, 0f);
        }
    }

    public void AddMessage(string message)
    {
        // 메시지가 추가되었을 때
        Canvas.ForceUpdateCanvases(); // UI 강제 갱신

        // 사용자가 스크롤을 건드리지 않았다면 맨 아래로 이동
        if (!isUserScrolling)
        {
            scrollRect.verticalNormalizedPosition = 0f;
            Debug.Log("자동 스크롤 실행");
        }
    }

    public void ResetScrollState()
    {
        isUserScrolling = false;
    }

}