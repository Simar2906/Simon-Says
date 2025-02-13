﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public SpriteRenderer[] colours;
    private int colourSelect = 0;

    public float stayLit = 0f;
    private float stayLitCounter = 0f;

    public float waitBetweenLights;
    private float waitBetweenCounter;

    private bool shouldBeLit;
    private bool shouldBeDark = false;

    public List<int> activeSequence;
    private int positionInSequence;

    private bool gameActive;
    private int inputInSequence;
    void Update()
    {
        if (shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;
            if (stayLitCounter < 0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
                shouldBeLit = false;

                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLights;

                positionInSequence++;
                Debug.Log("Lit time end");
            }
        }
        if(shouldBeDark)
        {
            waitBetweenCounter -= Time.deltaTime;

            if(positionInSequence >= activeSequence.Count) 
            {
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if(waitBetweenCounter <0)
                {
                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }
    public void StartGame()
    {
        activeSequence.Clear(); 
        
        positionInSequence = 0;
        inputInSequence = 0;
        colourSelect = Random.Range(0, colours.Length);

        activeSequence.Add(colourSelect);
        colours[activeSequence[positionInSequence]].color =  new Color (colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);

        stayLitCounter = stayLit;
        shouldBeLit = true;
    }

    public void ColourPressed(int whichButton)
    {
        if(gameActive)
        {
            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correct");

                inputInSequence ++;

                if(inputInSequence >= activeSequence.Count)
                {
                    positionInSequence = 0;
                    inputInSequence = 0;
                    colourSelect = Random.Range(0, colours.Length);

                    activeSequence.Add(colourSelect);
                    colours[activeSequence[positionInSequence]].color =  new Color (colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);

                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    gameActive = false;
                }
            }
            else
                Debug.Log("Wrong");
                gameActive = false;
        }
    }
}
