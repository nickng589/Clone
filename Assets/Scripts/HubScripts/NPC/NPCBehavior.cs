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
        dm.DisplayText(SceneDialogue.test);
        yield return new WaitWhile(() => Input.anyKeyDown == false);
        yield return new WaitWhile(() => !Input.GetKeyDown("space"));

        dm.DisableTextBox();
        dm.DisplayChoices(3, new string[] { "Press A for choice 1", "Press D for choice 2", "Press S for choice 3" });

        yield return new WaitWhile(() => Input.anyKey == true);
        yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

        dm.DisableChoices();

        switch (dm.GrabInput(3))
        {
            case ChoiceSystem.Choices.Zero:
                dm.DisplayText("You chose choice 1! Press space to close");
                break;
            case ChoiceSystem.Choices.One:
                dm.DisplayText("You chose choice 2! Press space to close");
                break;
            case ChoiceSystem.Choices.Two:
                dm.DisplayText("You chose choice 3! Press space to close");
                break;
        }

        yield return new WaitWhile(() => Input.anyKeyDown == false);
        yield return new WaitWhile(() => !Input.GetKeyDown("space"));

        dm.DisableTextBox();
        player.canMove = true;
        yield return null;
    }
}
