using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{

	//format for strings (Char name)_(World#)_(P if Player response or C if character response, O if opening)_(0,1,2 from last world)_(0, 1, 2 describes how selfless/nice the respone is with 2 being the highest)
	#region Leon (Character 1)
	//World 1
	public static string Leon_default = "Isn't this place so interesting?";

	public static string Leon_1_O_0_0 = "Hey! A newcomer! How are you doing?";

	public static string Leon_1_P_0_2 = "I’m doing great! How are you doing?";
	public static string Leon_1_P_0_1 = "Where am I?";
	public static string Leon_1_P_0_0 = "Why do you look so strange?";

	public static string Leon_1_C_0_2 = "I’m always doing great! I enjoy the puzzles!";
	public static string Leon_1_C_0_1 = "No one knows what this place is, it just is!";
	public static string Leon_1_C_0_0 = "HAHA! That’s just the way I look kiddo.";
	//World 2
	public static string Leon_2_O_2_1 = "Hey that last set of puzzles was really fun wasn’t it!";

	public static string Leon_2_P_2_2 = "Yeah they were great, do you have any ideas whats coming up next?";
	public static string Leon_2_P_2_1 = "You seem pretty smart what’s going on around here.";
	public static string Leon_2_P_2_0 = "Are you kidding me those puzzles were horrible!";

	public static string Leon_2_C_2_2 = "I’m always doing great! I enjoy the puzzles!";
	public static string Leon_2_C_2_1 = "Everyone here has been split! You probably saw a mirror on the way here, that thing is the culprit.  I’ve been searching on a way to recombine since I got here";
	public static string Leon_2_C_2_0 = "I guess you just don’t see the puzzles the same way as me then";
	#endregion

	#region Hazel (Character 2)
	public static string Hazel_default = "Got nothing better to do than talk to me, huh?";

	public static string Hazel_1_O_0_0 = "Hey you!";

	public static string Hazel_1_P_0_2 = "Hey, I’m Pero.  I got lost in here looking for a friend. Who are you?";
	public static string Hazel_1_P_0_1 = "Hello.";
	public static string Hazel_1_P_0_0 = "Don’t yell at me like that!";

	public static string Hazel_1_C_0_2 = "Hey! I’m Hazel.  Looks like we are in a similar situation, I also got lost looking for a friend.  What a coincidence!";
	public static string Hazel_1_C_0_1 = "Don’t be shy! I’m Hazel.";
	public static string Hazel_1_C_0_0 = "I’ll do whatever I want!";
	#endregion
}
