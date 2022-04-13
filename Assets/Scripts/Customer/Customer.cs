using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    // Singleton
    public static Customer customer { get; private set; }
    // Количество денег
    [SerializeField] int money = 500;
    // Купленные билеты
    private List<Ticket> tickets = new List<Ticket>();
    // Купленные гиды
    private List<Guide> guides = new List<Guide>();

    // Инициализация Singleton
    private void Awake()
    {
        customer = this;
    }

    /// <summary>
    /// Покупка билета
    /// </summary>
    /// <param name="ticket"> покупаемый билет </param>
    /// <returns> bool, если хватает денег, иначе false </returns>
    public bool BuyTicket(Ticket ticket)
    {
        if (money - ticket.GetCost() < 0)
            return false;

        tickets.Add(ticket);
        money -= ticket.GetCost();
        return true;
    }

    /// <summary>
    /// Покупка гида
    /// </summary>
    /// <param name="guide"> покупаемый гид </param>
    /// <returns> bool, если хватает денег, иначе false </returns>
    public bool BuyGuide(Guide guide)
    {
        if (money - guide.GetCost() < 0)
            return false;

        guides.Add(guide);
        money -= guide.GetCost();
        return true;
    }

    /// <summary>
    /// Возвращает количество денег посетителя
    /// </summary>
    /// <returns></returns>
    public int GetMoney()
    {
        return money;
    }

    /// <summary>
    /// Поиск у пользователя билета от зала
    /// </summary>
    /// <param name="hallID"> номер зала </param>
    /// <returns> true, если билет у пользователя найден, иначе false </returns>
    public bool FindTicket(int hallID)
    {
        foreach (var ticket in tickets)
            if (ticket.GetHallID() == hallID)
                return true;

        return false;
    }

    /// <summary>
    /// Поиск у пользователя гида соответствующего типа для зала
    /// </summary>
    /// <param name="hallID"> номер зала </param>
    /// <param name="telltype"> тип гида </param>
    /// <returns> true, если билет у пользователя найден, иначе false </returns>
    public bool FindGuide(int hallID, Type telltype)
    {
        foreach (var guide in guides)
            if (guide.GetHallID() == hallID && guide.GetTellType() == telltype)
                return true;

        return false;
    }

    /// <summary>
    /// Получить гида определённого типа
    /// </summary>
    /// <param name="hallID"> Номер зала для которого ищется гид </param>
    /// <param name="telltype"> Тип повествования гида </param>
    /// <returns></returns>
    public Guide GetGuide(int hallID, Type telltype)
    {
        foreach (var guide in guides)
            if (guide.GetHallID() == hallID && guide.GetTellType() == telltype)
                return guide;

        return null;
    }

    /// <summary>
    /// Поиск у пользователя гида любого типа для зала
    /// </summary>
    /// <param name="hallID"> номер зала </param>
    /// <returns> true, если билет у пользователя найден, иначе false </returns>
    public bool FindAnyHallGuide(int hallID)
    {
        foreach (var guide in guides)
            if (guide.GetHallID() == hallID)
                return true;

        return false;
    }
}
