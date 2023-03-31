using System;
using UnityEngine;
using Zenject;

public class FrenchFries : FoodItem
{
    [SerializeField] private Transform indicatorPosition;
    public GameObject phase0;
    public GameObject phase1;
    public GameObject phase2;
    [Inject] private FrenchFriesSetting _glassSetting;

    [Inject] private IndicatorAnimation _animation;

    public override void Appear(Action action)
    {
        SetAllVisible(false);
        phase0.SetActive(true);
        action();
    }

    private void SetAllVisible(bool visible)
    {
        phase0.SetActive(visible);
        phase1.SetActive(visible);
        phase2.SetActive(visible);
    }


    public override void Next(ItemType fromItemType, ItemType toItemType, Action action)
    {
        if (fromItemType == ItemType.FullFrenchFries && toItemType == ItemType.DestroyFrenchFries)
        {
            Destroy(gameObject);
            action();
        }
        else
        {
            AnimationProgress(1, () =>
            {
                UpdatePhase(toItemType);
                action();
            });
        }
    }

    private void UpdatePhase(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Phase0FrenchFries:
                SetAllVisible(false);
                phase0.SetActive(true);
                break;
            case ItemType.Phase1FrenchFries:
                SetAllVisible(false);
                phase1.SetActive(true);
                break;
            case ItemType.FullFrenchFries:
                SetAllVisible(false);
                phase2.SetActive(true);
                break;
        }
    }

    public override void ApplyItem(ItemType itemType, Action action)
    {
    }


    private void AnimationProgress(float amount, Action action)
    {
        _animation.AnimationProgress(amount, _glassSetting.duration, transform, indicatorPosition.position, action);
    }
}