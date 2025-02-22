using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AutoSwitchToMainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 10f)
        {
            SceneControllerManager.Instance.FadeAndLoadScene("MainMenu");
        }
        if(Input.GetMouseButtonDown(0))
        {
            SceneControllerManager.Instance.FadeAndLoadScene("MainMenu");
        }
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneControllerManager.Instance.FadeAndLoadScene("MainMenu");
        }
    }
}
