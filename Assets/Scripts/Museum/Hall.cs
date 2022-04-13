using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * \brief Класс зала, организующий работу с залом и экспонатами внутри него
 * 
 * Предназначен для следующих операций с залом:
 * - Проверка расписания
 * - Изменение состояний зала на основе расписания и действий игрока
 * - Уведомление слушателей о скором закрытии, закрытии или изменении состояния зала
 * 
 * Также в данном классе хранится список экспонатов внутри зала, есть возможность добавления новых
 */
public class Hall : MonoBehaviour
{
    [SerializeField] int hallID = -1;

    [SerializeField] Ticket ticket;

    [SerializeField] string theme = "None";

    [SerializeField] int showpieceLimit = 10;

    [SerializeField] Sprite preview;

    HallState currentState;

    public Timetable timetable;

    [SerializeField] GameObject barrier;

    [SerializeField] GameObject exit;
    /// Событие закрытия зала
    public Action<int> OnHallClose;
    /// Событие изменения состояния
    public Action<int> OnStateChanged;
    /// Событие скорого закрытия зала
    public Action<int, int> OnPreClose;
    // Ограничение, чтобы событие скорого закрытия зала не срабатывало много раз
    private bool preClosed = true;

    private List<IShowpiece> showpieces;

    // Данные, чтобы знать текущее время
    private ExternalData dataForTime;

    // Задаём начальное состояние
    private void Awake()
    {
        FindObjectOfType<ExternalData>().Awake();
        dataForTime = ExternalData.externalData;

        FirstStateSet();
    }

    // Подписываемся на продажу билета для изменения состояния при этом
    private void OnEnable()
    {
        FindObjectOfType<ButtonActions>().OnSellTicket += OnSellTicket;
    }

    // Проверка времени (на скорое закрытие или время для изменения состояния)
    private void Update()
    {
       CheckTime();
       CheckTimeForClose();
    }

    /**
     * Возвращет номер зала
     * \return Номер зала
     */
    public int GetHallID()
    {
        return hallID;
    }

    /**
     * \brief Возвращает билет от зала
     * 
     * Используется при покупке билета игроком
     * \return Билет от зала Ticket
     */
    public Ticket GetTicket()
    {
        return ticket;
    }

    /**
    * Возвращает описание зала
    * \return Описание зала
    */
    public string GetTheme()
    {
        return theme;
    }

    /**
     * Возвращает текущее количество экспонатов в зале
     * \return Текущее количество экспонатов в зале
     */
    public int GetShowpieceCount()
    {
        return showpieces.Count;
    }

    /**
     * Возвращает превью зала
     * \return Sprite превью зала
     * 
     * \warning Следующая формула не используется в методе!!!
     * 
     * \f[ 
     * (x+a)^n = \sum_{k = 0}^{n}(\frac{n}{k})x^k a^{n-k}
     * \f]
     */
    public Sprite GetPreview()
    {
        return preview;
    }

    /**
     * Задаёт состояние зала
     * \param state Задаваемое состояние
     */
    public void SetState(HallState state)
    {
        currentState = state;
        currentState.SetContext(this);

        currentState.Run();

        OnStateChanged?.Invoke(this.GetHallID() - 1);
        preClosed = true;

        if (currentState is ClosedState)
            OnHallClose?.Invoke(this.GetHallID());
    }

    /**
     * Возвращает состояние зала
     * \return Состояние зала HallState
     */
    public HallState GetState()
    {
        return currentState;
    }

    /**
     * \brief Проверяет расписание и изменяет состояние зала, если нужно
     * 
     * Изменяет состояние зала в зависимости от расписания:
     * - Зал закрывается по расписанию
     * - Зал открывается по расписанию
     * - Если зал открыт и пользователь купил билет от зала, то зал открывается для посетителя
     */
    public void CheckTime()
    {
        int minutes = dataForTime.GetTimeInMinutes();

        if (timetable.GetStartTimeInMinutes() == timetable.GetEndTimeInMinutes())
        {
            if (Customer.customer.FindTicket(hallID))
                currentState.OpenForCustomer();
            else
                currentState.Open();
        }
        else if (timetable.GetStartTimeInMinutes() < timetable.GetEndTimeInMinutes())
        {
            if (minutes > timetable.GetStartTimeInMinutes() && minutes < timetable.GetEndTimeInMinutes())
            {
                if (Customer.customer.FindTicket(hallID))
                    currentState.OpenForCustomer();
                else
                    currentState.Open();
            }
            else
            {
                currentState.Close();
            }
        }
        else
        {
            if (minutes > timetable.GetEndTimeInMinutes() && minutes < timetable.GetStartTimeInMinutes())
            {
                currentState.Close();
            }
            else 
            {
                if (Customer.customer.FindTicket(hallID))
                    currentState.OpenForCustomer();
                else
                    currentState.Open();
            }
        }
    }

    /// <summary>
    /// Задаёт первое состояние зала
    /// </summary>
    private void FirstStateSet()
    {
        int minutes = ExternalData.externalData.GetTimeInMinutes();

        if (timetable.GetStartTimeInMinutes() == timetable.GetEndTimeInMinutes())
        {
            currentState = new OpenedState();
        }
        else if (timetable.GetStartTimeInMinutes() < timetable.GetEndTimeInMinutes())
        {
            if (minutes > timetable.GetStartTimeInMinutes() && minutes < timetable.GetEndTimeInMinutes())
            {
                currentState = new OpenedState();
            }
            else
            {
                currentState = new ClosedState();
            }
        }
        else
        {
            if (minutes > timetable.GetEndTimeInMinutes() && minutes < timetable.GetStartTimeInMinutes())
            {
                currentState = new ClosedState();
            }
            else
            {
                currentState = new OpenedState();
            }
        }

        currentState.SetContext(this);
    }

    /**
     * Возвращает расписание зала в текстовом виде
     * 
     * \return Расписание зала в формате HH:MM - HH:MM 
     */
    public string GetTimetableInString()
    {
        int startMinutes = timetable.GetStartTimeInMinutes();
        int endMinutes = timetable.GetEndTimeInMinutes();
        string startHours = ((startMinutes / 60) / 10) < 1 ? "0" + (startMinutes / 60).ToString() : (startMinutes / 60).ToString();
        string startMin = ((startMinutes % 60) / 10) < 1 ? "0" + (startMinutes % 60).ToString() : (startMinutes % 60).ToString();

        string endHours = ((endMinutes / 60) / 10) < 1 ? "0" + (endMinutes / 60).ToString() : (endMinutes / 60).ToString();
        string endtMin = ((endMinutes % 60) / 10) < 1 ? "0" + (endMinutes % 60).ToString() : (endMinutes % 60).ToString();

        return $"{startHours}:{startMin} - {endHours}:{endtMin}";
    }

    /**
     * \brief Проверка расписания зала на скорое закрытие
     * 
     * Если до закрытия зала осталось 30 минут, запускается событие OnPreClose
     */
    public void CheckTimeForClose()
    {
        int minutes = dataForTime.GetTimeInMinutes();
        int dopMinutes = timetable.GetEndTimeInMinutes();

        if (timetable.GetStartTimeInMinutes() == timetable.GetEndTimeInMinutes())
            return;

        if (timetable.GetEndTimeInMinutes() - 60 < 0)
            dopMinutes = 24 * 60 + timetable.GetEndTimeInMinutes();

        if (minutes == dopMinutes - 60 && preClosed == true)
        {
            OnPreClose?.Invoke(hallID, 60);
            preClosed = false;
        }
    }
    
    /**
     * Добавление экспоната в зал
     * \param sp Экспонат для добавления
     */
    public void AddShowpiece(Showpiece sp)
    {
        if (showpieces.Count < showpieceLimit)
            showpieces.Add(sp);
        else
            Debug.Log($"Не хватает места для экспоната в зале {hallID}");
    }

    /**
     * \brief Инициализация зала
     * 
     * Метод выполняет последовательность действий по инициализации зала:
     * - Присваивает залу id
     * - Инициализируем список экспонатов
     * - Считываем экспонаты в зале 
     *      - Инициализируем экспонат
     *      - Добавляем экспонат в список
     *      
     * \see \link Showpiece::ShowpieceInit \endlink
     * \see AddShowpiece
     */
    public void HallInit()
    {
        hallID = Museum.museum.GetHallCount() + 1;

        showpieces = new List<IShowpiece>();

        foreach (var sp in GetComponentsInChildren<Showpiece>())
        {
            sp.ShowpieceInit(this);

            AddShowpiece(sp);
        }
    }

    /**
     * \brief Открывает зал для посетителя, если тот приобрёл билет
     * 
     * Если зал открыт по расписанию, то при покупке билета игроком зал открывается
     * \param _hallID Номер зала, для которого был куплен билет
     * \note Является подпиской на событие OnSellTicket в классе ButtonActions
     */
    public void OnSellTicket(int _hallID)
    {
        if (hallID == _hallID && currentState is OpenedState)
            currentState.OpenForCustomer();
    }

    /**
     * \brief Возвращает позицию выхода из зала
     *  
     *  Нужен для того, чтобы знать, куда выгонять посетителя при закрытии зала
     *  \return позиция выхода из зала
     */
    public Vector3 GetExitPosition()
    {
        return exit.transform.position;
    }

    /**
     * Возвращает ограждение зала
     * \return GameObject ограждения зала
     * \n
     * В игре ограждение выглядит следующим образом:
     * \image html barier.png
     */
    public GameObject GetBarrier()
    {
        return barrier;
    }
}
