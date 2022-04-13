using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideCost : Visitor
{
    // Расчёт стоимости гида с аудио повествованием
    public int VisitAudioTell(int hallID)
    {
        return 200 * Museum.museum.GetHall(hallID).GetShowpieceCount() / 10;
    }

    // Расчёт стоимости гида с текстовым повествованием
    public int VisitTextTell(int hallID)
    {
        return 100 * Museum.museum.GetHall(hallID).GetShowpieceCount() / 10;
    }
}
