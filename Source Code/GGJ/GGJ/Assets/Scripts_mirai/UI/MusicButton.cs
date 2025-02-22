using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicButton : MonoBehaviour
{
    private CanvasGroup canvas;
    void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    public void TurnOn(){
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
    }

    public void TurnOff(){
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
    }
}
