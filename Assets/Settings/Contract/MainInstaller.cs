using Script.Core.Character;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [Inject] private PrefabsCollection _prefabs;

    public override void InstallBindings()
    {
        CreateFood();
        CreateInventoryFood();
        CreateCharacter();
        CreatePopupManager();
        CreateIndicator();
    }

    private void CreatePopupManager()
    {
        Container.Bind<IPopupManager>().To<PopupManager>()
            .FromComponentInNewPrefab(_prefabs.popupManager).AsSingle();
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

    private void CreateIndicator()
    {
        Container.BindFactory<Indicator, Indicator.Factory>()
            .FromComponentInNewPrefab(_prefabs.simpleIndicator);
    }
}