using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouseclik : MonoBehaviour
{
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound();
        }
    }
    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
