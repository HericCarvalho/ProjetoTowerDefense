using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    void OnMouseDown()
    {
        GetComponent<Tower>().OnSelected();
    }
}