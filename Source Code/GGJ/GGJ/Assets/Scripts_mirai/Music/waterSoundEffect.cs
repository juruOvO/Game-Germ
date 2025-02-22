using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterSoundEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void TurnOff()
    {
        if(audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
