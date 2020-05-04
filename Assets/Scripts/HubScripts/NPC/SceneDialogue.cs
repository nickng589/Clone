using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{

	//format for strings (Char name)_(World#)_(P if Player response or C if character response, O if opening)_(0, 1, 2 describes how selfless/nice the respone is with 2 being the highest)
	#region Leon (Character 1)
	//World 1
	public static string Leon_1_O_0 = "Hey! A newcomer! How are you doing?";

	public static string Leon_1_P_2 = "I’m doing great! How are you doing?";
	public static string Leon_1_P_1 = "Where am I?";
	public static string Leon_1_P_0 = "Why do you look so strange?";

	public static string Leon_1_C_2 = "I’m always doing great! I enjoy the puzzles!";
	public static string Leon_1_C_1 = "No one knows what this place is, it just is!";
	public static string Leon_1_C_0 = "HAHA! That’s just the way I look kiddo.";
	#endregion

	#region Hazel (Character 2)
	public static string Hazel_1_O_0 = "Hey you!";

	public static string Hazel_1_P_2 = "Hey, I’m Pero.  I got lost in here looking for a friend. Who are you?";
	public static string Hazel_1_P_1 = "Hello.";
	public static string Hazel_1_P_0 = "Don’t yell at me like that!";

	public static string Hazel_1_C_2 = "Hey! I’m Hazel.  Looks like we are in a similar situation, I also got lost looking for a friend.  What a coincidence!";
	public static string Hazel_1_C_1 = "Don’t be shy! I’m Hazel.";
	public static string Hazel_1_C_0 = "I’ll do whatever I want!";
	#endregion
}
