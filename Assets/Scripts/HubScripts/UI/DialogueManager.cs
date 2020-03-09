using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    // A ChoiceSystem component that controls the panel UI/text stuff
    // Should be filled in inspector
    ChoiceSystem choiceSystem;

    [SerializeField]
    // The panel UI object holding the main textbox
    // Should be filled in inspector
    private GameObject textBoxContainer;

    [SerializeField]
    // The text component of the text object parented under the textBoxContainer
    // Should be filled in inspector
    private Text textBoxText;

    /**
     * Display choices to the player onscreen.
     * 
     * @param howMany: how many choices do you need
     * @param optionText: which text that will display in each choice
     *
     * Keep in mind that the choice direction/index mapping is that the 0th index
     * maps to the left choice, the 1st index maps to the right choice, the 2nd index
     * maps to the down choice, and the 3rd index maps to the up choice.
     * 
     * Unless you have only 1 choice, which maps to down.
     * 
     */
    public void DisplayChoices(int howMany, string[] optionText)
    {
        DisableTextBox();
        Debug.Assert(optionText.Length == howMany, "Number of choices stated does not match number of choices provided");
        choiceSystem.SetChoiceText(howMany, optionText);
    }

    // Removes choices offscreen after player has chosen
    public void DisableChoices()
    {
        choiceSystem.DisableChoices();
    }

    // Display text into the normal textbox 
    public void DisplayText(string text) {
        textBoxText.text = text;
        textBoxContainer.SetActive(true);
    }

    // Disable the normal textbox
    public void DisableTextBox()
    {
        textBoxContainer.SetActive(false);
    }

    /**
     * Checks for keypresses of the main game keys, otherwise returns invalid.
     *
     * Returns an int based on the ChoiceSystem.Choices directions, should be
     * used for general "Press key to continue" things. 
     */
    public ChoiceSystem.Choices GrabInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            return ChoiceSystem.Choices.Up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            return ChoiceSystem.Choices.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return ChoiceSystem.Choices.Down;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return ChoiceSystem.Choices.Right;
        }
        else
        {
            return ChoiceSystem.Choices.Invalid;
        }

    }

    /**
     * Checks for keypresses associated with choices. 
     *
     * @param choices: how many choices were on screen
     * 
     * Based on how many choices were onscreen, can determine which keypresses
     * are meant for choices or not and can map back to the index within the
     * array for easier programming.
     *
     * Returns an int based on the ChoiceSystem.Choices indexes
     */
    public ChoiceSystem.Choices GrabInput(int choices) {
        if (Input.GetKeyDown(KeyCode.W) && choices == 4)
        {
            return ChoiceSystem.Choices.Three;
        }
        else if (Input.GetKeyDown(KeyCode.A) && choices != 2)
        {
            return ChoiceSystem.Choices.Zero;
        }
        else if (Input.GetKeyDown(KeyCode.S) && choices > 1)
        {
            if (choices == 1)
                return ChoiceSystem.Choices.Zero;
            return ChoiceSystem.Choices.Two;
        }
        else if (Input.GetKeyDown(KeyCode.D) && choices > 1)
        {
            return ChoiceSystem.Choices.One;
        }
        else {
            return ChoiceSystem.Choices.Invalid;
        }

    }

}
