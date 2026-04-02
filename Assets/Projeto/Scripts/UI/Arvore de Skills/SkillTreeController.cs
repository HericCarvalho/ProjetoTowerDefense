using UnityEngine;
using UnityEngine.EventSystems;

public class SkillTreeController : MonoBehaviour, IDragHandler, IScrollHandler
{
    public RectTransform initialFocus;
    public RectTransform content;
    public Canvas canvas;

    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 2f;

    public float smoothSpeed = 10f;

    private Vector2 targetPosition;
    private Vector3 targetScale;


    void Start()
    {
        targetPosition = content.anchoredPosition;
        targetScale = content.localScale;

        if (initialFocus != null)
        {
            CenterOnTarget(initialFocus);
        }
    }

    void Update()
    {
        content.anchoredPosition = Vector2.Lerp(
            content.anchoredPosition,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );

        content.localScale = Vector3.Lerp(
            content.localScale,
            targetScale,
            Time.deltaTime * smoothSpeed
        );
    }

    public void OnDrag(PointerEventData eventData)
    {
        targetPosition += eventData.delta / canvas.scaleFactor * content.localScale.x;
    }

    public void OnScroll(PointerEventData eventData)
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            content,
            eventData.position,
            eventData.pressEventCamera,
            out mousePos
        );

        float scroll = eventData.scrollDelta.y;

        Vector3 newScale = targetScale + Vector3.one * scroll * zoomSpeed;
        newScale = ClampScale(newScale);

        Vector3 scaleFactor = newScale - targetScale;

        targetPosition -= mousePos * scaleFactor.x;

        targetScale = newScale;
    }
    public void CenterOnTarget(RectTransform target)
    {
        Vector2 targetPos = (Vector2)content.InverseTransformPoint(target.position);
        targetPosition = -targetPos;
    }

    Vector3 ClampScale(Vector3 scale)
    {
        scale.x = Mathf.Clamp(scale.x, minZoom, maxZoom);
        scale.y = Mathf.Clamp(scale.y, minZoom, maxZoom);
        scale.z = 1;

        return scale;
    }
}