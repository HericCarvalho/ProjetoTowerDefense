using UnityEngine;
using UnityEngine.UI;

public class SkillConnectionUI : MonoBehaviour
{
    public RectTransform from;
    public RectTransform to;

    public Image line;

    void Update()
    {
        Vector3 dir = to.position - from.position;

        line.rectTransform.position = from.position;
        line.rectTransform.sizeDelta = new Vector2(dir.magnitude, 5f);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        line.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}