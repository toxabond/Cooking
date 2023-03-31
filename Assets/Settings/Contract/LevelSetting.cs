using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(fileName = "LevelSetting", menuName = "Game/LevelSetting")]
public class LevelSetting : ScriptableObject
{
    public string externalUrl;
    [FormerlySerializedAs("UseLocalLevel")]
    public bool isUseLocalLevel;
    public LevelConfig levelConfig;
}

[Serializable]
public class LevelConfig
{
    public int idZone = 0;
    public float levelTime = 5 * 60;
    public int characterPlaceAmount = 4;
    public int glassPlaceAmount = 3;
    public int meatPlaceAmount = 3;
    public int burgerPlaceAmount = 3;
    public int hotdogPlaceAmount = 3;
    public int sausagePlaceAmount = 3;
    public int frenchFriesPlaceAmount = 3;
    
    public bool isGenerateLevel = false;
    public GenerationConfiguration generationConfiguration;

    public List<CharacterBlockConfig> characterConfigs;
    
    public int CharacterAmount {
        get
        {
            return isGenerateLevel ? generationConfiguration.characterAmount : characterConfigs.Count;
        }
    }
}
[Serializable]
public class GenerationConfiguration
{
    public int characterAmount = 100;
    
    public int glassAmount = 100;
    public int burgerAmount = 50;
    public int hotdogAmount = 50;
    public int frenchFriesAmount = 70;
}

