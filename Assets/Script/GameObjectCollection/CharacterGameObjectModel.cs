using UnityEngine;

public class CharacterGameObjectModel : GameObjectModel
{
    public BaseCharacter Character;

    public CharacterGameObjectModel(Transform transform, BaseCharacter character) : base(transform, character)
    {
        Character = character;
    }
}