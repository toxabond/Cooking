using System;
using DG.Tweening;
using Script.Core.Character;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BaseCharacter : MonoBehaviour, IItem
{
    public Inventory inventory;
    public Image progress;

    [HideInInspector] public Transform startPosition;
    [HideInInspector] public Transform targetPosition;
    [Inject] private CharacterSetting _characterSetting;
    private ITimer _timer;

    public void Appear(Action action)
    {
        throw new NotImplementedException();
    }

    public void Next(ItemType fromItemType, ItemType toItemType, Action action)
    {
        transform.DOMove(targetPosition.position, _characterSetting.moveTime)
            .OnComplete(() => action());
    }

    public void ApplyItem(ItemType itemType, Action action)
    {
        inventory.ApplyItem(itemType, action);
    }

    public void Disappear(Action action)
    {
        transform.DOMove(startPosition.position, _characterSetting.moveTime)
            .OnComplete(() =>
            {
                Destroy(this.gameObject);
                action();
            });
    }


    public void Bind(ItemModel modelItemModel)
    {
        _timer = modelItemModel.ItemTimer;
    }

    private void Update()
    {
        if (_timer != null)
        {
            if (Math.Abs(_timer.Duration) > float.Epsilon)
            {
                progress.fillAmount = 1 - _timer.Current / _timer.Duration;
            }
            else
            {
                progress.fillAmount = 1;
            }
        }
    }

    public class Factory : PlaceholderFactory<CharacterType, BaseCharacter>
    {
    }
}