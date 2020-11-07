using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Game_Manager : MonoBehaviour
{
    public SpriteRenderer[] colours; //Array of colours(buttons)
    private int colourSelect = 0; //Next Random Selected Colour

    public float stayLit = 1f; //time button will stay lit
    private float stayLitCounter = 0f; //counts down time and compared with stayLit

    public float waitBetweenLights = 0.3f; // time to wait between lights
    private float waitBetweenCounter; //counts down time and compared with waitBetweenLights

    private bool shouldBeLit; //Boolean to compare if button should be lit
    private bool shouldBeDark = false; // doubt i will use it

    public List<int> activeSequence; // List of sequence the player has to follow, extended with help of colorSelect
    private int positionInSequence; // marks current position in the sequence
    private int inputInSequence; // what player clicked on

    private bool gameActive; // used to deactivate game when sequence is being shown

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

    }

    // Update is called once per frame
    void Update()
    {

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
