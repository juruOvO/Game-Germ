using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public static int score = 0;
    private CanvasGroup scoreBoard;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreShow;
    [SerializeField] private RectTransform scoreRectTransform;
    [SerializeField] private float scaleDuration = 0.5f;
    private bool scaleIsRunning = false;

    void Awake(){
        scoreBoard = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        scoreText.text = "Score : ";
        scoreShow.text = score.ToString();
    }

    void Update()
    {
        scoreShow.text = score.ToString();
        if (Input.GetKeyDown(KeyCode.G)){
            AttainScore(1);
        }
    }


    void AttainScore(int sc){
        score += sc;
        scoreShow.text = score.ToString();

        if(!scaleIsRunning){
            StartCoroutine(Scale());
        }
    }

    void TurnOnScoreboard(){
        scoreBoard.alpha = 1;
    }

    void TurnDownScoreboard(){
        scoreBoard.alpha = 0;
    }

    IEnumerator Scale(){
        scaleIsRunning = true;
        yield return null;
        scoreRectTransform.localScale = Vector3.zero;
        for(int i = 0; i < 10; i++){
            scoreRectTransform.localScale +=  new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(scaleDuration / 10);
        }
        scaleIsRunning = false;
    }
}
