using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndSceneSwitch : MonoBehaviour
{
    public bool isTutorial= false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void EndGame()
    {
        SceneControllerManager.Instance.FadeAndLoadScene("MapTesting");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)) {
            SceneControllerManager.Instance.FadeAndLoadScene("MapTesting");
        }
        if (Generator.EndGame == true)
        {
            if(isTutorial)
            {
                MapVisualize.needinit = true;
            }
            Generator.EndGame = false;
            SceneControllerManager.Instance.FadeAndLoadScene("MapTesting");
        }
    }
}
