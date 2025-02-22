using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Generator : MonoBehaviour
{
    public static int ObjectNum;
    public static bool EndGame;
    public GameObject targetEnemy;
    public int generateBaseNum;
    public int generateNum;
    public float intervalTime;
    public float sessionTime;
    public float climaxTime;
    public float startTime;
    public float breakTime;

    public virtual void SetParams()
    {

    }

    public virtual void SetIntensity() { }
    private void Awake()
    {
        SetIntensity();
        SetParams();
    }
    public void Start()
    {
        EndGame = false;
        ObjectNum = 0;
        InvokeRepeating("CreateEnemy", startTime, intervalTime);
        Invoke("StopGenerate", sessionTime);
        InvokeRepeating("CreateEnemy", breakTime, intervalTime);
        Invoke("StopGenerate", climaxTime);
        if(ObjectNum ==0) {
            EndGame = true;
        }
    }

    public void Update()
    {
        
    }

    private void CreateEnemy()
    {
        generateNum = (int)(generateBaseNum * math.tanh(Time.time/72.819157f));
        //If player is still alive

        for (int i=0;i<generateNum;i++)
        {
            //Should add function aim for selecting enemy type by probability.
            //A-0.3 B-0.3 C-0.4/3 D-0.4/3 E-0.4/3 BOSS-Manual
            float randomSelect = UnityEngine.Random.Range(0f, 1f);
            if (randomSelect >= 0 && randomSelect <= 0.34)
            {
                targetEnemy = GameObject.Find("EnemyARoot");
            }
            else if (randomSelect > 0.34 && randomSelect <= 0.64)
            {
                targetEnemy = GameObject.Find("EnemyBRoot");
            }
            else if (randomSelect > 0.64 && randomSelect <= 0.76)
            {
                targetEnemy = GameObject.Find("EnemyCRoot");
            }
            else if (randomSelect > 0.76 && randomSelect <= 0.88)
            {
                targetEnemy = GameObject.Find("EnemyDRoot");
            }
            else
            {
                targetEnemy = GameObject.Find("EnemyERoot");
            }

            Vector3 randomPosition = this.transform.position;
            randomPosition.x = this.transform.position.x + UnityEngine.Random.Range(GLOBAL.LEFTBOUND, GLOBAL.RIGHTBOUND);
            randomPosition.y = GLOBAL.UPPERBOUND + 1f;
            randomPosition.z = 0;
            GameObject temp = Instantiate(targetEnemy, randomPosition, Quaternion.identity);
            temp.tag = "Enemy";
            temp.AddComponent<Scr_SetColor>().species=Scr_SetColor.Species.Advanced;
            ObjectNum++;
        }
    }
    private void StopGenerate()
    {
        CancelInvoke("CreateEnemy");
    }

}
