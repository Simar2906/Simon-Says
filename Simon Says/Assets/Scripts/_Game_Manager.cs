using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Game_Manager : MonoBehaviour
{
    public SpriteRenderer[] colours; //Array of colours(buttons)
    public AudioSource[] buttonSounds;
    private int colourSelect = 0; //Next Random Selected Colour

    public float stayLit = 1f; //time button will stay lit
    private float stayLitCounter = 0f; //counts down time and compared with stayLit

    public float waitBetweenLights = 0.3f; // time to wait between lights
    private float waitBetweenCounter; //counts down time and compared with waitBetweenLights

    private bool shouldBeLit; //Boolean to compare if button should be lit
    private bool shouldBeDark = false; // doubt i will use it

    public List<int> activeSequence; // List of sequence the player has to follow, extended with help of colorSelect
    private int positionInSequence; // marks current position in the sequence
    private int inputInSequence; // number to be inputted in the sequence compared with active sequence

    private bool gameActive; // used to deactivate game when sequence is being shown

    public AudioSource correct;
    public AudioSource incorrect;


    // Start is called before the first frame update
    public void StartGame()
    {
        activeSequence.Clear();
        stayLitCounter = stayLit;
        positionInSequence = 0;
        inputInSequence = 0;
        colourSelect = Random.Range(0, colours.Length);
        activeSequence.Add(colourSelect);
        colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
        buttonSounds[activeSequence[positionInSequence]].Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldBeLit)
        {
            stayLitCounter -= Time.deltaTime;
            if(stayLitCounter<=0)
            {
                colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 0.5f);
                buttonSounds[activeSequence[positionInSequence]].Stop();
                
                shouldBeLit = false;
                shouldBeDark = true;
                waitBetweenCounter = waitBetweenLights;

                positionInSequence ++;
                Debug.Log("Lit time end");
            }
        }
        else
        {
            waitBetweenCounter -= Time.deltaTime;
            if(positionInSequence >= activeSequence.Count) 
            {
                shouldBeDark = false;
                gameActive = true;
            }
            else
            {
                if(waitBetweenCounter <= 0)
                {
                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                    buttonSounds[activeSequence[positionInSequence]].Play();
                    
                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    shouldBeDark = false;
                }
            }
        }
    }
    public void ColourPressed(int whichButton)
    {
        if (gameActive)
        {
            if (activeSequence[inputInSequence] == whichButton)
            {
                Debug.Log("Correct");
                inputInSequence++;

                if (inputInSequence >= activeSequence.Count)
                {
                    positionInSequence = 0;
                    inputInSequence = 0;
                    colourSelect = Random.Range(0, colours.Length);
                    activeSequence.Add(colourSelect);

                    colours[activeSequence[positionInSequence]].color = new Color(colours[activeSequence[positionInSequence]].color.r, colours[activeSequence[positionInSequence]].color.g, colours[activeSequence[positionInSequence]].color.b, 1f);
                    buttonSounds[activeSequence[positionInSequence]].Play();
                    stayLitCounter = stayLit;
                    shouldBeLit = true;
                    gameActive = false;
                    correct.Play();

                }
            }
            else
            {
                Debug.Log("Wrong");
                incorrect.Play();

                GameEnd();
            }
            gameActive = false;
        }
    }
    public void GameEnd()
    {
        
    }
}
