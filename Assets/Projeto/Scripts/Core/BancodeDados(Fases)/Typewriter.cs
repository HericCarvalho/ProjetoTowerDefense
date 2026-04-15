using System.Collections;
using TMPro;
using UnityEngine;

public class Typewriter : MonoBehaviour
{
    public float speed = 0.03f;

    public IEnumerator Write(TextMeshProUGUI textUI, string text)
    {
        textUI.text = "";

        foreach (char c in text)
        {
            textUI.text += c;
            yield return new WaitForSecondsRealtime(speed);
        }
    }
}