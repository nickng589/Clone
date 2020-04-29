using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDialogueSequence: MonoBehaviour 
{
    [SerializeField]
    DialogueManager dm;

    private void Start()
    {
        StartCoroutine("ExampleCoroutine");
    }

    /**
     * Here is an example of how a dialogue scene could work, see below for a
     * line-by-line explanation.
     */
    IEnumerator ExampleCoroutineWithoutComments()
    {
        dm.DisplayText("Hello");
        yield return new WaitWhile(() => dm.GrabInput() != ChoiceSystem.Choices.Right);

        dm.DisplayText("Here is the second text, press D to continue and see choices");
        yield return new WaitWhile(() => Input.anyKeyDown == false);
        yield return new WaitWhile(() => dm.GrabInput() != ChoiceSystem.Choices.Right);

        dm.DisableTextBox();
        dm.DisplayChoices(3, new string[] { "Press A for choice 1", "Press D for choice 2", "Press S for choice 3" });

        yield return new WaitWhile(() => Input.anyKey == true);
        yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

        dm.DisableChoices();
        switch (dm.GrabInput(3))
        {
            case ChoiceSystem.Choices.Zero:
                dm.DisplayText("You chose choice 1! Press d to close");
                break;
            case ChoiceSystem.Choices.One:
                dm.DisplayText("You chose choice 2! Press d to close");
                break;
            case ChoiceSystem.Choices.Two:
                dm.DisplayText("You chose choice 3! Press d to close");
                break;
        }

        yield return new WaitWhile(() => Input.anyKey == true);
        yield return new WaitWhile(() => dm.GrabInput() != ChoiceSystem.Choices.Right);

        dm.DisableTextBox();
        yield return null;
    }

    /**
     * Here is an example of how a dialogue scene could work, explained. 
     */
    IEnumerator ExampleCoroutine() {
        // Initially starts off with something in the textbox
        dm.DisplayText("This is the first text, press D to continue");


        // Now we want to wait for a keypress on D, so we yield WaitWhile
        // Documentation for Wait While: https://docs.unity3d.com/ScriptReference/WaitWhile.html
        yield return new WaitWhile(() => dm.GrabInput() != ChoiceSystem.Choices.Right);

        // We will return another text, as if there is more to converse about
        dm.DisplayText("Here is the second text, press D to continue and see choices");

        // This is necessary to prevent all of the code running in the same frame that right is pushed down
        // Waiting for right to be released
        yield return new WaitWhile(() => Input.anyKeyDown == false);

        // We wait for right to be pressed again
        yield return new WaitWhile(() => dm.GrabInput() != ChoiceSystem.Choices.Right);

        // We are now presenting choices, so we disable the main textbox
        dm.DisableTextBox();
        // We show the choices
        dm.DisplayChoices(3, new string[] { "Press A for choice 1", "Press D for choice 2", "Press S for choice 3" });


        // Waiting for right to be released
        yield return new WaitWhile(() => Input.anyKey == true);
        // We wait for something to be pressed - we use GrabInput with an argument of 3 because we have 3 choices
        // We need to see if A, S, or D, are pressed and that is what GrabInput(3) will look for here
        yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

        // A key has been pressed, we don't need to show the choices anymore
        dm.DisableChoices();

        // Based on which key was pressed, we can show a different result 
        // GrabInput() maps back to the indices of the array so we can just look at that to determine what to do next
        switch (dm.GrabInput(3)) {
            case ChoiceSystem.Choices.Zero:
                dm.DisplayText("You chose choice 1! Press d to close");
                break;
            case ChoiceSystem.Choices.One:
                dm.DisplayText("You chose choice 2! Press d to close");
                break;
            case ChoiceSystem.Choices.Two:
                dm.DisplayText("You chose choice 3! Press d to close");
                break;
        }

        // Wait for keys to be released
        yield return new WaitWhile(() => Input.anyKey == true);
        // Wait for right to be pressed
        yield return new WaitWhile(() => dm.GrabInput() != ChoiceSystem.Choices.Right);

        // The sequence is over, disable the textbox
        dm.DisableTextBox();
        yield return null;
    }

    

}
