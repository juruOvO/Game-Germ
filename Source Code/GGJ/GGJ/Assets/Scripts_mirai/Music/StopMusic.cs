using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    public void StopMusicFunc()
    {
        MusicManager.Instance.StopMusic();
        MusicManager.Instance.PlayFightBGM();
    }

    public void PlayMainTheme(){
        MusicManager.Instance.PlayMainTheme();
        MusicManager.Instance.ChangeVolumn("0.75");
    }
}
    