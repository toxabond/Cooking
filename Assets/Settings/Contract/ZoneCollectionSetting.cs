using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneCollectionSetting", menuName = "Game/ZoneCollectionSetting")]
public class ZoneCollectionSetting : ScriptableObject
{
    public List<Zone> zoneCollection;
}