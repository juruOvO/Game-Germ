using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    private CanvasGroup canvas;
    public CanvasGroup fader;
    private bool visible = false;

    void Awake(){
        canvas = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(visible){
                TurnDownOpetionMenu();
            }else{
                TurnOnOpetionMenu();
            }

        }
    }

    public void TurnOnOpetionMenu(){
        visible = true;
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        fader.alpha = 0.8f;
    }

    public void TurnDownOpetionMenu()
    {
        visible = false;
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        fader.alpha = 0;
    }
}
