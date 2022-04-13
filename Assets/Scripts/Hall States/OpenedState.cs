using UnityEngine;

public class OpenedState : HallState
{
    // Описание состояния
    string description = "Открыт";

    /// <summary>
    /// Запуск работы состояния
    /// </summary>
    public override void Run()
    {
        ;
    }

    /// <summary>
    /// Получить описание состояния
    /// </summary>
    /// <returns></returns>
    public override string GetDescription()
    {
        return description;
    }

    public override  void Open()
    {
    }

    public override  void Close()
    {
        hall.SetState(new ClosedState());
    }

    public override  void OpenForCustomer()
    {
        hall.SetState(new OpenedForCustomerState());
    }
}
