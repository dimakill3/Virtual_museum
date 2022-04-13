using UnityEngine;

public class ClosedState : HallState
{
    // Описание состояния
    string description = "Закрыт";

    /// <summary>
    /// Запуск работы состояния
    /// </summary>
    public override void Run()
    {
        barrier.SetActive(true);
    }

    /// <summary>
    /// Получить описание состояния
    /// </summary>
    /// <returns></returns>
    public override string GetDescription()
    {
        return description;
    }

    public override void Open()
    {
        hall.SetState(new OpenedState());
    }

    public override void Close()
    {
    }

    public override void OpenForCustomer()
    {
        hall.SetState(new OpenedForCustomerState());
    }
}
