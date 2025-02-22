using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListenerNewGame : MonoBehaviour
{
    public ScaleButton bt1;
    public ScaleButton bt2;
    [SerializeField] private ButtonClickEventType buttonClickEventType;
    [SerializeField] private string msg;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        if (bt1.state== true&&bt2.state==true)
        {
            switch (buttonClickEventType)
                {
                    case ButtonClickEventType.SceneChange:
                        EventsNotifier.Instance.CallForButtonClickEvent_SceneChange(msg);
                        break;
                    case ButtonClickEventType.VolumeChange:
                        EventsNotifier.Instance.CallForButtonClickEvent_VolumnChange(msg);
                        Debug.Log(1);
                        break;
            }
        }
        
    }
}
