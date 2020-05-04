using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

        
public class CutscenePlayer : MonoBehaviour
{
    public PlayableDirector pd;
    public PlayableAsset p_OpenTimeline;
    public PlayableAsset p_CloseTimeline;

    public PlayableAsset p_AddisonTimeline;
    public PlayableAsset p_HazelTimeline;
    public PlayableAsset p_LeonTimeline;
    public PlayableAsset p_NormanTimeline;
    public PlayableAsset p_RomyTimeline;
    public PlayableAsset p_TimTimeline;

    public int friendshipThreshold;


    private List<PlayableAsset> timelineArray;
    private double timer;
    private int phase;
    private int index;



    // Start is called before the first frame update
    void Start()
    {
        timelineArray = new List<PlayableAsset>();
        if (PlayerPrefs.GetInt("Addison") >= friendshipThreshold) {
            timelineArray.Add(p_AddisonTimeline);
        }
        if (PlayerPrefs.GetInt("Hazel") >= friendshipThreshold)
        {
            timelineArray.Add(p_HazelTimeline);
        }
        if (PlayerPrefs.GetInt("Leon") >= friendshipThreshold)
        {
            timelineArray.Add(p_LeonTimeline);
        }
        if (PlayerPrefs.GetInt("Norman") >= friendshipThreshold)
        {
            timelineArray.Add(p_NormanTimeline);
        }
        if (PlayerPrefs.GetInt("Romy") >= friendshipThreshold)
        {
            timelineArray.Add(p_RomyTimeline);
        }
        if (PlayerPrefs.GetInt("Tim") >= friendshipThreshold)
        {
            timelineArray.Add(p_TimTimeline);
        }
        phase = 1;
        index = 0;
        StartPhase1();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 && phase == 1) {
            phase = 2;
            ProcessPhase2();
        }
        if (timer < 0 && phase == 2) {
            ProcessPhase2();
        }
    }

    private void StartPhase1() {
        pd.playableAsset = p_OpenTimeline;
        timer = p_OpenTimeline.duration;
        pd.Play();
    }

    private void ProcessPhase2()
    {
        if (index >= timelineArray.Count)
        {
            phase = 3;
            StartPhase3();
            return;
        }
        else {
            pd.playableAsset = timelineArray[index];
            timer = timelineArray[index].duration;
            pd.Play();
            index += 1;
        }
    }

    private void StartPhase3() {
        pd.playableAsset = p_CloseTimeline;
        timer = p_OpenTimeline.duration;
        pd.Play();
    }




}
