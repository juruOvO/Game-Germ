using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class FillButton : MonoBehaviour
{
  
    public bool isOver = false;
    public float FillSpeed = 0.1f;

    public bool ClickAct = true;
    public string KeyAct = "None";

    [Serializable]
    public class ButtonClickedEvent : UnityEvent
    {
    }

    [FormerlySerializedAs("onClick")]
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

    private UnityEngine.UI.Image fillImage;

    void Start()
    {
        transform.Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
        fillImage = transform.Find("Canvas").Find("Front").GetComponent<UnityEngine.UI.Image>();
        fillImage.fillAmount = 0;
    }

    void Update()
    {
        if(isOver) fillImage.fillAmount = FillSpeed + fillImage.fillAmount*(1-FillSpeed);
        else fillImage.fillAmount *= 1-FillSpeed;

        if (isOver && Input.GetMouseButtonDown(0))
        {
            Press();
        }
    }

    private void Press()
    {
            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
    }

    public void OnMouseOver()
    {
        isOver = true;
    }

    public void OnMouseExit()
    {
        isOver = false;
    }

    public bool GetAct()
    {
        if (ClickAct) return Input.GetMouseButtonDown(0);
        else return Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), KeyAct));
    }
}
