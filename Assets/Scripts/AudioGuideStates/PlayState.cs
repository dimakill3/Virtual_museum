using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : AudioGuideState
{

    public override void Pause()
    {
        audioGuide.ChangeState(new PausedState());
    }

    public override void Play()
    { }

    public override string Run()
    {
        return "Воспроизводит";
    }

    public override void Stop()
    {
        audioGuide.ChangeState(new StoppedState());
    }
}
