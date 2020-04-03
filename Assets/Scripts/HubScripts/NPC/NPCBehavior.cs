using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    private NPCmanager npcmanager;

    [SerializeField]
    [Tooltip("The dialogue manager for the npc")]
    DialogueManager dm;

    [SerializeField]
    [Tooltip("The player")]
    HubPlayerController player;

    [SerializeField]
    [Tooltip("The player number")]
    int player_num;
    #endregion

    public bool canMove;

    public float walkTimer;

    Rigidbody2D npcRigidBody;

    void Start()
    {
        canMove = true;
        walkTimer = 1;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown("space") && player.canMove == true)
        {
            GetComponentInParent<NPCMovement>().canMove = false;
            StartCoroutine("Example");
        }
    }

    IEnumerator Example()
    {
        player.canMove = false;
        //dm.DisplayText(SceneDialogue.test);
        switch (player_num) 
        {
            case 2:
                dm.DisplayText("Hey! A newcomer! How are you doing?");
                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));

                dm.DisableTextBox();
                dm.DisplayChoices(3, new string[] { "Press A for:  I’m doing great! How are you doing?", "Press D for: Where am I?", "Press S for: Why do you look so strange?" });

                yield return new WaitWhile(() => Input.anyKey == true);
                yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

                dm.DisableChoices();

                switch (dm.GrabInput(3))
                {
                    case ChoiceSystem.Choices.Zero:
                        dm.DisplayText("I’m always doing great! I enjoy the puzzles! Press space to close");
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText("No one knows what this place is, it just is! Press space to close");
                        break;
                    case ChoiceSystem.Choices.Two:
                        dm.DisplayText(" HAHA! That’s just the way I look kiddo. Press space to close");
                        break;
                }

                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));
                break;
            case 3:
                dm.DisplayText("Hey how has everyone been treating you?");
                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));

                dm.DisableTextBox();
                dm.DisplayChoices(3, new string[] { "Press A for: Great! Who are you?", "Press D for: Not too bad.", "Press S for: Terrible, get me out of here." });

                yield return new WaitWhile(() => Input.anyKey == true);
                yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

                dm.DisableChoices();

                switch (dm.GrabInput(3))
                {
                    case ChoiceSystem.Choices.Zero:
                        dm.DisplayText("This is my home! These are my friends! Press space to close");
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText("Great! Its nice to meet you. Press space to close");
                        break;
                    case ChoiceSystem.Choices.Two:
                        dm.DisplayText("You’re gonna have to find your own way out.  Hopefully you find more comfort in here in the future. Press space to close");
                        break;
                }

                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));
                break;
            case 4:
                dm.DisplayText("…");
                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));

                dm.DisableTextBox();
                dm.DisplayChoices(3, new string[] { "Press A for: Hey there", "Press D for: uhhhhhh, hello?", "Press S for: are you going to say anything?" });

                yield return new WaitWhile(() => Input.anyKey == true);
                yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

                dm.DisableChoices();

                switch (dm.GrabInput(3))
                {
                    case ChoiceSystem.Choices.Zero:
                        dm.DisplayText("H… hello. Press space to close");
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText(".... Press space to close");
                        break;
                    case ChoiceSystem.Choices.Two:
                        dm.DisplayText(":( Press space to close");
                        break;
                }

                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));
                break;
            case 5:
                dm.DisplayText("Hey could you ask what that person in the bottom right corner is doing here?");
                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));

                dm.DisableTextBox();
                dm.DisplayChoices(3, new string[] { "Press A for: Yeah sure, but who are you?", "Press D for: Tell me who you are first?", "Press S for: Don't tell me what to do!" });

                yield return new WaitWhile(() => Input.anyKey == true);
                yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

                dm.DisableChoices();

                switch (dm.GrabInput(3))
                {
                    case ChoiceSystem.Choices.Zero:
                        dm.DisplayText("I got lost here after running away after an argument with them.  Not that I care about them or anything.");
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText("It doesn’t matter could you just do it?");
                        break;
                    case ChoiceSystem.Choices.Two:
                        dm.DisplayText("Well then don’t talk to me either.");
                        break;
                }

                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));
                break;
            case 6:
                dm.DisplayText("What do you want?");
                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));

                dm.DisableTextBox();
                dm.DisplayChoices(3, new string[] { "Press A for: (Name of C5) wants to talk to you.", "Press D for: What are you doing here?", "Press S for: Why are you in such a bad mood?" });

                yield return new WaitWhile(() => Input.anyKey == true);
                yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

                dm.DisableChoices();

                switch (dm.GrabInput(3))
                {
                    case ChoiceSystem.Choices.Zero:
                        dm.DisplayText("Well, I don’t want to talk to them, tell them that.");
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText("I’m… Just here.");
                        break;
                    case ChoiceSystem.Choices.Two:
                        dm.DisplayText("I’m not in a bad mood!");
                        break;
                }

                yield return new WaitWhile(() => Input.anyKeyDown == false);
                yield return new WaitWhile(() => !Input.GetKeyDown("space"));
                break;
            default:
                Debug.Log("Something is broken lol.  This Number of player does not exist");
                break;
        }
        dm.DisableTextBox();
        player.canMove = true;
        yield return null;
    }
}
