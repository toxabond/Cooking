using System;
using System.ComponentModel;
using UnityEngine.Serialization;

[Serializable]
public class CharacterSetting
{
[Description("time sec")]
    public float moveTime = 3.0f;

    public float waitTime = 30.0f;
}