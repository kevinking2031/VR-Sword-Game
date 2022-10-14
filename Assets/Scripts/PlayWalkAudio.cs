using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playWalkAudio : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
