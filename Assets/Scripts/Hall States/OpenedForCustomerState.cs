using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedForCustomerState : HallState
{
    private string description = "Открыт";

    public override void Run()
    {
        barrier.SetActive(false);
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
    }

    public override void Close()
    {
        hall.SetState(new ClosedState());
    }

    public override void OpenForCustomer()
    {
    }
}
