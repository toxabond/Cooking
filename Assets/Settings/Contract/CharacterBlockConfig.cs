using System;
using System.Collections.Generic;
using Script.Core.Character;
using UnityEngine.Serialization;

[Serializable]
public class CharacterBlockConfig
{
    public int createdTime;
    public CharacterType type;
    public int position;
    public List<RewardFoodType> inventory;
}