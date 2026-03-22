using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float moveSpeed = 2f;
    public float lifeTime = 1f;


    private float timer;
    private GameObject prefabReference;

    void OnEnable()
    {
        timer = lifeTime;
    }

    public void Setup(int damage, Color color, GameObject prefab)
    {
        text.text = damage.ToString();
        text.color = color;

        timer = lifeTime;
        prefabReference = prefab;
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (prefabReference == null)
            {
                return;
            }

            ObjectPool.instance.ReturnObject(gameObject, prefabReference);
        }
    }
}