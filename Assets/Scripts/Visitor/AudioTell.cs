using TMPro;

public class AudioTell : Tell
{
    // Состояние
    AudioGuideState state;

    // Инициализируем состяоние
    public AudioTell()
    {
        state = new StoppedState();
        state.SetContext(this);
    }

    // Метод обработки аудио повествования
    public int Accept(Visitor v, int hallID)
    {
        return v.VisitAudioTell(hallID);
    }

    // Изменение состояния
    public void ChangeState(AudioGuideState st)
    {
        state = st;
        state.SetContext(this);
    }

    // Воспроизведение
    public string Play()
    {
        state.Play();
        return state.Run();
    }

    // Остановить
    public string Stop()
    {
        state.Stop();
        return state.Run();
    }

    // Поставить на паузу
    public string Pause()
    {
        state.Pause();
        return state.Run();
    }
}
