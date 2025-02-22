using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : SingletonMonobehaviour<MusicManager>
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;

    protected override void Awake(){
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }


    void Start(){
        EventsNotifier.Instance.OnClickEvent_VolumeChange += ChangeVolumn;
    }

    public void ChangeVolumn(string para){
        float target = float.Parse(para);
        audioSource.volume = target;
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayFightBGM()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void PlayMainTheme()
    {
        audioSource.Stop();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

}
