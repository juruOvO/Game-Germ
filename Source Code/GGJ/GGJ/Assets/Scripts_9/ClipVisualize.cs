using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ClipVisualize : MonoBehaviour
{
    [SerializeField] private GameObject Canon;

    private CanonController cc;
    public float ts=0;
    public Transform pos1;
    public Transform pos2;
    public List<GameObject> clip = new List<GameObject>();


    private int ShowNum = 3;
    public float ShowSpeed = 0.1f;
    public float LoadSpeed = 0.5f;
    public float ShowScale = 0.3f;

    private void Start()
    {
        cc = Canon.GetComponent<CanonController>();
        ShowNum = cc.ShowNum;
        if (cc.Type == 0){
            pos2 = transform.Find("FillPos");
            pos1 = transform.Find("LoadPos");
        }
        else{
            pos1 = transform.Find("FillPos");
            pos2 = transform.Find("LoadPos");
        }
    }

    private void Update()
    {
        if (ts < cc.bulletCount-0.02f) ts += LoadSpeed*Time.deltaTime;
        else if (ts > cc.bulletCount+0.02f) ts -= LoadSpeed*Time.deltaTime;
        else ts = cc.bulletCount;
        for (int i = 0; i < clip.Count; i++)
        {
            float scale_pct = 0.5f-Mathf.Cos((1f-Mathf.Clamp(ts-(float)i-ShowNum,0,1))*(1f-Mathf.Clamp((float)i+1-ts,0,1))*Mathf.PI)/2;
            float pos_pct = Mathf.Clamp(((float)i+1-ts+ShowNum)/(ShowNum+1),0,1);
            if (scale_pct < 0.01f) clip[i].SetActive(false);
            else {
                scale_pct *= ShowScale;
                clip[i].SetActive(true);
                if (i <= cc.bulletCount-1)
                    if (i == cc.bulletCount-cc.bulletSelected-1)
                    {
                        if (i==0) pos_pct = Mathf.Clamp(((float)i+1-Mathf.Floor(ts)+ShowNum)/(ShowNum+1),0,1);
                        clip[i].transform.position = (Vector3.Lerp(pos1.position, pos2.position, pos_pct)+new Vector3(0,0.1f,0))*ShowSpeed + clip[i].transform.position*(1-ShowSpeed);
                    }
                    else 
                    {
                        if (i > cc.bulletCount-cc.bulletSelected-1) pos_pct = Mathf.Clamp(((float)i+1-Mathf.Floor(ts)+ShowNum)/(ShowNum+1),0,1);
                        clip[i].transform.position = Vector3.Lerp(pos1.position, pos2.position, pos_pct)*ShowSpeed + clip[i].transform.position*(1-ShowSpeed);
                    }

                else
                    clip[i].transform.position = new Vector3(clip[i].transform.position.x,(pos2.position.y+0.5f)*ShowSpeed + clip[i].transform.position.y*(1-ShowSpeed),clip[i].transform.position.z);
                clip[i].transform.localScale = new Vector3(scale_pct, scale_pct, scale_pct)*ShowSpeed + clip[i].transform.localScale*(1-ShowSpeed);
            }
        }
    }

    public void InitClip()
    {
        ShowNum = cc.ShowNum;
        while (clip.Count > 0)
        {
            Destroy(clip[0]);
            clip.RemoveAt(0);
        }
        for (int i = 0; i < cc.bulletCount; i++)
        {
            GameObject bullet = Instantiate(cc.bulletListTemp[i], pos1.position, Quaternion.identity);
            bullet.transform.SetParent(transform.Find("Bullets"));
            bullet.transform.localScale = new Vector3(0, 0, 0);
            Destroy(bullet.GetComponent<Rigidbody2D>());
            Destroy(bullet.GetComponent<BoxCollider2D>());
            Destroy(bullet.GetComponent<CircleCollider2D>());
            Destroy(bullet.GetComponent<Scr_BulletBase>());
            bullet.tag = "Untagged";
            bullet.SetActive(false);
            clip.Add(bullet);
        }
        ts = cc.bulletCount + 3;

    }

}
