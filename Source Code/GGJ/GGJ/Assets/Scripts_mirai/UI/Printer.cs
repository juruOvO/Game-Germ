using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Printer : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    [SerializeField] float PrintSpeed = 10.0f;
    public bool tipTextPrintIsRunning = false;

    void Awake(){
        tmp = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        tmp.text = "";
        //StartCoroutine(Print("hello"));
    }

    void Update()
    {
        
    }

    public IEnumerator Print(string s){
        tipTextPrintIsRunning = true;
        tmp.text = "";
        for(int i = 0; i < s.Length; i++){
            yield return new WaitForSeconds(1/PrintSpeed);
            tmp.text += s[i];
        }
        Debug.Log("Print Coroutine Finished");
        tipTextPrintIsRunning = false;
    }
}
