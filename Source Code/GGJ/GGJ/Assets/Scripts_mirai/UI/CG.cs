using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CG : MonoBehaviour
{
    private float timer = 0f; 
    public List<SpriteRenderer> spriteRenderers;
    private int counter = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1.5f)
        {
            timer = 0f;
            counter++;
            handle();
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneControllerManager.Instance.FadeAndLoadScene("MainMenu");
            MusicManager.Instance.PlayMainTheme();
            Debug.Log("CG to Main Menu");
        }

    }

    void handle()
    {
        if (counter == 9 ) { 
            SceneControllerManager.Instance.FadeAndLoadScene("MainMenu");
            MusicManager.Instance.PlayMainTheme();
            Debug.Log("CG to Main Menu");
        }
        else
        {
            Color color = Color.white;
            color.a = 0f;
            spriteRenderers[counter - 1].color = color;
            color.a = 1f;
            spriteRenderers[counter].color = color;
        }
        
    }
}
