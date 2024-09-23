using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommandLog
{
    public static SortedList<float, Command> recordedCommands = new SortedList<float, Command>();
}