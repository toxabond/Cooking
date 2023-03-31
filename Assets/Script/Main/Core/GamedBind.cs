using System.Collections.Generic;
using System.Linq;
using Zenject;

public class GamedBind
{
    [Inject]private GameModel _gameModel;

    public Model BindPlaceWithGameObject(Place place, GameObjectModel gameObjectModel)
    {
        var model = TryCreateModel(place);
        model.GameObjectModel = gameObjectModel;
        TryBind(model);
        return model;
    }

    public Model BindPlaceWithItemModel(Place place, ItemModel itemModel)
    {
        var model = TryCreateModel(place);
        model.ItemModel = itemModel;
        TryBind(model);
        return model;
    }

    public List<GameObjectModel> GetGameObjectModels()
    {
        return _gameModel.Models.Select(g => g.Value.GameObjectModel).ToList();
    }

    public List<ItemModel> GetItemModelList()
    {
        return _gameModel.Models.Select(m => m.Value.ItemModel).ToList();
    }

    private Model TryCreateModel(Place place)
    {
        if (_gameModel.Models.TryGetValue(place.GetHashCode(), out var value))
        {
            return value;
        }

        value = new Model();
        _gameModel.Models.Add(place.GetHashCode(), value);
        return value;
    }

    private void TryBind(Model model)
    {
        if (model.ItemModel == null || model.GameObjectModel == null)
        {
            return;
        }

        model.GameObjectModel.Item?.Bind(model.ItemModel);

        model.GameObjectModel.ChangeItem += item => { item.Bind(model.ItemModel); };
    }
}