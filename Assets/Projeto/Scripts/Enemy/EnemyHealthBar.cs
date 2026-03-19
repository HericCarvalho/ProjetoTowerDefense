using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage;

    private Transform target;
    private Camera cam;

    private EnemyHealth health;

    public Vector3 offset = new Vector3(0, 2f, 0);

    void Start()
    {
        cam = Camera.main;
        target = transform.parent;
        health = target.GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (target == null) return;

        transform.position = target.position + offset;

        transform.forward = cam.transform.forward;

        fillImage.fillAmount = health.CurrentHealth / health.maxHealth;
    }
}