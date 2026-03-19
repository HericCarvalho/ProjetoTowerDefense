using UnityEngine;
using UnityEngine.UI;

public class WavePreviewUI : MonoBehaviour
{
    public Transform container;
    public GameObject previewItemPrefab;

    public void ShowPreview(EnemyWave wave)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

        foreach (EnemyGroup group in wave.groups)
        {
            GameObject item = Instantiate(previewItemPrefab, container);

            Text txt = item.GetComponentInChildren<Text>();
            txt.text = group.enemyPrefab.name + " x" + group.count;
        }
    }
}