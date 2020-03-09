using UnityEngine;

public class ChoiceSystem : MonoBehaviour
{
    // The choice enum: use to indicate choices by direction that they appear
    // Alternatively, by index that they were passed in
    public enum Choices {
        Up = 0,
        Left = 1,
        Right = 2,
        Down = 3,
        Invalid = 4,
        Zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
    }

    [SerializeField]
    // List of the four choice panel objects
    // Should be filled in inspector
    private ChoicePanelController[] choicePanels;

    /** 
     * (I'm using the python style of documentation, whatever)
     * 
     * Allows the caller to set the choice panels with text values
     * 
     * @param howMany: how many choices there are 
     * @param optionText: string array of elements
     * 
     * The howMany int is not needed, but I'm keeping it here because it's good to keep in mind
     */
    public void SetChoiceText(int howMany, string[] optionText)
    {
        Debug.Assert(howMany < 4, "Too many choices for UI to support");

        switch (howMany) {
            case 1:
                HandleOneChoice(optionText);
                break;
            case 2:
                HandleTwoChoices(optionText);
                break;
            case 3:
                HandleThreeChoices(optionText);
                break;
            case 4:
                HandleFourChoices(optionText);
                break;
        }
    }

    // Places one choice in the "Down" position
    private void HandleOneChoice(string[] text)
    {
        choicePanels[(int)Choices.Down].SetText(text[0]);
        choicePanels[(int)Choices.Down].SetVisible(true);
    }

    // Places two choices in the "Left" and "Right" positions
    private void HandleTwoChoices(string[] text) {
        choicePanels[(int)Choices.Left].SetText(text[0]);
        choicePanels[(int)Choices.Left].SetVisible(true);

        choicePanels[(int)Choices.Right].SetText(text[1]);
        choicePanels[(int)Choices.Right].SetVisible(true);
    }

    // Places three choices in the "Left", "Right", and "Down" positions
    private void HandleThreeChoices(string[] text)
    {
        choicePanels[(int)Choices.Left].SetText(text[0]);
        choicePanels[(int)Choices.Left].SetVisible(true);

        choicePanels[(int)Choices.Right].SetText(text[1]);
        choicePanels[(int)Choices.Right].SetVisible(true);

        choicePanels[(int)Choices.Down].SetText(text[2]);
        choicePanels[(int)Choices.Down].SetVisible(true);
        
    }

    // Places four choices in four spots
    private void HandleFourChoices(string[] text)
    {
        choicePanels[(int)Choices.Left].SetText(text[0]);
        choicePanels[(int)Choices.Left].SetVisible(true);

        choicePanels[(int)Choices.Right].SetText(text[1]);
        choicePanels[(int)Choices.Right].SetVisible(true);

        choicePanels[(int)Choices.Down].SetText(text[2]);
        choicePanels[(int)Choices.Down].SetVisible(true);

        choicePanels[(int)Choices.Up].SetText(text[3]);
        choicePanels[(int)Choices.Up].SetVisible(true);

    }

    // Must be called after handling choices to reset for the next usage
    public void DisableChoices()
    {
        choicePanels[(int)Choices.Up].SetVisible(false);
        choicePanels[(int)Choices.Left].SetVisible(false);
        choicePanels[(int)Choices.Right].SetVisible(false);
        choicePanels[(int)Choices.Down].SetVisible(false);
    }


}
