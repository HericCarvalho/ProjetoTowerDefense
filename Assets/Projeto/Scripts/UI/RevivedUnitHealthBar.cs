using UnityEngine;
using UnityEngine.UI;

public class RevivedUnitHealthBar : MonoBehaviour
{
    public Image fillImage;

    private Transform target;
    private Camera cam;

    private RevivedUnit unit;

    public Vector3 offset = new Vector3(0, 2f, 0);

    void Start()
    {
        cam = Camera.main;
        target = transform.parent;
        unit = target.GetComponent<RevivedUnit>();
    }

    void Update()
    {
        if (target == null || unit == null)
            return;

        transform.position = target.position + offset;

        transform.forward = cam.transform.forward;

        fillImage.fillAmount = unit.CurrentHealth / unit.maxHealth;
    }
}