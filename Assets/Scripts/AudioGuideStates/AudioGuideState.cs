using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioGuideState
{
    protected AudioTell audioGuide;

    // Задать гида для состояния
    public void SetContext(AudioTell at)
    {
        audioGuide = at;
    }

    // Выполнить действие аудиогида при определённом состоянии
    public abstract string Run();

    public abstract void Play();
    public abstract void Stop();
    public abstract void Pause();
}
