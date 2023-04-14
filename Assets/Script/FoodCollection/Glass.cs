using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Glass : FoodItem
{
    [SerializeField] private Image progress;
    [SerializeField] private Transform indicatorPosition;
    [Inject] private GlassSetting _glassSetting;

    [Inject] private IndicatorAnimation _animation;

    private void Start()
    {
        SetProgress(0);
    }

    public override void Appear(Action action)
    {
        action();
    }

    public override void Next(ItemType fromItemType, ItemType toItemType, Action action)
    {
        if (fromItemType == ItemType.EmptyGlass && toItemType == ItemType.FullGlass)
        {
            AnimationProgress(1, action);
        }
        else if (fromItemType == ItemType.FullGlass && toItemType == ItemType.EmptyGlass)
        {
            SetProgress(0);
            action();
        }
    }

    public override void ApplyItem(ItemType itemType, Action action)
    {
    }

    public void SetProgress(float amount)
    {
        progress.fillAmount = amount;
    }

    public void AnimationProgress(float amount, Action action)
    {
        progress.fillAmount = 0;
        progress.DOFillAmount(amount, _glassSetting.duration);
        _animation.AnimationProgress(amount, _glassSetting.duration, transform, indicatorPosition.position, action);
    }
}