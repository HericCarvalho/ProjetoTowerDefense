using UnityEngine;

public class RevivedUnit : MonoBehaviour
{
    public float range = 3f;
    public float speed = 4f;

    private Transform target;
    private Vector3 origin;

    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        if (target == null)
        {
            FindTarget();
            ReturnToOrigin();
        }
        else
        {
            AttackTarget();
        }
    }

    void FindTarget()
    {
        foreach (Transform enemy in EnemyManager.instance.enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);

            if (dist <= range)
            {
                target = enemy;

                enemy.GetComponent<EnemyMovement>().enabled = false;

                break;
            }
        }
    }

    void AttackTarget()
    {
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist < 0.5f)
        {
            EnemyHealth eh = target.GetComponent<EnemyHealth>();

            if (eh != null)
                eh.TakeDamage(10f);
        }

        if (target == null || !target.gameObject.activeSelf)
        {
            target = null;
        }
    }

    void ReturnToOrigin()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            origin,
            speed * Time.deltaTime
        );
    }
}