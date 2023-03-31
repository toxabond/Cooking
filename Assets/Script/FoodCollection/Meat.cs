using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Sausage : FoodItem
{
    [SerializeField] private Image progress;
    [SerializeField] private Transform indicatorPosition;
    [Inject] private MeatSetting _meatSetting;
    [Inject] private IndicatorAnimation _animation;


    public override void Appear(Action action)
    {
        progress.DOColor(new Color(0.46f, 0.46f, 0.46f, 1.0f), _meatSetting.duration);
        _animation.AnimationProgress(1, _meatSetting.duration, transform, indicatorPosition.position, action);
    }

    public override void Next(ItemType fromItemType, ItemType toItemType, Action action)
    {
        if (toItemType == ItemType.DestroySausage)
        {
            Destroy(gameObject);
        }

        action();
    }

    public override void ApplyItem(ItemType itemType, Action action)
    {
        throw new NotImplementedException();
    }

    public class Factory : PlaceholderFactory<Meat>
    {
    }
}