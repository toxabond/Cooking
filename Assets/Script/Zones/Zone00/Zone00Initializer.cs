using Script.Core.Behaviors;
using Script.Core.Interface;
using Zenject;

public class Zone00Initializer
{
    [Inject] public IGameBuilder Builder;
    [Inject] public IGameFactory Factory;
    [Inject] public ICharacterGenerator CharacterGenerator;
    [Inject] public ICharacterCalculator CharacterGeneratorCalculator;
    private Zone00UIWrapper _ui;
    private LevelConfig _config;

    public void Init(LevelConfig levelConfig, GameModel gameModel, IUIElements uiElements)
    {
        _config = levelConfig;
        _ui = new Zone00UIWrapper(uiElements);
        Factory.Init(_ui.StartCharacterPlace);
        
        CharacterGenerator.Init(levelConfig, _ui.CharacterPlace, gameModel);
        CharacterGeneratorCalculator.Init(levelConfig);

        CreateGlass();
        CreateMeat();
        CreateBurger();
        CreateSausage();
        CreateHotdog();
        CreateFrenchFries();
    }

    private void CreateFrenchFries()
    {
        Builder
            .BindPlaceWithGameObjectModel(_ui.FrenchFriesId, _ui.FrenchFriesPlace)
            .BindPlaceWithItemModel(_ui.FrenchFriesId, 0, _config.frenchFriesPlaceAmount-1, ItemType.DestroyFrenchFries)
            .Subscription(ModifyItemType.Create, _ui.FrenchFriesId, 0, _config.frenchFriesPlaceAmount-1, _ui.FrenchFriesButton);

        for (var i = 0; i < _config.frenchFriesPlaceAmount; i++)
        {
            var sequenceBehavior = Factory.CreateFrenchFriesHandler(_ui.FrenchFriesId, i, _ui.CharacterId, 0, _config.characterPlaceAmount-1);
            _ui.FrenchFriesButtons[i + 1].onClick.AddListener(() => { sequenceBehavior.Execute(); });
        }
    }

    private void CreateSausage()
    {
        Builder
            .BindPlaceWithGameObjectModel(_ui.SausageId, _ui.SausagePlace)
            .BindPlaceWithItemModel(_ui.SausageId, 0, _config.sausagePlaceAmount-1, ItemType.DestroySausage)
            .Subscription(ModifyItemType.Create, _ui.SausageId, 0, _config.sausagePlaceAmount-1, _ui.SausageButton);
        
    }
    
    private void CreateHotdog()
    {
        Builder
            .BindPlaceWithGameObjectModel(_ui.HotdogId, _ui.HotdogPlace)
            .BindPlaceWithItemModel(_ui.HotdogId, 0, _config.hotdogPlaceAmount-1, ItemType.DestroyHotdog)
            .Subscription(ModifyItemType.Create, _ui.HotdogId, 0, _config.hotdogPlaceAmount-1, _ui.HotdogButton);

        Builder
            .SubscriptionExternal(_ui.SausageId, _ui.SausageButtons, 1, _ui.HotdogId, 0, _config.hotdogPlaceAmount-1);

        var handler = Factory.CreateHandlerOnlyExternal<ApplyHandler>(ModifyItemType.Apply, _ui.HotdogId, 0, _config.hotdogPlaceAmount-1);
        handler.itemType = ItemType.Cabbage;
        _ui.СabbageButton.onClick.AddListener(() => { handler.Execute(); });

        var handler1 = Factory.CreateHandlerOnlyExternal<ApplyHandler>(ModifyItemType.Apply, _ui.HotdogId, 0, _config.hotdogPlaceAmount-1);
        handler1.itemType = ItemType.Mustard;
        _ui.MustardButton.onClick.AddListener(() => { handler1.Execute(); });

        Builder.SubscriptionExternal(_ui.HotdogId, _ui.HotdogButtons, 1, _ui.CharacterId, 0, _config.characterPlaceAmount-1);
    }

    private void CreateMeat()
    {
        Builder
            .BindPlaceWithGameObjectModel(_ui.MeatId, _ui.MeatPlace)
            .BindPlaceWithItemModel(_ui.MeatId, 0, _config.meatPlaceAmount-1, ItemType.DestroyMeat)
            .Subscription(ModifyItemType.Create, _ui.MeatId, 0, _config.meatPlaceAmount-1, _ui.MeatButtons[0]);
    }

    private void CreateBurger()
    {
        Builder
            .BindPlaceWithGameObjectModel(_ui.BurgerId, _ui.BurgerPlace)
            .BindPlaceWithItemModel(_ui.BurgerId, 0, _config.burgerPlaceAmount-1, ItemType.DestroyBurger)
            .Subscription(ModifyItemType.Create, _ui.BurgerId, 0, _config.burgerPlaceAmount-1, _ui.BunButton);

        Builder
            .SubscriptionExternal(_ui.MeatId, _ui.MeatButtons, 1, _ui.BurgerId, 0, _config.burgerPlaceAmount-1);

        var handler = Factory.CreateHandlerOnlyExternal<ApplyHandler>(ModifyItemType.Apply, _ui.BurgerId, 0, _config.burgerPlaceAmount-1);
        handler.itemType = ItemType.Tomato;
        _ui.СheeseButton.onClick.AddListener(() => { handler.Execute(); });

        var handler1 = Factory.CreateHandlerOnlyExternal<ApplyHandler>(ModifyItemType.Apply, _ui.BurgerId, 0, _config.burgerPlaceAmount-1);
        handler1.itemType = ItemType.Cheese;
        _ui.TomatoButton.onClick.AddListener(() => { handler1.Execute(); });

        Builder.SubscriptionExternal(_ui.BurgerId, _ui.BurgerButtons, 1, _ui.CharacterId, 0, _config.characterPlaceAmount-1);
    }

    private void CreateGlass()
    {
        Builder
            .BindPlaceWithGameObjectModel(_ui.GlassId, _ui.GlassPlace, 0, _config.glassPlaceAmount-1, ItemType.EmptyGlass)
            .BindPlaceWithItemModel(_ui.GlassId, 0, _config.glassPlaceAmount-1, ItemType.EmptyGlass);
        for (var i = 0; i < _config.glassPlaceAmount; i++)
        {
            var sequenceBehavior = Factory.CreateGlassHandler(_ui.GlassId, i, _ui.CharacterId, 0, _config.characterPlaceAmount-1);
            _ui.GlassButtons[i].onClick.AddListener(() => { sequenceBehavior.Execute(); });
        }
    }
}