using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoShop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) {
            SceneControllerManager.Instance.FadeAndLoadScene("Shop");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneControllerManager.Instance.FadeAndLoadScene("GameScene1");
        }
    }
}
