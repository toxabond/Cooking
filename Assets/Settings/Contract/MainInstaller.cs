using Script.Core.Character;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public MainSetting MainSetting;
    [Inject] private PrefabsCollection _prefabs;

    public override void InstallBindings()
    {

        CreateFood();
        CreateInventoryFood();
        CreateCharacter();
        // CreateGlass();
        // CreateMeat();

        CreateIndicator();
    }

    private void CreateFood()
    {
        Container.BindFactory<ItemType, FoodItem, FoodItem.Factory>()
            .FromFactory<FoodItemFactory<FoodItem>>();
    }
    
    private void CreateInventoryFood()
    {
        Container.BindFactory<ItemType, FoodIconItem, FoodIconItem.Factory>()
            .FromFactory<FoodIconItemFactory<FoodIconItem>>();
    }

    private void CreateCharacter()
    {
        Container.BindFactory<CharacterType, BaseCharacter, BaseCharacter.Factory>()
            .FromFactory<CharacterFactory<BaseCharacter>>();
    }

    // private void CreateGlass()
    // {
    //     Container.BindFactory<Glass, Glass.Factory>()
    //         .FromComponentInNewPrefab(_prefabs.food.glass);
    // }

    // private void CreateMeat()
    // {
    //     Container.BindFactory<Meat, Meat.Factory>()
    //         .FromComponentInNewPrefab(_prefabs.food.meat);
    // }

    private void CreateIndicator()
    {
        Container.BindFactory<Indicator, Indicator.Factory>()
            .FromComponentInNewPrefab(_prefabs.simpleIndicator);
        
        // Container.BindFactory<Indicator, Indicator.Factory>()
        //     .FromPoolableMemoryPool<Indicator>(x => x.WithInitialSize(20)
        //         .FromComponentInNewPrefab(_prefabs.simpleIndicator)
        //     );
    }
}