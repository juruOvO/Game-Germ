using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CanonController : MonoBehaviour
{
    public static List<int> staticBulletList1 = new();
    public static List<int> staticBulletList2 = new();
    public int Type = 0;
    public Transform FirePos;
    public GameObject Clip;
    public GameObject TestBullet;
    private ClipVisualize cv;

    public float rotspeed = 2f;
    public float rotspeedmax = 5f;
    public float rotspeeddecay = 0.8f;
    private float rotspeedtemp = 0f;
    public float RotAngleLimitL = 15f;
    public float RotAngleLimitR = -105f;

    public float reloadTime = 1f;
    public float FireTime = 0.5f;
    public float bulletSpeed = 10f;
    public float bulletSize = 0.5f;
    public int bulletSelected = 0;
    public int ShowNum = 3;

    public float MaxChargeTime = 2f;
    public float ChargeTime = 0f;
    public float ChargeTimeDecay = 0.995f;
    private UnityEngine.UI.Image chargeBar;

    public int bulletCount = 0;
    static public List<GameObject> bullets = new (); 
    public List<GameObject> bulletList = new();
    public List<GameObject> bulletListInGame = new();
    public List<GameObject> bulletListTemp = new();
    public List<GameObject> bulletListRemoveInGame = new();
    public List<GameObject> bulletListRemoveTemp = new();

    public bool isFiring = false;
    public bool isReloading = false;
    public bool InGame = false;
    bool start = false;

    private KeyCode [] KeySets;
    private bool InputEnabled = false;
    

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        transform.Find("ChargeBar").gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
        chargeBar = transform.Find("ChargeBar").Find("Image").gameObject.GetComponent<UnityEngine.UI.Image>();
        FirePos = transform.Find("FirePos");
        if (Type == 0)
            KeySets = new KeyCode[] { KeyCode.A, KeyCode.D, KeyCode.S, KeyCode.W };
        else
            KeySets = new KeyCode[] { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.DownArrow, KeyCode.UpArrow };
        cv = Clip.GetComponent<ClipVisualize>();
        StartCoroutine(FireCoroutine());
    }

    void FixedUpdate()
    {
        float angle = transform.rotation.eulerAngles.z>180 ? transform.rotation.eulerAngles.z-360 : transform.rotation.eulerAngles.z;
        if (Input.GetKey(KeySets[0]) && InputEnabled && angle < RotAngleLimitL)
            rotspeedtemp = Mathf.Clamp(rotspeedtemp + rotspeed, -rotspeedmax, rotspeedmax);
        if (Input.GetKey(KeySets[1]) && InputEnabled && angle > RotAngleLimitR)
            rotspeedtemp = Mathf.Clamp(rotspeedtemp - rotspeed, -rotspeedmax, rotspeedmax);
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotspeedtemp);
        if (Mathf.Abs(rotspeedtemp) > 0.1) rotspeedtemp *= rotspeeddecay;
        else rotspeedtemp = 0;
    }

    void Update()
    {
        if (!start)
        {
            start = true;
            if (Type == 0)
            {
                foreach (int index in staticBulletList1)
                {
                    Debug.Log(index);
                    AddCorreBullet(index);
                }
            }
            else
            {
                foreach (int index in staticBulletList2)
                {
                    AddCorreBullet(index);
                }
            }
            GameStart();
        }

        if (isFiring || isReloading)
        {
            if (ChargeTime > 0.02f)
                ChargeTime *= ChargeTimeDecay;
            else
                ChargeTime = 0;
        }
        chargeBar.fillAmount = ChargeTime / MaxChargeTime;
    }
    public void AddCorreBullet(int index)
    {
        GameObject bullet = Instantiate(bullets[index], FirePos.position, FirePos.rotation);
        bullet.SetActive(false);
        AddBullet(bullet);
    }
    static public void AddStaticCorreBullet(int index,int player)
    {
        Debug.Log(index);
        if (player == 1)
            staticBulletList1.Add(index);
        if (player == 2)
            staticBulletList2.Add(index);        
    }
    void GameStart()
    {
        bulletListInGame.Clear();
        bulletListTemp.Clear();
        bulletListInGame.AddRange(bulletList);
        ReloadBullet();
        InGame = true;
        InputEnabled = true;
    }

    void GameOver()
    {
        InGame = false;
        InputEnabled = false;
        while(bulletListRemoveInGame.Count > 0)
        {
            if (bulletListRemoveInGame[0] != null)
                Destroy(bulletListRemoveInGame[0]);
            bulletListRemoveInGame.RemoveAt(0);
        }
    }

    IEnumerator FireCoroutine()
    {
        bool keytemp = false;
        while (true)
        {
            if (Input.GetKeyDown(KeySets[3]) && InputEnabled && !isFiring)
                bulletSelected = (bulletSelected >= ShowNum-1)||(bulletSelected>=bulletCount-1) ? 0 : bulletSelected+1;
                
            if (Input.GetKey(KeySets[2]) && InputEnabled) 
            {
                ChargeTime += Time.deltaTime;
                //if (ChargeTime > MaxChargeTime) ChargeTime -= MaxChargeTime;
                keytemp = true;
            }
            else 
            {
                if (ChargeTime > 0.02f)
                    ChargeTime *= ChargeTimeDecay;
                else
                    ChargeTime = 0;
            }
            if (keytemp && !Input.GetKey(KeySets[2]))
                {
                    keytemp = false;
                    if (!InputEnabled){
                        ChargeTime = 0;
                        yield return null;
                        continue;
                    }
                    GameObject bullet = Instantiate(NextBullet, FirePos.position+FirePos.up*0.6f, FirePos.rotation);
                    //bullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
                    bullet.SetActive(true);
                    bullet.GetComponent<Rigidbody2D>().velocity = FirePos.up * bulletSpeed * Mathf.Clamp(ChargeTime / MaxChargeTime,0.1f,1);
                    audioSource.Play();    
                
                    if (bulletSelected != 0)
                    {
                        GameObject temp = bulletListTemp[bulletCount - bulletSelected - 1];
                        bulletListTemp.RemoveAt(bulletCount - bulletSelected - 1);
                        bulletListTemp.Insert(bulletCount-1, temp);
                        temp = cv.clip[bulletCount - bulletSelected - 1];
                        cv.clip.RemoveAt(bulletCount - bulletSelected - 1);
                        cv.clip.Insert(bulletCount-1, temp);
                    }
                    bulletCount--;
                    
                    while (bulletSelected >= bulletCount && bulletSelected > 0)
                        bulletSelected--;

                    if (bulletCount == 0)
                    {
                        isReloading = true;
                        yield return new WaitForSeconds(reloadTime);
                        ReloadBullet();
                        isReloading = false;
                    }
                    else
                    {
                        isFiring = true;
                        yield return new WaitForSeconds(FireTime);
                        isFiring = false;
                    }
                }
            yield return null;
        }
    }

    public void ReloadBullet(){
        while(bulletListRemoveTemp.Count > 0)
        {
            if (bulletListRemoveTemp[0] != null)
                Destroy(bulletListRemoveTemp[0]);
            bulletListRemoveTemp.RemoveAt(0);
        }
        System.Random randompos = new System.Random();
        bulletListTemp.Clear();
        bulletListTemp.AddRange(bulletListInGame);
        for (int i = 0; i < bulletListTemp.Count; i++)
        {
            int randomIndex = randompos.Next(i, bulletListTemp.Count);
            GameObject temp = bulletListTemp[i];
            bulletListTemp[i] = bulletListTemp[randomIndex];
            bulletListTemp[randomIndex] = temp;
        }
        bulletCount = bulletListTemp.Count;
        cv.InitClip();
    }

    public void SetType(int type)
    {
        Type = type;
    }

    public void DisableInput()
    {
        InputEnabled = false;    
    }

    public void EnableInput()
    {
        InputEnabled = true;
    }

    public GameObject NextBullet
    {
        get
        {
            if (bulletListTemp.Count <= bulletSelected || bulletCount == 0)
                return null;
            return bulletListTemp[bulletCount - bulletSelected - 1];
        }
        set
        {
            bulletCount++;
            bulletListTemp.Insert(bulletCount-1, value);
            GameObject t = Instantiate(value, cv.pos2.position, Quaternion.identity);
            Destroy(t.GetComponent<Rigidbody2D>());
            Destroy(t.GetComponent<BoxCollider2D>());
            Destroy(t.GetComponent<CircleCollider2D>());
            Destroy(t.GetComponent<Scr_BulletBase>());
            t.tag = "Untagged";
            t.transform.localScale = new Vector3(0, 0, 0);
            t.SetActive(false);
            cv.clip.Insert(bulletCount-1, t);
        }
    }

    public void AddBulletTemp(GameObject bullet, int pos = -1)
    {
        if (pos == -1){
            System.Random randompos = new System.Random();
            bulletCount++;
            int randomIndex = randompos.Next(0,bulletCount-1);
            bulletListTemp.Insert(randomIndex, bullet);
            GameObject t = Instantiate(bullet, cv.pos1.position, Quaternion.identity);
            Destroy(t.GetComponent<Rigidbody2D>());
            Destroy(t.GetComponent<BoxCollider2D>());
            Destroy(t.GetComponent<CircleCollider2D>());
            Destroy(t.GetComponent<Scr_BulletBase>());
            t.tag = "Untagged";
            t.transform.localScale = new Vector3(0, 0, 0);
            t.SetActive(false);

            cv.clip.Insert(randomIndex, t);
        }else{
            bulletCount++;
            bulletListTemp.Insert(pos, bullet);
            GameObject t = Instantiate(bullet, cv.pos1.position, Quaternion.identity);
            Destroy(t.GetComponent<Rigidbody2D>());
            Destroy(t.GetComponent<BoxCollider2D>());
            Destroy(t.GetComponent<CircleCollider2D>());
            Destroy(t.GetComponent<Scr_BulletBase>());
            t.tag = "Untagged";
            t.transform.localScale = new Vector3(0, 0, 0);
            t.SetActive(false);
            cv.clip.Insert(pos, t);
        }
        bullet.SetActive(false);
        bulletListRemoveTemp.Add(bullet);
    }

    public void AddBulletInGame(GameObject bullet, int pos = -2)
    {
        if (pos != -2 && pos != -3)
            AddBulletTemp(bullet, pos);
        bulletListInGame.Add(bullet);
        if (pos != -3)
            bulletListRemoveInGame.Add(bullet);
    }

    public void AddBullet(GameObject bullet, int pos = -3)
    {
        AddBulletInGame(bullet, pos);
        bulletList.Add(bullet);
    }

    public void RemoveBulletTemp(GameObject bullet)
    {
        GameObject temp = bulletListTemp[bulletCount - 1];
        bulletListTemp.Remove(bullet);
        Destroy(bullet);
        bulletCount = bulletListTemp.FindIndex(x => x == temp) + 1;
    }

    public void RemoveBulletInGame(GameObject bullet)
    {
        bulletListInGame.Remove(bullet);
        RemoveBulletTemp(bullet);
    }

    public void RemoveBullet(GameObject bullet)
    {
        bulletList.Remove(bullet);
        RemoveBulletInGame(bullet);
    }

    public void RemoveBulletTemp(int pos)
    {
        Destroy(bulletListTemp[pos]);
        bulletListTemp.RemoveAt(pos);
        if (pos < bulletCount)
            bulletCount--;
    }

    public void RemoveBulletInGame(int pos)
    {
        RemoveBulletTemp(pos);
        bulletListInGame.RemoveAt(pos);
    }

    public void RemoveBullet(int pos)
    {
        RemoveBulletTemp(pos);
        bulletList.RemoveAt(pos);
    }

}
