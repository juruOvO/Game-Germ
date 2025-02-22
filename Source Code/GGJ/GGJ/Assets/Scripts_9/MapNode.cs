using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapNode : MonoBehaviour
{   
    public int layer;
    public int index;
    public float OverScale = 1.5f;
    public float UpdateSpeed = 0.1f;
    public bool isOver = false;
    public int state;
    public string Type;
    public bool ready = false;

    void Start(){
        StartCoroutine(CoolDown());
    }

    void Update(){
        if (isOver)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(OverScale, OverScale, 1), UpdateSpeed);
            if (Input.GetMouseButtonDown(0)&&state==3&&ready)
            {
                transform.parent.GetComponent<MapVisualize>().UpdatePlayerPosition(layer, index);
            }
            
        }
        else
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), UpdateSpeed);
        
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(2f);
        ready = true;
    }

    public void OnMouseOver()
    {
        isOver = true;
    }

    public void OnMouseExit()
    {
        isOver = false;
    }

    public void ShowText(string text){
        Text textObj = transform.Find("Canvas").Find("Text").GetComponent<Text>();
        textObj.text = text;
    }

    public void SetType(string Type){
        transform.Find("Model").Find(Type).gameObject.SetActive(true);
        this.Type = Type;
    }

    public void SetColor(Color color){
        transform.Find("Model").Find("Circle").GetComponent<SpriteRenderer>().color = color;
        transform.Find("Canvas").Find("Text").GetComponent<Text>().color = Color.white - color + Color.black;
    }

    public void SetState(int State){
        state = State;
        // if (State == 0)
        //     SetColor(Color.grey);
        // else if (State == 1)
        //     SetColor(Color.white);
        // else if (State == 2)
        //     SetColor(Color.red);
        // else if (State == 3)
        //     SetColor(Color.green);
        GameObject temp = transform.Find("Model").Find(Type).gameObject;
        if (State == 0)
            temp.GetComponent<SpriteRenderer>().color = (Color.grey+Color.white)/2;
        else if (State == 1)
            temp.GetComponent<SpriteRenderer>().color = Color.white;
        else if (State == 2)
            temp.GetComponent<SpriteRenderer>().color = (Color.red+Color.white)/2;
        else if (State == 3)
            temp.GetComponent<SpriteRenderer>().color = (Color.green+Color.white)/2;
    }

}
