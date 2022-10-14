using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlashSound : MonoBehaviour
{
    public AudioClip slash;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void PlaySlash() {
        audioSource.PlayOneShot(slash);
    }
}
