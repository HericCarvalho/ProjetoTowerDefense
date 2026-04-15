using UnityEngine;
using System.Collections;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public RectTransform hand;

    Coroutine currentRoutine;
    bool isRunning = false;

    public float clickScaleMin = 0.8f;
    public float clickSpeed = 0.3f;

    public float dragSpeed = 1f;

    public void SetPlacedTower(Transform tower)
    {
        
    }

    public void ShowClick(Transform target)
    {
        Debug.Log("ShowClick chamado!");

        if (target == null)
        {
            Debug.LogError("Target NULL no tutorial!");
            return;
        }

        StopAll();

        hand.gameObject.SetActive(true);

        Debug.Log("Mão ativada!");

        currentRoutine = StartCoroutine(ClickRoutine(target));
    }

    public void ShowDrag(Transform start, Transform end)
    {
        if (start == null || end == null) return;

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
        isRunning = false;

        if (currentRoutine != null)
            StopCoroutine(currentRoutine);
    }

    IEnumerator ClickRoutine(Transform target)
    {
        isRunning = true;

        while (isRunning && target != null)
        {
            hand.position = target.position;

            hand.localScale = Vector3.one;
            yield return new WaitForSeconds(clickSpeed);

            hand.localScale = Vector3.one * clickScaleMin;
            yield return new WaitForSeconds(clickSpeed);
        }
    }

    IEnumerator DragRoutine(Transform start, Transform end)
    {
        isRunning = true;

        while (isRunning && start != null && end != null)
        {
            float t = 0;

            while (t < 1f)
            {
                if (!isRunning) yield break;

                t += Time.deltaTime * dragSpeed;
                hand.position = Vector3.Lerp(start.position, end.position, t);

                yield return null;
            }

            yield return new WaitForSeconds(0.4f);
        }
    }
}