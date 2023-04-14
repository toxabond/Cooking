using Script.Core.Character;
using Zenject;

public class CharacterFactory<T> : IFactory<CharacterType, T>
{
    [Inject] private PrefabsCollection _prefabs;
    [Inject] private readonly DiContainer _container;
    

    public T Create(CharacterType itemType)
    {
        return _container.InstantiatePrefabForComponent<T>(_prefabs.characterCollection[(int)itemType]);
    }
}