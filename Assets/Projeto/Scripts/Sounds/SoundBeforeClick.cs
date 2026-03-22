using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBeforeClick : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerSound()
    {
        audioSource.Play();
    }
}
