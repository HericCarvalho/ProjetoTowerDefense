using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildMenuUI : MonoBehaviour
{
    public static BuildMenuUI instance;

    public GameObject menuPanel;

    BuildNode currentNode;

    public bool IsMenuOpen => menuPanel.activeSelf;

    void Awake()
    {
        instance = this;
        menuPanel.SetActive(false);
    }

    void Update()
    {
        if (!IsMenuOpen) return;

        if (IsPointerDown())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            CloseMenu();
        }
    }

    bool IsPointerDown()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            return true;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            return true;

        return false;
    }

    public void OpenMenu(BuildNode node)
    {
        currentNode = node;
        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        currentNode = null;
    }

    public BuildNode GetNode()
    {
        return currentNode;
    }

    public void OnTowerButton(TowerData data)
    {
        BuildManager.instance.SelectTower(data);
        CloseMenu();
    }
}