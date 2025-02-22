using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ScaleButton : MonoBehaviour
{

    public bool isOver = false;
    public float ScaleSpeed = 0.1f;
    public float ScaleSize = 1.5f;
    public float SelectSize = 0.7f;
    public bool Floating = false;
    public float FloatingSpeed = 0.1f;
    public float FloatScale = 0.1f;
    public float FloatBias = 0.1f;

    public bool state = false;
    public GameObject State0;
    public GameObject State1;

    public bool ClickAct = false;
    public string KeyAct = "None";

    private bool changed = false;

    [Serializable]
    public class ButtonClickedEvent : UnityEvent
    {
    }

    [FormerlySerializedAs("onClick")]
    [SerializeField]
    private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();


    void Start()
    {
        transform.Find("Model").Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void Update()
    {
        if (changed)
        {
            transform.Find("Model").localScale = Vector3.one * SelectSize * ScaleSpeed + transform.Find("Model").localScale * (1-ScaleSpeed);
            if (transform.Find("Model").localScale.x - SelectSize < 0.1f) changed = false;
        }
        else
        {
            if (isOver) transform.Find("Model").localScale = Vector3.one * ScaleSize * ScaleSpeed + transform.Find("Model").localScale * (1-ScaleSpeed);
            else transform.Find("Model").localScale = Vector3.one * ScaleSpeed + transform.Find("Model").localScale * (1-ScaleSpeed);

            if (GetAct()) Press();

        }

        if (Floating)
        {
            transform.Find("Model").localPosition = new Vector3(0,  Mathf.Sin((Time.time+FloatBias) * 2 * Mathf.PI * FloatingSpeed) * FloatScale, 0);
        }
    }

    private void Press()
    {
            state = !state;
            if (state)
            {
                State0.SetActive(false);
                State1.SetActive(true);
            }
            else
            {
                State0.SetActive(true);
                State1.SetActive(false);
            }
            changed = true;
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
        if (ClickAct) return Input.GetMouseButtonDown(0)&&isOver;
        else return Input.GetKeyDown((KeyCode) System.Enum.Parse(typeof(KeyCode), KeyAct));
    }
}
