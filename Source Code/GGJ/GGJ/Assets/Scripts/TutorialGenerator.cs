using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGenerator : MonoBehaviour
{
    public GameObject targetEnemy;
    public int num;
    public static bool confirm;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
        confirm = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(confirm)
        {
            if (num <= 5)
            {
                InvokeRepeating("CreateEnemy", 0f, 0.5f);
            }
            else
            {
                StopGenerate();
                if (Generator.ObjectNum <= 0)
                {
                    confirm = false;
                    Generator.EndGame=true;
                }
                
            }
        }
    }
    private void CreateEnemy()
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
        temp.AddComponent<Scr_SetColor>().species = Scr_SetColor.Species.Advanced;
        num++;
        Generator.ObjectNum = num;
    }
    private void StopGenerate()
    {
        CancelInvoke("CreateEnemy");
    }
}
