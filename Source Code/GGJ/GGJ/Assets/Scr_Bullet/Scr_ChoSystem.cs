using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class Scr_ChoSystem : MonoBehaviour
{
    int player1 = 1;
    int player2 = 2;
    bool ifPlayer1Chos = true;
    bool ifPlayer2Chos = true;
    bool startChos = false;
    public GameObject prefab;
    public List<GameObject> bullets = new();
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject pointer1;
    public GameObject pointer2;
    int index;

    List<GameObject> cards = new List<GameObject>();
    int[] cardsIndex = new int[3];

    private void Start()
    {
        foreach(var card in bullets)
        {
            CanonController.bullets.Add(card);
        }
    }
    public void StartChos()
    {
        for (int i = 0; i < 3; i++)
        {
            index = Random.Range(0, sprites.Count);
            cardsIndex[i] = index;
            GameObject card = Instantiate(prefab);
            card.GetComponent<SpriteRenderer>().sprite = sprites[index];
            card.transform.position = new Vector2(i * 5 - 5, 0);
            cards.Add(card);
        }
        ifPlayer1Chos = false;
        ifPlayer2Chos = false;
        startChos = true;
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Scoreboard.score > 20)
            {
                StartChos();
                Scoreboard.score -= 20;
            }
        }
        if (startChos)
        {
            if (!ifPlayer1Chos)
            {

                pointer1.transform.position = new Vector3(5 * player1 - 5, 0, 0);

                if (Input.GetKeyDown(KeyCode.S))
                {
                    ifPlayer1Chos = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    player1--;
                    if (player1 == player2)
                        player1--;
                    if (player1 == -1)
                        player1 = 2;
                    if (player1 == player2)
                        player1--;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    player1++;
                    if (player1 == player2)
                        player1++;
                    if (player1 == 3)
                        player1 = 0;
                    if (player1 == player2)
                        player1++;
                }
            }
            if (!ifPlayer2Chos)
            {
                pointer2.transform.position = new Vector3(5 * player2 - 5, 0, 0);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    ifPlayer2Chos = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    player2--;
                    if (player1 == player2)
                        player2--;
                    if (player2 == -1)
                        player2 = 2;
                    if (player1 == player2)
                        player2--;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    player2++;
                    if (player1 == player2)
                        player2++;
                    if (player2 == 3)
                        player2 = 0;
                    if (player1 == player2)
                        player2++;
                }
            }
        }
        if (startChos && ifPlayer1Chos && ifPlayer2Chos)
        {
            pointer1.transform.position = pointer2.transform.position = new Vector3(0, -100, 0);
            AddBulletToClip(0, cardsIndex[player1]);
            AddBulletToClip(1, cardsIndex[player2]);
            foreach (var ca in cards)
            {
                Destroy(ca);
            }
            cards.Clear();
            startChos = false;
        }
    }
    void AddBulletToClip(int playerNumber, int index)
    {
        if(index<=3)
        {
            if (playerNumber == 0)
                CanonController.AddStaticCorreBullet(index+3, 1);
            if (playerNumber == 1)
                CanonController.AddStaticCorreBullet(index+3, 2);
        }
        if(index==4)
        {
            int color = Random.Range(0, 3);
            if(playerNumber == 0)
                for (int i = 0; i < 3; i++)
                    CanonController.AddStaticCorreBullet(color+7, 1);
            if (playerNumber == 1)
                for (int i = 0; i < 3; i++)
                    CanonController.AddStaticCorreBullet(color + 7, 2);
        }
        if(index==5)
        {
            if (playerNumber == 0)
                CanonController.AddStaticCorreBullet(10, 1);
            if (playerNumber == 0)
                CanonController.AddStaticCorreBullet(10, 2);
        }
    }
}


