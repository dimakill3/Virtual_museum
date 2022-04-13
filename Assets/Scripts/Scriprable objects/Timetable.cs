using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "My objects/Timetable")]
public class Timetable : ScriptableObject
{
    public
    Vector2 startTime;

    public
    Vector2 endTime;

    public int GetStartTimeInMinutes()
    {
        return (int) (startTime.x * 60 + startTime.y);
    }

    public int GetEndTimeInMinutes()
    {
        return (int) (endTime.x * 60 + endTime.y);
    }
}
