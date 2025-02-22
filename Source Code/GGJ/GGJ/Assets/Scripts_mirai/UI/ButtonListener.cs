using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
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
