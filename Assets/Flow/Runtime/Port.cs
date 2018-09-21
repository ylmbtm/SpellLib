﻿using System.Collections;
using System.Collections.Generic;

public delegate object ValueHandler();
public delegate void FlowHandler();
public class Port
{
    public string name;

    public List<Connection> Connections = new List<Connection>();

    public bool IsConnected { get { return Connections.Count > 0; } }
}

public class ValueIn : Port
{
    public ValueIn(string name)
    {
        this.name = name;
    }

    public object Value
    {
        get { return Connections[0].Value; }
    }
}

public class ValueOut : Port
{
    ValueHandler valueHandler;
    public ValueOut(string name)
    {
        this.name = name;
    }

    public void SetValueHandler(ValueHandler valueHandler)
    {
        this.valueHandler = valueHandler;
    }

    public object Value
    {
        get { return valueHandler(); }
    }
}

public class FlowIn : Port
{
    FlowHandler flowHandler;
    public FlowIn(string name)
    {
        this.name = name;
    }

    public void SetFlowHandler(FlowHandler flowHandler)
    {
        this.flowHandler = flowHandler;
    }

    public void Call()
    {
        if (flowHandler != null)
            flowHandler.Invoke();
    }
}

public class FlowOut : Port
{
    public FlowOut(string name)
    {
        this.name = name;
    }

    public void Call()
    {
        for (int i = 0; i < Connections.Count; i++)
            Connections[i].Call();
    }
}

