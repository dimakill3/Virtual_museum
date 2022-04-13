using System.Collections.Generic;
using UnityEngine;

/**
 * \brief Класс музея, управляющий залами.
 * 
 * Предназначен для добавления и хранения залов музея, а также выдачи информации о них.
 * 
 * \note Класс является **одиночкой**.
 */
public class Museum : MonoBehaviour
{
    public static Museum museum { get; private set; }

    private List<Hall> halls;

    // Инициализируем Singleton и подписываемся на продажу билета
    private void Awake()
    {
        museum = this;
        halls = new List<Hall>();

        foreach (var hall in GetComponentsInChildren<Hall>())
        {
            hall.HallInit();
            AddHall(hall);
        }
    }

    /**
     * Возвращает зал музея с указанным номером, если он имеется в музее
     * \param hallID Номер зала
     * \return Зал с указанным номером, если он имеется в музее, иначе - null.
     */
    public Hall GetHall(int hallID)
    {
        foreach (var hall in halls)
        {
            if (hall.GetHallID() == hallID)
                return hall;
        }
        Debug.Log($"Зал с номером {hallID} не найден в музее!");
        return null;
    }

    /**
     * Возвращает список всех залов музея
     * \return Список всех залов музея
     */
    public List<Hall> GetHalls()
    {
        return halls;
    }

    /**
    * Возвращает количество залов в музее
    * \return Количество залов в музее
    */
    public int GetHallCount()
    {
        return halls.Count;
    }

    /**
     * Добавляет зал в музей
     * \param hall Зал для добавления
     */
    public void AddHall(Hall hall)
    {
        halls.Add(hall);
    }
}
