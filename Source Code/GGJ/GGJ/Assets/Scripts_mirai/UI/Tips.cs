using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tips : MonoBehaviour
{
    private bool visible = true;
    private CanvasGroup canvansGroup;
    private Printer tipTextPrinter;
    private Coroutine tipTextPrintCoroutine;
    private int tipCount = 0;
    
    private float timer = 0;
    private RectTransform rectTransform;
    public List<string> tips = new List<string>();
    private int tipSize;

    void Awake(){
        canvansGroup = GetComponent<CanvasGroup>();
        tipTextPrinter = GetComponentInChildren<Printer>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        PrintTips(tips[0]);
        StartCoroutine(Shake());
        tipSize = tips.Count;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(visible){
            if(Input.GetKeyDown(KeyCode.Space)){
                TurnDownTips();
                tipCount++;
                StartCoroutine(Shake());
                TurnOnTips();
                if(tipCount < tipSize)
                    PrintTips(tips[tipCount]);
                if(tipCount == tipSize)
                {
                    TurnDownTips();
                    TutorialGenerator.confirm = true;
                }
            }
        }
    }
    
    private void TurnDownTips(){
        visible = false;
        canvansGroup.alpha = 0;
        canvansGroup.blocksRaycasts = false;
        if(tipTextPrinter.tipTextPrintIsRunning && tipTextPrintCoroutine != null){
            StopCoroutine(tipTextPrintCoroutine);
        }
    }

    private void TurnOnTips(){
        visible = true;
        canvansGroup.alpha = 1;
        canvansGroup.blocksRaycasts = true;
    }
    
    void PrintTips(string s){
        if(tipTextPrinter.tipTextPrintIsRunning && tipTextPrintCoroutine != null){
            StopCoroutine(tipTextPrintCoroutine);
        }
        tipTextPrintCoroutine = StartCoroutine(tipTextPrinter.Print(s));
    }

    IEnumerator Shake(){
        yield return null;
        rectTransform.position += new Vector3(0, 10, 0);
        yield return new WaitForSeconds(0.05f);
        rectTransform.position += new Vector3(0, -10, 0);
    }
}
