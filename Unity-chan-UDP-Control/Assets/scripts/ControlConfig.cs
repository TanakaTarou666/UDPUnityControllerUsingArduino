using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class UDPSettings
{
    public string ipAddress;
    public int port;
}

[Serializable]
public class MovementData
{
    public Vector3 moveDirection;
    public Vector3 rotateDirection;
    public bool isFirstPerson;
}

[Serializable]
public class ControlConfig
{
    public string controlType;
    public UDPSettings udpSettings;
    public MovementData movementData;
}

public static class ControlConfigLoader
{
    public static ControlConfig LoadConfig(TextAsset jsonTextAsset)
    {
        return JsonUtility.FromJson<ControlConfig>(jsonTextAsset.text);
    }
}
