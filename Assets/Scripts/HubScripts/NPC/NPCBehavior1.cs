using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior1 : MonoBehaviour
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
        if (!PlayerPrefs.HasKey("morality"))
        {
            PlayerPrefs.SetInt("morality", 0);
        }
        PlayerPrefs.SetInt("Leon", 0);
        PlayerPrefs.SetInt("Romy", 0);
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
                dm.DisplayText(SceneDialogue.World1_opening);
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
                        PlayerPrefs.SetInt("morality", PlayerPrefs.GetInt("morality") + 1);
                        dm.DisplayText("I’m always doing great! I enjoy the puzzles! Press space to close");
                        PlayerPrefs.SetInt("Leon", PlayerPrefs.GetInt("Leon") + 1);
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText("No one knows what this place is, it just is! Press space to close");
                        break;
                    case ChoiceSystem.Choices.Two:
                        PlayerPrefs.SetInt("morality", PlayerPrefs.GetInt("morality") - 1);
                        dm.DisplayText(" HAHA! That’s just the way I look kiddo. Press space to close");
                        PlayerPrefs.SetInt("Leon", PlayerPrefs.GetInt("Leon") - 1);
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
                        PlayerPrefs.SetInt("Hazel", PlayerPrefs.GetInt("Romy") + 1);
                        break;
                    case ChoiceSystem.Choices.One:
                        dm.DisplayText("Great! Its nice to meet you. Press space to close");
                        break;
                    case ChoiceSystem.Choices.Two:
                        dm.DisplayText("You’re gonna have to find your own way out.  Hopefully you find more comfort in here in the future. Press space to close");
                        PlayerPrefs.SetInt("Hazel", PlayerPrefs.GetInt("Romy") - 1);
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
        GetComponentInParent<NPCMovement>().canMove = true;
        yield return null;
    }
}
