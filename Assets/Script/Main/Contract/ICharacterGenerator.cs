using System.Collections.Generic;
using UnityEngine;

public interface ICharacterGenerator
{
    void Init(LevelConfig levelConfig, IReadOnlyList<Transform> uiCharacterPlace, GameModel gameModel);
    bool TryCreateCharacter(Place place, CharacterBlockConfig block);
    bool HasReadyCharacter { get; }
    CharacterBlockConfig Dequeue();
}