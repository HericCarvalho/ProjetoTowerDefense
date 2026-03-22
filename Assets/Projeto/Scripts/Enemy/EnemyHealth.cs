using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    public float damage = 10f;
    public float attackSpeed = 1f;
    public float moveSpeed = 5f;
    public float maxHealth = 100f;

    [Header("Resistances (0 a 1)")]
    [Range(0f, 1f)] public float physicalResistance;
    [Range(0f, 1f)] public float magicalResistance;

    [Header("Attack Type")]
    public bool isRanged;
    public bool isMagical;

    [Header("Damage Popup")]
    public GameObject damagePopupPrefab;

    private float health;

    private GameObject prefabReference;
    public GameObject fragmentPrefab;
    [Range(0f, 1f)] public float dropChance = 0.2f;


    void OnEnable()
    {
        health = maxHealth;
        EnemyManager.instance.RegisterEnemy(transform);
    }

    public void SetPrefabReference(GameObject prefab)
    {
        prefabReference = prefab;
    }

    public float CurrentHealth => health;

    public void TakeDamage(float amount, bool isMagicDamage, bool isTrueDamage = false)
    {
        float finalDamage = amount;

        if (!isTrueDamage)
        {
            if (isMagicDamage)
            {
                finalDamage *= (1f - magicalResistance);
                Debug.Log($"[DANO MÁGICO] Base: {amount} | Resist: {magicalResistance * 100}% | Final: {finalDamage}");
            }
            else
            {
                finalDamage *= (1f - physicalResistance);
                Debug.Log($"[DANO FÍSICO] Base: {amount} | Resist: {physicalResistance * 100}% | Final: {finalDamage}");
            }
            ShowDamagePopup((int)finalDamage, isMagicDamage, isTrueDamage);
        }
        else
        {
            Debug.Log($"[TRUE DAMAGE] Base: {amount} | Final: {finalDamage} (IGNORA RESISTÊNCIA)");
        }


        health -= finalDamage;

        Debug.Log($"[HP] Inimigo ficou com: {health}");

        if (health <= 0)
            Die();
    }

    void Die()
    {
        EnemyManager.instance.UnregisterEnemy(transform);
        ObjectPool.instance.ReturnObject(gameObject, prefabReference);
        WaveManager.instance.RegisterEnemyDeath();

        EnemyReward reward = GetComponent<EnemyReward>();
        if (reward != null)
            reward.GiveReward();
        if (Random.value <= dropChance)
        {

        }

    }
    void ShowDamagePopup(int damage, bool isMagic, bool isTrue)
    {
        if (damagePopupPrefab == null) return;

        GameObject popupGO = ObjectPool.instance.GetObject(damagePopupPrefab);

        popupGO.transform.position = transform.position + Vector3.up * 2f;

        DamagePopup popup = popupGO.GetComponent<DamagePopup>();

        Color color = Color.white;

        if (isTrue)
            color = Color.white;
        else if (isMagic)
            color = Color.blue;
        else
            color = Color.gray;

        popup.Setup(damage, color, damagePopupPrefab);
    }
    public GameObject GetPrefab()
    {
        return prefabReference;
    }
}