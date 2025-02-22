using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEffect : MonoBehaviour
{
    [Range(0,100)] public float health = 100;
    public bool OnEffect = true;
    [SerializeField] private SpriteRenderer waveSprite;

    void Update()
    {
        if(OnEffect){
            Color tempColor = waveSprite.color;
            tempColor.g = (155 + health)/255f;
            tempColor.b = (155 + health)/255f;
            tempColor.r = 1;          
            waveSprite.color = tempColor;
        }else{
            waveSprite.color = Color.white;
        }       
    }
}
