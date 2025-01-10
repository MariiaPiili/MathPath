using System;
using System.Collections.Generic;


[Serializable]
public class Assignment
{
    public string AssignmentItself;
    public string Answer;
}

[System.Serializable]
public class Wrapper<T>
{
    public List<T> Items;

    public Wrapper(List<T> items)
    {
        Items = items;
    }
}
