﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior3 : MonoBehaviour
{
    #region Editor Variables

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

    public bool talked;

    void Start()
    {
        talked = false;
        canMove = true;
        walkTimer = 1;
        if (!PlayerPrefs.HasKey("morality"))
        {
            PlayerPrefs.SetInt("morality", 0);
        }
        if (!PlayerPrefs.HasKey("Leon"))
        {
            PlayerPrefs.SetInt("Leon", 0);
        }
        if (!PlayerPrefs.HasKey("Hazel"))
        {
            PlayerPrefs.SetInt("Hazel", 0);
        }
        if (!PlayerPrefs.HasKey("Tim"))
        {
            PlayerPrefs.SetInt("Tim", 0);
        }
        PlayerPrefs.SetInt("Romy", 0);
        PlayerPrefs.SetInt("NA", 0);
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
        string opening = "ERROR";
        string response1 = "ERROR";
        string response2 = "ERROR";
        string response3 = "ERROR";
        string character1 = "ERROR";
        string character2 = "ERROR";
        string character3 = "ERROR";
        string char_morality = "ERROR";
        int relationship = 0;
        switch (player_num) 
        {
            case 1:
                if (!talked)
                {
                    relationship = PlayerPrefs.GetInt("Leon");
                    Debug.Log(relationship);
                    if (relationship >= 1)
                    {
                        opening = SceneDialogue.Leon_3_O_2;
                        response3 = SceneDialogue.Leon_3_P_2_0;
                        response2 = SceneDialogue.Leon_3_P_2_1;
                        response1 = SceneDialogue.Leon_3_P_2_2;
                        character3 = SceneDialogue.Leon_3_C_2_0;
                        character2 = SceneDialogue.Leon_3_C_2_1;
                        character1 = SceneDialogue.Leon_3_C_2_2;
                    }
                    else if (relationship <= 1)
                    {
                        opening = SceneDialogue.Leon_3_O_0;
                        response3 = SceneDialogue.Leon_3_P_0_0;
                        response2 = SceneDialogue.Leon_3_P_0_1;
                        response1 = SceneDialogue.Leon_3_P_0_2;
                        character3 = SceneDialogue.Leon_3_C_0_0;
                        character2 = SceneDialogue.Leon_3_C_0_1;
                        character1 = SceneDialogue.Leon_3_C_0_2;
                    } else
                    {
                        opening = SceneDialogue.Leon_3_O_1;
                        response3 = SceneDialogue.Leon_3_P_1_0;
                        response2 = SceneDialogue.Leon_3_P_1_1;
                        response1 = SceneDialogue.Leon_3_P_1_2;
                        character3 = SceneDialogue.Leon_3_C_1_0;
                        character2 = SceneDialogue.Leon_3_C_1_1;
                        character1 = SceneDialogue.Leon_3_C_1_2;
                    }
                    char_morality = "Leon";
                } else
                {
                    opening = SceneDialogue.Leon_default;
                }
                break;
            case 2:
                if (!talked)
                {
                    relationship = PlayerPrefs.GetInt("Hazel");
                    Debug.Log(relationship);
                    if (relationship >= 1)
                    {
                        opening = SceneDialogue.Hazel_2_O_2;
                        response3 = SceneDialogue.Hazel_2_P_2_0;
                        response2 = SceneDialogue.Hazel_2_P_2_1;
                        response1 = SceneDialogue.Hazel_2_P_2_2;
                        character3 = SceneDialogue.Hazel_2_C_2_0;
                        character2 = SceneDialogue.Hazel_2_C_2_1;
                        character1 = SceneDialogue.Hazel_2_C_2_2;
                    }
                    else if (relationship <= 1)
                    {
                        opening = SceneDialogue.Hazel_2_O_0;
                        response3 = SceneDialogue.Hazel_2_P_0_0;
                        response2 = SceneDialogue.Hazel_2_P_0_1;
                        response1 = SceneDialogue.Hazel_2_P_0_2;
                        character3 = SceneDialogue.Hazel_2_C_0_0;
                        character2 = SceneDialogue.Hazel_2_C_0_1;
                        character1 = SceneDialogue.Hazel_2_C_0_2;
                    }
                    else
                    {
                        opening = SceneDialogue.Hazel_2_O_1;
                        response3 = SceneDialogue.Hazel_2_P_1_0;
                        response2 = SceneDialogue.Hazel_2_P_1_1;
                        response1 = SceneDialogue.Hazel_2_P_1_2;
                        character3 = SceneDialogue.Hazel_2_C_1_0;
                        character2 = SceneDialogue.Hazel_2_C_1_1;
                        character1 = SceneDialogue.Hazel_2_C_1_2;
                    }
                    char_morality = "Hazel";
                }
                else
                {
                    opening = SceneDialogue.Hazel_default;
                }
                break;
            case 3:
                if (!talked)
                {
                    relationship = PlayerPrefs.GetInt("Tim");
                    Debug.Log(relationship);
                    if (relationship >= 1)
                    {
                        opening = SceneDialogue.Tim_3_O_2;
                        response3 = SceneDialogue.Tim_3_P_2_0;
                        response2 = SceneDialogue.Tim_3_P_2_1;
                        response1 = SceneDialogue.Tim_3_P_2_2;
                        character3 = SceneDialogue.Tim_3_C_2_0;
                        character2 = SceneDialogue.Tim_3_C_2_1;
                        character1 = SceneDialogue.Tim_3_C_2_2;
                    }
                    else if (relationship <= 1)
                    {
                        opening = SceneDialogue.Tim_3_O_0;
                        response3 = SceneDialogue.Tim_3_P_0_0;
                        response2 = SceneDialogue.Tim_3_P_0_1;
                        response1 = SceneDialogue.Tim_3_P_0_2;
                        character3 = SceneDialogue.Tim_3_C_0_0;
                        character2 = SceneDialogue.Tim_3_C_0_1;
                        character1 = SceneDialogue.Tim_3_C_0_2;
                    }
                    else
                    {
                        opening = SceneDialogue.Tim_3_O_1;
                        response3 = SceneDialogue.Tim_3_P_1_0;
                        response2 = SceneDialogue.Tim_3_P_1_1;
                        response1 = SceneDialogue.Tim_3_P_1_2;
                        character3 = SceneDialogue.Tim_3_C_1_0;
                        character2 = SceneDialogue.Tim_3_C_1_1;
                        character1 = SceneDialogue.Tim_3_C_1_2;
                    }
                }
                else
                {
                    opening = SceneDialogue.Tim_default;
                }
                break;
            case 4:
                if (!talked)
                {
                    opening = SceneDialogue.Romy_1_O_0;
                    response3 = SceneDialogue.Romy_1_P_0_0;
                    response2 = SceneDialogue.Romy_1_P_0_1;
                    response1 = SceneDialogue.Romy_1_P_0_2;
                    character3 = SceneDialogue.Romy_1_C_0_0;
                    character2 = SceneDialogue.Romy_1_C_0_1;
                    character1 = SceneDialogue.Romy_1_C_0_2;
                    char_morality = "Romy";
                }
                else
                {
                    opening = SceneDialogue.Romy_default;
                }
                break;
            case 5:
                if (!talked)
                {
                    opening = SceneDialogue.Norman_1_O_0;
                    response3 = SceneDialogue.Norman_1_P_0_0;
                    response2 = SceneDialogue.Norman_1_P_0_1;
                    response1 = SceneDialogue.Norman_1_P_0_2;
                    character3 = SceneDialogue.Norman_1_C_0_0;
                    character2 = SceneDialogue.Norman_1_C_0_1;
                    character1 = SceneDialogue.Norman_1_C_0_2;
                    char_morality = "NA";
                }
                else
                {
                    opening = SceneDialogue.Norman_default;
                }
                break;
            default:
                Debug.Log("Something is broken lol.  This Number of player does not exist");
                break;
        }
        dm.DisplayText(opening);
        yield return new WaitWhile(() => Input.anyKeyDown == false);
        yield return new WaitWhile(() => !Input.GetKeyDown("space"));
        dm.DisableTextBox();
        if (!talked)
        {
            dm.DisplayChoices(3, new string[] { response1, response2, response3 });

            yield return new WaitWhile(() => Input.anyKey == true);
            yield return new WaitWhile(() => dm.GrabInput(3) == ChoiceSystem.Choices.Invalid);

            dm.DisableChoices();

            switch (dm.GrabInput(3))
            {
                case ChoiceSystem.Choices.Zero:
                    PlayerPrefs.SetInt("morality", PlayerPrefs.GetInt("morality") + 1);
                    dm.DisplayText(character1);
                    PlayerPrefs.SetInt(char_morality, PlayerPrefs.GetInt(char_morality) + 1);
                    break;
                case ChoiceSystem.Choices.One:
                    dm.DisplayText(character2);
                    break;
                case ChoiceSystem.Choices.Two:
                    PlayerPrefs.SetInt("morality", PlayerPrefs.GetInt("morality") - 1);
                    dm.DisplayText(character3);
                    PlayerPrefs.SetInt(char_morality, PlayerPrefs.GetInt(char_morality) - 1);
                    break;
            }
            yield return new WaitWhile(() => Input.anyKeyDown == false);
            yield return new WaitWhile(() => !Input.GetKeyDown("space"));
            dm.DisableTextBox();
            talked = true;
        }
        player.canMove = true;
        GetComponentInParent<NPCMovement>().canMove = true;
        yield return null;
    }
}
