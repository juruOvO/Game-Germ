using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scr_SetColor : MonoBehaviour
{
    public enum Species
    {
        Basic,Advanced,Super,HasSet
    }
    public enum eColor
    {
        Red, Yellow, Blue,Orange,Green,Purple,Black
    }
    public Species species=Species.Basic;
    public eColor color;

    void Start()
    {
        if (species != Species.HasSet)
        {
            switch (species)
            {
                case Species.Basic:
                    color = (eColor)Random.Range(0, 3);
                    break;
                case Species.Advanced:
                    color = (eColor)Random.Range(0, 6);
                    break;
                case Species.Super:
                    color = (eColor)Random.Range(0, 7);
                    break;
            }
        }
        switch (color)
        {
            case eColor.Red:
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 1);
                break;
            case eColor.Yellow:
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
                break;
            case eColor.Blue:
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 1);
                break;
            case eColor.Orange:
                GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f);
                break;
            case eColor.Green:
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 1, 0.5f);
                break;
            case eColor.Purple:
                GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 1);
                break;
            case eColor.Black:
                GetComponent<SpriteRenderer>().color = new Color(0.66667f, 0.66667f, 0.66667f);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
