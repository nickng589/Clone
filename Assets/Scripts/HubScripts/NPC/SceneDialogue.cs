using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDialogue : MonoBehaviour
{

	//format for strings (Char name)_(World#)_(P if Player response or C if character response, O if opening)_(0,1,2 from last world)_(0, 1, 2 describes how selfless/nice the respone is with 2 being the highest)
	#region Leon (Character 1)
	//World 1
	public static string Leon_default = "Isn't this place so interesting?";

	public static string Leon_1_O_0 = "Hey! A newcomer! How are you doing?";

	public static string Leon_1_P_0_2 = "I’m doing great! How are you doing?";
	public static string Leon_1_P_0_1 = "Where am I?";
	public static string Leon_1_P_0_0 = "Why do you look so strange?";

	public static string Leon_1_C_0_2 = "I’m always doing great! I enjoy the puzzles!";
	public static string Leon_1_C_0_1 = "No one knows what this place is, it just is!";
	public static string Leon_1_C_0_0 = "HAHA! That’s just the way I look kiddo.";

	//World 2
	//good
	public static string Leon_2_O_2 = "Hey, that last set of puzzles was really fun, wasn’t it!";

	public static string Leon_2_P_2_2 = "Yeah they were great, do you have any ideas on whats coming up next?";
	public static string Leon_2_P_2_1 = "You seem pretty smart, what’s going on around here?";
	public static string Leon_2_P_2_0 = "Are you kidding me? Those puzzles were horrible!";

	public static string Leon_2_C_2_2 = "I think the upcoming puzzles have something to do with binary. May want to look into it.";
	public static string Leon_2_C_2_1 = "Everyone here has been split! You probably saw a mirror on the way here, that thing is the culprit.  I’ve been searching on a way to recombine since I got here.";
	public static string Leon_2_C_2_0 = "I guess you just don’t see the puzzles the same way as me then.";
	//neutral
	public static string Leon_2_O_1 = "Hey, it’s you again!";

	public static string Leon_2_P_1_2 = "You seem pretty smart, what’s going on around here?";
	public static string Leon_2_P_1_1 = "Yup it’s me again. My name is Pero.";
	public static string Leon_2_P_1_0 = "Uhhhhh, could you stop talking to me?";

	public static string Leon_2_C_1_2 = "Everyone here has been split! You probably saw a mirror on the way here, that thing is the culprit.  I’ve been searching on a way to recombine since I got here.";
	public static string Leon_2_C_1_1 = "Looks like you have been dealing with the puzzles well so far! You haven’t gone crazy yet! Good for you.";
	public static string Leon_2_C_1_0 = "Hey hey hey, don’t forget you were the one who walked up to me.";

	//bad
	public static string Leon_2_O_0 = "Hey there.";

	public static string Leon_2_P_0_2 = "Sorry about talking about your looks last time, it just came out";
	public static string Leon_2_P_0_1 = "Hey, it’s the crazy looking guy again.";
	public static string Leon_2_P_0_0 = "Uhhhhh, could you stop talking to me?";

	public static string Leon_2_C_0_2 = "No problem, that kind of stuff happens to me all the time too.  Hope you are enjoying the puzzles!";
	public static string Leon_2_C_0_1 = "HAHA, yup it is the crazy looking guy again!";
	public static string Leon_2_C_0_0 = "Hey hey hey, don’t forget you were the one who walked up to me.";

	//World 3
	//good
	public static string Leon_3_O_2 = "Hey I’m trying to collect some data about the forest, could you tell me a little about the puzzles you just completed?  In exchange I’ll tell you a little about what I know! ";

	public static string Leon_3_P_2_2 = "Good thing I took CS61C at UC Berkeley because knowing binary really helped me with the last puzzle";
	public static string Leon_3_P_2_1 = "Honestly, I forgot the last set of puzzles.  But so far it seems like I am alternating between this forest and some place where there is a split version of me";
	public static string Leon_3_P_2_0 = "How about you play the puzzles yourself?";

	public static string Leon_3_C_2_2 = "Ohhhhh, very interesting. I hear that it's the number one public university? Anyways, here's a bit of my knowledge: there is definitely an exit to the forest, but only past all these puzzles.";
	public static string Leon_3_C_2_1 = "Yep, I'm glad you've noticed it! I think I've almost discovered a way to recombine people who have been split - give me a bit longer to work on it.";
	public static string Leon_3_C_2_0 = "I guess I will then.";
	//neutral
	public static string Leon_3_O_1 = "Hey it’s the new kid!  Learn anything new?";

	public static string Leon_3_P_1_2 = "It seems like I am alternating between this forest and some place where there is a split version of me.  Do you know anything about whats going on?";
	public static string Leon_3_P_1_1 = "Uh, not much.  I guess the puzzles are getting gradually harder?";
	public static string Leon_3_P_1_0 = "No";

	public static string Leon_3_C_1_2 = "Mhmmmmmmmm, that seems to be what’s going on for everyone here.  I think we were all split across dimensions when entering the forest and need to make it to the center to get recombined.";
	public static string Leon_3_C_1_1 = "Ah, that does align with the rest of my data.  You could probably expect that trend going forward";
	public static string Leon_3_C_1_0 = "That’s unfortunate, maybe you should pay closer attention.";

	//bad
	public static string Leon_3_O_0 = "It’s the kid again...";

	public static string Leon_3_P_0_2 = "What are those notes there for?  Looks interesting.";
	public static string Leon_3_P_0_1 = "Yup it’s me, the kid.";
	public static string Leon_3_P_0_0 = "What’s with the tone, old man?";

	public static string Leon_3_C_0_2 = "Oh, these notes?  I’m glad you asked!  I’ve been trying to figure out what’s happening in this weird place for a while.";
	public static string Leon_3_C_0_1 = "Yup it is you… the kid.";
	public static string Leon_3_C_0_0 = "What’s with the rudeness, young man?";

	//World 4
	//good
	public static string Leon_4_O_2 = "Hey kid I’ve just collected some new data! Wanna take a look? ";

	public static string Leon_4_P_2_2 = "Of course!";
	public static string Leon_4_P_2_1 = "I’m in a bit of a rush but I guess I have time.";
	public static string Leon_4_P_2_0 = "Nah, not really.";

	public static string Leon_4_C_2_2 = "So i’ve been studying the insects who have entered this forest.  Outside of the forest have a lifespan of one year.  But tehir lifespan is halved after entering the forest.  This leads me to believe that all living things that enter this forest have their lifespan halved… Including you and me.";
	public static string Leon_4_C_2_1 = "To make it short, based on data I have collected from insects who have entered the forest, the lifespan of living things are halved when entering the forest.";
	public static string Leon_4_C_2_0 = "Alright, maybe next time then.";
	//neutral
	public static string Leon_4_O_1 = "I’ve been collecting some data, but it isn’t quite complete.  Wanna listen and maybe you could help me out?";

	public static string Leon_4_P_1_2 = "Yeah sure, let me take a look.";
	public static string Leon_4_P_1_1 = "I guess I could take a quick look, I’m in a bit of a rush though.";
	public static string Leon_4_P_1_0 = "No";

	public static string Leon_4_C_1_2 = "So I’ve observed a few fruit flies who have lived 15, 19, 21, 18, and 22 days long.  From what I remember fruit flies have an average lifespan of 40 days.  I think this means something but I’m not sure what...";
	public static string Leon_4_C_1_1 = "I’ll make it quick then.  So I’ve observed a few fruit flies who have lived 15, 19, 21, 18, and 22 days long. I think this means something but I’m not sure what...";
	public static string Leon_4_C_1_0 = "Alright I’ll keep looking myself then.";

	//bad
	public static string Leon_4_O_0 = "Sorry kiddo I’m busy right now.";

	public static string Leon_4_P_0_2 = "Busy with what?  I’m interested.";
	public static string Leon_4_P_0_1 = "Uh alright.";
	public static string Leon_4_P_0_0 = "Oh yeah? Me too.";

	public static string Leon_4_C_0_2 = "Oh really?  Great! Yeah I’ve been tinkering around with insects for a while.  It seems like there is a connection between their lifespan and being split but I’m not sure what..";
	public static string Leon_4_C_0_1 = "I’ll just get back to work then.";
	public static string Leon_4_C_0_0 = "Hey, you're the one who walked up to me.";
	#endregion

	#region Hazel (Character 2)
	//World 1
	public static string Hazel_default = "Got nothing better to do than talk to me, huh?";

	public static string Hazel_1_O_0 = "Hey, you!";

	public static string Hazel_1_P_0_2 = "Hey, I’m Pero.  I got lost in here looking for a friend. Who are you?";
	public static string Hazel_1_P_0_1 = "Hello.";
	public static string Hazel_1_P_0_0 = "Don’t yell at me like that!";

	public static string Hazel_1_C_0_2 = "Hey! I’m Hazel.  Looks like we are in a similar situation, I also got lost looking for a friend.  What a coincidence!";
	public static string Hazel_1_C_0_1 = "Don’t be shy! I’m Hazel. I also got lost looking for a friend. If you need anything, let me know.";
	public static string Hazel_1_C_0_0 = "I’ll do whatever I want!";

	//World 2
	//good
	public static string Hazel_2_O_2 = "Well, if it isn’t my best friend Pero.";

	public static string Hazel_2_P_2_2 = "Haha, if it isn’t my best bud Hazel";
	public static string Hazel_2_P_2_1 = "Best friend? We just met.";
	public static string Hazel_2_P_2_0 = "I’m not even close to being your best friend.";

	public static string Hazel_2_C_2_2 = "Glad someone my age finally is around, no one else here can really relate to me.";
	public static string Hazel_2_C_2_1 = "Well, how many times do I have to meet you before I can call you that? If you haven't noticed, we're trapped here for a while. Might as well be friends, right?";
	public static string Hazel_2_C_2_0 = "Is that right?  Forget I said anything then.";
	//neutral
	public static string Hazel_2_O_1 = "I still never got your name, I gave you mine so it’s only fair.";

	public static string Hazel_2_P_1_2 = "Oh it’s Pero, should have introduced myself sooner.";
	public static string Hazel_2_P_1_1 = "Its Pero.";
	public static string Hazel_2_P_1_0 = "You never asked.";

	public static string Hazel_2_C_1_2 = "No problem, nice to meet you Pero!";
	public static string Hazel_2_C_1_1 = "Pretty straightforward guy, huh?";
	public static string Hazel_2_C_1_0 = "I thought introducing yourself was common manners? Is it not?";

	//bad
	public static string Hazel_2_O_0 = "If it isn’t the rude brat from before.";

	public static string Hazel_2_P_0_2 = "I think we got off on the wrong foot. My name is Pero, nice to meet you.";
	public static string Hazel_2_P_0_1 = "Yup it’s me, the rude guy.";
	public static string Hazel_2_P_0_0 = "Well, if it isn’t the ruder brat from before.";

	public static string Hazel_2_C_0_2 = "No problem, nice to meet you Pero!";
	public static string Hazel_2_C_0_1 = "HAHA, You’re a pretty funny guy.";
	public static string Hazel_2_C_0_0 = "Haha, you think you are a pretty funny guy.  huh?";

	//World 3
	//good
	public static string Hazel_3_O_2 = "How has it been going finding your friend?  For me it’s been a bit… rough.";

	public static string Hazel_3_P_2_2 = "Yeah me too, still no clue where they are.  All I can do is head further into this forest";
	public static string Hazel_3_P_2_1 = "Not sure what to tell you, I have no idea if I’m even going in the right direction.";
	public static string Hazel_3_P_2_0 = "How about we both just focus on finding our own friend, don’t worry about me.";

	public static string Hazel_3_C_2_2 = "Yeah I hope we can both make some progress toward finding our friends.  But at the same time I can’t help but start to feel hopeless.";
	public static string Hazel_3_C_2_1 = "You and me both.  For all we know we might be getting further and further from our friends.";
	public static string Hazel_3_C_2_0 = "Hey, don’t get snappy with me, I just wanted to talk.";
	//neutral
	public static string Hazel_3_O_1 = "How’s your search for your friend going?";

	public static string Hazel_3_P_1_2 = "Still searching.  All I can do is go deeper. how about you?";
	public static string Hazel_3_P_1_1 = "Not sure if I am getting any further or any closer to be honest to you..";
	public static string Hazel_3_P_1_0 = "It’s been brutal.  I feel like I’m getting further and further as I go deeper and deeper.";

	public static string Hazel_3_C_1_2 = "Yeah, I’m still looking too.  Starting to feel a little hopeless, though.";
	public static string Hazel_3_C_1_1 = "I feel you, I can’t tell if I’m making any progress or not.";
	public static string Hazel_3_C_1_0 = "Yeah It’s starting to look worse and worse the longer I look. At this point...I just want to get out of here.";

	//bad
	public static string Hazel_3_O_0 = "Still around here, huh?";

	public static string Hazel_3_P_0_2 = "Yup still around.  What are you doing here?";
	public static string Hazel_3_P_0_1 = "Yup, nowhere else to go.";
	public static string Hazel_3_P_0_0 = "Welll... Yeah, nowhere else to go? Thats a dumb question.";

	public static string Hazel_3_C_0_2 = "Been searching for my friend.";
	public static string Hazel_3_C_0_1 = "You and me both.";
	public static string Hazel_3_C_0_0 = "Smart guy, eh?";
	#endregion

	#region Tim (Character 3)
	//World 1
	public static string Tim_default = "Uh... Hello";

	public static string Tim_1_O_0 = "…";

	public static string Tim_1_P_0_2 = "Hey there.";
	public static string Tim_1_P_0_1 = "uhhhhhh, hello?";
	public static string Tim_1_P_0_0 = "Are you going to say anything?";

	public static string Tim_1_C_0_2 = "H… hello";
	public static string Tim_1_C_0_1 = "…";
	public static string Tim_1_C_0_0 = "…";

	//World 2
	//good
	public static string Tim_2_O_2 = "Hello...";

	public static string Tim_2_P_2_2 = "Hey, are you doing alright?  Are you lost?";
	public static string Tim_2_P_2_1 = "Hello… Again?";
	public static string Tim_2_P_2_0 = "Do you not know how to talk?  Hello?";

	public static string Tim_2_C_2_2 = "*sniffles* yeah, I got lost here. I’m doing ok";
	public static string Tim_2_C_2_1 = "Hey...";
	public static string Tim_2_C_2_0 = "…";
	//neutral
	public static string Tim_2_O_1 = "Hello...";

	public static string Tim_2_P_1_2 = "Hey, are you doing alright?  Are you lost?";
	public static string Tim_2_P_1_1 = "Hello… Again?";
	public static string Tim_2_P_1_0 = " Do you not know how to talk?  Hello?";

	public static string Tim_2_C_1_2 = "I'm... ok";
	public static string Tim_2_C_1_1 = "Uh...";
	public static string Tim_2_C_1_0 = "...";
	//bad
	public static string Tim_2_O_0 = "...";

	public static string Tim_2_P_0_2 = "I think we got off to a bad start last time, how are you doing?";
	public static string Tim_2_P_0_1 = "...";
	public static string Tim_2_P_0_0 = "Still not talking huh?";

	public static string Tim_2_C_0_2 = "Not… Bad...";
	public static string Tim_2_C_0_1 = "...";
	public static string Tim_2_C_0_0 = "...";

	//World 3
	//good
	public static string Tim_3_O_2 = "Hey ... could I tell you something?";

	public static string Tim_3_P_2_2 = "Of course, whats on your mind?";
	public static string Tim_3_P_2_1 = "Yeah, but could you make it quick?";
	public static string Tim_3_P_2_0 = "Not really in the mood right now, keep it to yourself.";

	public static string Tim_3_C_2_2 = "I got lost here after running away from home.  It’s scary here, but it’s a lot scarier at home.";
	public static string Tim_3_C_2_1 = "This place is pretty scary.";
	public static string Tim_3_C_2_0 = "…";
	//neutral
	public static string Tim_3_O_1 = "It’s you… again...";

	public static string Tim_3_P_1_2 = "Yup, it’s me.  How are you doing?";
	public static string Tim_3_P_1_1 = "It’s me again.";
	public static string Tim_3_P_1_0 = "Who else could it be?";

	public static string Tim_3_C_1_2 = "I’m ok, just a bit scared.";
	public static string Tim_3_C_1_1 = "Hi.";
	public static string Tim_3_C_1_0 = "...";
	//bad
	public static string Tim_3_O_0 = "...";

	public static string Tim_3_P_0_2 = "You alright?";
	public static string Tim_3_P_0_1 = "...";
	public static string Tim_3_P_0_0 = "Are you a mute?";

	public static string Tim_3_C_0_2 = "I’m fine...";
	public static string Tim_3_C_0_1 = "...";
	public static string Tim_3_C_0_0 = "...";

	//World 4
	//good
	public static string Tim_4_O_2 = "Could I tell you one more thing?";

	public static string Tim_4_P_2_2 = "I’d love to listen to anything you have to say.";
	public static string Tim_4_P_2_1 = "A bit busy but if you make it quick I can listen.";
	public static string Tim_4_P_2_0 = "Nope!";

	public static string Tim_4_C_2_2 = "My parents at home, they weren’t nice to me.  I don’t want to ever go back… Maybe I’ll stay here";
	public static string Tim_4_C_2_1 = "I … don’t want to go back home.  Maybe I can just stay here.";
	public static string Tim_4_C_2_0 = "…";
	//neutral
	public static string Tim_4_O_1 = "Hello.";

	public static string Tim_4_P_1_2 = "Hey.  How’s it going?";
	public static string Tim_4_P_1_1 = "Hey.";
	public static string Tim_4_P_1_0 = "Do you know how to say anything else besides greetings?";

	public static string Tim_4_C_1_2 = "This forest is pretty nice.  The people here are better than at home.";
	public static string Tim_4_C_1_1 = "Hi.";
	public static string Tim_4_C_1_0 = "...";
	//bad
	public static string Tim_4_O_0 = "...";

	public static string Tim_4_P_0_2 = "How’s it going kid?";
	public static string Tim_4_P_0_1 = "...";
	public static string Tim_4_P_0_0 = "Hello? Is there a brain in that head?";

	public static string Tim_4_C_0_2 = "Good...";
	public static string Tim_4_C_0_1 = "...";
	public static string Tim_4_C_0_0 = "...";
	#endregion

	#region Romy (Character 4)
	//World 1
	public static string Romy_default = "I love this forest!";

	public static string Romy_1_O_0 = "Hey how has everyone been treating you?";

	public static string Romy_1_P_0_2 = "Great! Who are you?";
	public static string Romy_1_P_0_1 = "Not too bad.";
	public static string Romy_1_P_0_0 = "Terrible, get me out of here.";

	public static string Romy_1_C_0_2 = "I'm Romy! I live here. Welcome to the forest! It's a weird place, as I'm sure you've seen.";
	public static string Romy_1_C_0_1 = "Great! It’s nice to meet you.";
	public static string Romy_1_C_0_0 = "You’re gonna have to find your own way out.  Hopefully you find more comfort in here in the future";

	//World 2
	//good
	public static string Romy_2_O_2 = "Hi friend, have you gotten a grasp for what’s going on yet?";

	public static string Romy_2_P_2_2 = "Uhh. I’m still a little confused to be quite honest.";
	public static string Romy_2_P_2_1 = "I guess so";
	public static string Romy_2_P_2_0 = "Ez pz";

	public static string Romy_2_C_2_2 = "Don’t worry, I can help. I’ll try and keep an eye out for Robyn for you";
	public static string Romy_2_C_2_1 = "Great, let me know if you need anything";
	public static string Romy_2_C_2_0 = "lol";
	//neutral
	public static string Romy_2_O_1 = "Do you need some help?";

	public static string Romy_2_P_1_2 = "That would be great! Where do I need to go?";
	public static string Romy_2_P_1_1 = "No thank you.";
	public static string Romy_2_P_1_0 = "I got this on my own.";

	public static string Romy_2_C_1_2 = "Don’t give up! Just take your time and think things through. Head straight for that exit. ";
	public static string Romy_2_C_1_1 = "You’ll be fineeeeee, you got it.";
	public static string Romy_2_C_1_0 = "Jk, good luck :)";
	//bad
	public static string Romy_2_O_0 = "Hey hey hey";

	public static string Romy_2_P_0_2 = "How are you doing?";
	public static string Romy_2_P_0_1 = "Hey";
	public static string Romy_2_P_0_0 = "Go away big raccoon thing.";

	public static string Romy_2_C_0_2 = "Great! Hope you are doing good as well";
	public static string Romy_2_C_0_1 = "Ha ha, hope you are enjoying the forest";
	public static string Romy_2_C_0_0 = "Alright.";
	#endregion

	#region Norman/Addison (Character 5)
	//World 1
	public static string Norman_default = "Oh man I made a mistake.";
	public static string Addison_default = "I miss him.";

	public static string Norman_1_O_0 = "Hey I got into a fight with my wife, Addison.  If you see her could you tell her something?";

	public static string Norman_1_P_0_2 = "Sure, what is it?";
	public static string Norman_1_P_0_1 = "I guess, its kinda a pain though.";
	public static string Norman_1_P_0_0 = "No, you are pathetic.";

	public static string Norman_1_C_0_2 = "Tell her that I miss her and I was wrong.  Thanks kid.";
	public static string Norman_1_C_0_1 = "Tell her I miss her, please.";
	public static string Norman_1_C_0_0 = "Seriously? come on man.";

	//World 2
	//good
	public static string Addison_2_O_2 = "Hey, have you seen my husband? Maybe you've seen him around? I'm really worried...";

	public static string Addison_2_P_2_2 = "Yeah, he told me to tell you he misses you, and loves you.";
	public static string Addison_2_P_2_1 = "Uh yeah, he told me to tell you something but I forgot";
	public static string Addison_2_P_2_0 = "No I haven't";

	public static string Addison_2_C_2_2 = "Oh my god.  I hope I can find him, I miss him too.";
	public static string Addison_2_C_2_1 = "Well...thanks for nothing, I guess.";
	public static string Addison_2_C_2_0 = "Oh no, I think he ran in to find me and now we are both lost...";
	//neutral
	public static string Addison_2_O_1 = "Hey, have you seen my husband?";

	public static string Addison_2_P_1_2 = "Yeah, he told me to tell you he misses you.";
	public static string Addison_2_P_1_1 = "Uh yeah he told me to tell you something but I forgot";
	public static string Addison_2_P_1_0 = "No I haven't";

	public static string Addison_2_C_1_2 = "I miss him too! I got to find him!";
	public static string Addison_2_C_1_1 = "How could you forget?";
	public static string Addison_2_C_1_0 = "Oh no, I think he ran in to find me and now we are both lost";
	//bad
	public static string Addison_2_O_0 = "Hey have you seen my husband?";

	public static string Addison_2_P_0_2 = "I have.  I was supposed to deliver a message but I couldn't understand him.";
	public static string Addison_2_P_0_1 = "No, I haven't";
	public static string Addison_2_P_0_0 = "Yeah, he looked like quite the loser";

	public static string Addison_2_C_0_2 = "I got to find him then! We both got lost in here.";
	public static string Addison_2_C_0_1 = "Oh no, I think he ran in to find me and now we are both lost...";
	public static string Addison_2_C_0_0 = "Hey! Don't speak about my husband like that";
	#endregion
}
