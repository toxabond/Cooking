using System;
using UnityEngine;

public class GameObjectModel
{
    public Transform Transform { get; }
    private IItem _item;

    public IItem Item
    {
        get => _item;
        set
        {
            _item = value;
            ChangeItem(_item);
        }
    }

    public event Action<IItem> ChangeItem = delegate { };

    public GameObjectModel(Transform transform, IItem item)
    {
        Transform = transform;
        this.Item = item;
    }
}