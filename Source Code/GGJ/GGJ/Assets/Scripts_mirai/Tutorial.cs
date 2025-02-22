using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup canvas;
    private bool visible = false;

    private void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!visible)
        {
            if(Input.GetKeyDown(KeyCode.E)) 
            {
                TurnOn();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            {
                TurnOff();
            }
        }
    }
    void TurnOn()
    {
        visible = true;
        canvas.alpha = 1.0f;
        canvas.blocksRaycasts = true;
    }

    void TurnOff()
    {
        visible = false;
        canvas.alpha = 0.0f;
        canvas.blocksRaycasts = false;
    }
}
