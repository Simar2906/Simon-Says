using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public SpriteRenderer[] colours;
    private int colourSelect = 0;
    public float stayLit = 0f;
    private float stayLitCounter = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(stayLitCounter > 0)
        {
            stayLitCounter -= Time.deltaTime;
        }
        else
        {
            colours[colourSelect].color =  new Color (colours[colourSelect].color.r, colours[colourSelect].color.g, colours[colourSelect].color.b, 0.5f);
        }
    }
    public void StartGame()
    {
        colourSelect = Random.Range(0, colours.Length);
        colours[colourSelect].color =  new Color (colours[colourSelect].color.r, colours[colourSelect].color.g, colours[colourSelect].color.b, 1f);
        stayLitCounter = stayLit;
    }

}
