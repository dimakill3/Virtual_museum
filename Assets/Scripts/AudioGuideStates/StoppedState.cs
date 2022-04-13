using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppedState : AudioGuideState
{
    public override void Pause()
    { }

    public override void Play()
    {
        audioGuide.ChangeState(new PlayState());
    }

    public override string Run()
    {
        return "Остановлен";
    }

    public override void Stop()
    { }
}
