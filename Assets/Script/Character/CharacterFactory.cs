
    using Script.Core.Character;
    using Zenject;
   
    
    public class CharacterFactory<T> : IFactory<CharacterType, T>
    {
        [Inject] private PrefabsCollection _prefabs;
        [Inject]
        readonly DiContainer _container = null;

        public DiContainer Container
        {
            get { return _container; }
        }
        
        
        public T Create(CharacterType itemType)
        {
            return _container.InstantiatePrefabForComponent<T>(_prefabs.characterCollection[(int)itemType]);
        }
    }
