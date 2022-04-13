using UnityEngine;

public abstract class HallState
{
    protected Hall hall;

    protected GameObject barrier;

    public void SetContext(Hall hall)
    {
        this.hall = hall;
        barrier = hall.GetBarrier();
    }

    public abstract void Run();

    public abstract void Open();

    public abstract void Close();

    public abstract void OpenForCustomer();

    public abstract string GetDescription();
}
