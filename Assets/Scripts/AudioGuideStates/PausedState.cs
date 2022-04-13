using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : AudioGuideState
{

    public override void Pause()
    { }

    public override void Play()
    {
        audioGuide.ChangeState(new PlayState());
    }

    public override string Run()
    {
        return "На паузе";
    }

    public override void Stop()
    {
        audioGuide.ChangeState(new StoppedState());
    }
}
