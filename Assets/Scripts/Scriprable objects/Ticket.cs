using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Ticket : ScriptableObject
{
    // Для какого зала билет
    public int hallID;

    public int cost;

    public int GetCost()
    {
        return cost;
    }
    public int GetHallID()
    {
        return hallID;
    }
}
