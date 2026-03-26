using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public Tower towerXP;

    public Slider xpBar;
    public TextMeshProUGUI levelText;

    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (towerXP == null)
            return;

        Vector3 dir = cam.position - transform.position;
        dir.y = 0f;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(-dir);

        float progress = (float)towerXP.currentXP / towerXP.xpToNextLevel;

        xpBar.value = progress;
        levelText.text = "Lv " + towerXP.level;

        xpBar.gameObject.SetActive(progress < 0.99f);
    }
}