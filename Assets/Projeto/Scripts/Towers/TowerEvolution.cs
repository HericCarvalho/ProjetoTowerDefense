using UnityEngine;

public class TowerEvolution : MonoBehaviour
{
    public GameObject nextStagePrefab;

    public void Evolve()
    {
        if (nextStagePrefab == null)
            return;

        Instantiate(nextStagePrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }
}