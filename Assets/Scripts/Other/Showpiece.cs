using UnityEngine;

/**
 * \brief Класс экспоната, содержащий всю информацию об экспонате
 * 
 * Данный класс содержит в себе название, тематику, описание и местоположение (в каком зале) экспоната
 * 
 * Uml-диаграмма классов:
 * \startuml {myUML.png}
 *  interface IShowpiece {
 *     +GetDescription()
 *  }
 *  class Showpiece {
 *     -hallID
 *     +GetDescription()
 *  }
 *  class ShowpieceDecorator {
 *     -wrappe
 *     +SetWrappe()
 *     +GetDescription()
 *  }
 *  class ArtistLinkDecorator {
 *     +GetDescription()
 *  }
 *  class ShowpieceLinkDecorator {
 *     +GetDescription()
 *  }
 *  class DecoratorManager {
 *     -decorators
 *     +AddDecorator()
 *     +ProcessDecorator()
 *  }
 *  
 *  ShowpieceDecorator <|-- ArtistLinkDecorator
 *  ShowpieceDecorator <|-- ShowpieceLinkDecorator 
 *  DecoratorManager o-- ShowpieceDecorator
 *  IShowpiece <--o ShowpieceDecorator
 *  IShowpiece <-- Showpiece
 * \enduml
 * \image html MyUML.png
 */
public class Showpiece : MonoBehaviour, IShowpiece
{
    [SerializeField] string showpieceName;

    [SerializeField] string theme;

    [SerializeField] string description;

    private int showpieceID = -1;

    private int hallID = -1;

    /**
    * Задаёт описание экспонату
    */
    public void SetDescription(string description)
    {
        this.description = description;
    }

    /**
    * Возвращает порядковый номер зала, в котором находится экспонат
    * \return Порядковый номер зала, в котором находится экспонат
    */
    public int GetHallID()
    {
        return hallID;
    }

    /**
    * Возвращает порядковый номер экспоната
    * \return Порядковый номер экспоната
    */
    public int GetShowpieceID()
    {
        return showpieceID;
    }

    /**
     * \brief Инициализация экспоната
     * 
     * Назначение экспонату номера, и указание, в каком зале он находится
     * \param hall Зал, в котором находится экспонат
     * \see \link Hall \endlink
     */
    public void ShowpieceInit(Hall hall)
    {
        showpieceID = hall.GetShowpieceCount() + 1;
        hallID = hall.GetHallID();
    }

    /**
    * Возвращает название экспоната
    * \return Название экспоната
    */
    public string GetName()
    {
        return showpieceName;
    }

    /**
    * Возвращает тематику экспоната
    * \return Тематику экспоната
    */
    public string GetTheme()
    {
        return theme;
    }

    /**
    * Возвращает описание экспоната
    * \return Описание экспоната
    */
    public string GetDescription()
    {
        return description;
    }
}
