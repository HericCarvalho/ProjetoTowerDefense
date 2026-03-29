using UnityEngine;
using UnityEngine.EventSystems;

public class TowerDragUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TowerData towerData;

    RectTransform rectTransform;
    Canvas canvas;
    GameObject ghost;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DRAG FUNCIONANDO");

        ghost = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ghost.transform.localScale = Vector3.one;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BuildNode node = BuildMenuUI.instance.GetNode();

        if (node != null)
        {
            node.BuildTower(towerData);
        }

        BuildMenuUI.instance.CloseMenu();
    }

}