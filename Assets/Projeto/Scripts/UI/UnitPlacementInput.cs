using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UnitPlacementInput : MonoBehaviour
{
    public static UnitPlacementInput instance;

    private GameObject selectedUnit;

    void Awake()
    {
        instance = this;
    }

    public void SetSelectedUnit(GameObject unit)
    {
        selectedUnit = unit;
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsReviveOpen())
            return;

        if (selectedUnit == null)
            return;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            int touchId = Touchscreen.current.primaryTouch.touchId.ReadValue();

            if (EventSystem.current.IsPointerOverGameObject(touchId))
                return;

            Vector2 screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
            TryPlace(screenPos);
        }

        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 screenPos = Mouse.current.position.ReadValue();
            TryPlace(screenPos);
        }
    }

    void TryPlace(Vector2 screenPos)
    {
        Vector3 worldPos = GetWorldPosition(screenPos);

        UnitPlacementManager.instance.SpawnUnit(selectedUnit, worldPos);

        selectedUnit = null; 
    }

    Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}