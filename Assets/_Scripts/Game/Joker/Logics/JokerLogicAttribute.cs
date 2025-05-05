using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class JokerLogicAttribute : Attribute
{
    public string ID { get; }

    public JokerLogicAttribute(string id)
    {
        ID = id;
    }
}