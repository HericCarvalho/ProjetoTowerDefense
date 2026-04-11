using UnityEngine;
using System.Collections;

public class TutorialUI : MonoBehaviour
{
    public RectTransform hand;

    [Header("Targets")]
    public Transform shopButton;
    public Transform towerIcon;
    public Transform buildSpot;
    public Transform startWaveButton;
    public Transform placedTower;
    public Transform upgradeButton;

    Coroutine currentRoutine;

   // public Transform placedTower;

    public void SetPlacedTower(Transform tower)
    {
        placedTower = tower;
    }
    public void ShowClick(Transform target)
    {
        StopAll();

        hand.gameObject.SetActive(true);
        currentRoutine = StartCoroutine(ClickRoutine(target));
    }

    public void ShowDrag(Transform start, Transform end)
    {
        StopAll();

        hand.gameObject.SetActive(true);
        currentRoutine = StartCoroutine(DragRoutine(start, end));
    }

    public void HideHand()
    {
        StopAll();
        hand.gameObject.SetActive(false);
    }

    public void HideAll()
    {
        HideHand();
    }

    void StopAll()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
    }

    IEnumerator ClickRoutine(Transform target)
    {
        while (true)
        {
            hand.position = target.position;

            // animação fake de clique
            hand.localScale = Vector3.one;
            yield return new WaitForSeconds(0.3f);

            hand.localScale = Vector3.one * 0.8f;
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator DragRoutine(Transform start, Transform end)
    {
        while (true)
        {
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime;
                hand.position = Vector3.Lerp(start.position, end.position, t);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
