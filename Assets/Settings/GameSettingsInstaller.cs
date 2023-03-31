using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private LevelSetting level;
    [SerializeField] private PrefabsCollection prefabsCollection;
    

    [SerializeField] private CharacterSetting characterSetting;
    
    [SerializeField] private GlassSetting glassSetting;
    [SerializeField] private MeatSetting meatSetting;
    [SerializeField] private FrenchFriesSetting frenchFriesSetting;

    public override void InstallBindings()
    {
        // Container.Bind<IndicatorAnimation>();
        // Container.BindFactory<IndicatorAnimation, IndicatorAnimation.Factory>();
        Container.Bind<IndicatorAnimation>().To<IndicatorAnimation>().AsCached();
        Container.Bind<Loader>().To<Loader>().AsSingle();
        Container.Bind<GameModel>().To<GameModel>().AsSingle();
        Container.Bind<MainController>().To<MainController>().AsSingle();
        Container.Bind<GamedBind>().To<GamedBind>().AsSingle();
        Container.Bind<GameFactory>().To<GameFactory>().AsSingle();
        Container.Bind<CharacterGenerator>().To<CharacterGenerator>().AsSingle();
        Container.Bind<GameBuilder>().To<GameBuilder>().AsSingle();
        Container.Bind<Zone00Initializer>().To<Zone00Initializer>().AsSingle();
        
        Container.BindInstance(prefabsCollection).AsSingle();
        Container.BindInstance(glassSetting).AsSingle();
        Container.BindInstance(frenchFriesSetting).AsSingle();
        Container.Bind<LevelSetting>().FromInstance(level).AsSingle();
        Container.Bind<MeatSetting>().FromInstance(meatSetting).AsSingle();
        Container.Bind<CharacterSetting>().FromInstance(characterSetting).AsSingle();
    }
}