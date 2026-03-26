using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            HandleClick(Touchscreen.current.primaryTouch.position.ReadValue());
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleClick(Mouse.current.position.ReadValue());
        }
    }

    void HandleClick(Vector2 screenPosition)
    {
        if (IsPointerOverUI())
            return;

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            TowerUIManager.instance.Close(); 
            return;
        }


        BuildNode node = hit.collider.GetComponentInParent<BuildNode>();
        Tower tower = hit.collider.GetComponentInParent<Tower>();

        if (tower != null)
        {
            TowerUIManager.instance.SelectTower(tower);
            return;
        }

        if (BuildManager.instance.CanBuild())
        {
            if (node != null && !node.HasTower())
            {
                if (BuildManager.instance.selectedTower != null)
                {
                    node.BuildTower(BuildManager.instance.selectedTower);
                    BuildManager.instance.CancelBuild();
                }
            }

            return;
        }

        if (node != null)
        {
            if (!node.HasTower())
            {
                BuildMenuUI.instance.OpenMenu(node);
                return;
            }
        }

        TowerUIManager.instance.Close();
    }

    bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            return EventSystem.current.IsPointerOverGameObject(
                Touchscreen.current.primaryTouch.touchId.ReadValue()
            );
        }

        return EventSystem.current.IsPointerOverGameObject();
    }
}