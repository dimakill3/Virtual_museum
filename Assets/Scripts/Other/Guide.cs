using System;

public class Guide
{
    // Номер зала
    private int hallID = 1;
    // Стоимость
    private int cost = 100;
    // Тип
    public Tell tellType;

    // Инициализация гида, и расчёт его стоимости
    public Guide(int _hallID, Tell _tellType)
    {
        hallID = _hallID;
        tellType = _tellType;
        cost = tellType.Accept(new GuideCost(), _hallID);
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetHallID()
    {
        return hallID;
    }

    public Type GetTellType()
    {
        return tellType.GetType();
    }

    /// <summary>
    /// Возвращает гида, если это аудиогид
    /// </summary>
    /// <returns></returns>
    public AudioTell GetAudioGuide()
    {
        return tellType.GetType() == typeof(AudioTell) ? (AudioTell) tellType : null;
    }
}
