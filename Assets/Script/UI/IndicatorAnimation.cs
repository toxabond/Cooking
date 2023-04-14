
    using System;
    using DG.Tweening;
    using UnityEngine;
    using Zenject;
    using Object = UnityEngine.Object;

    public class IndicatorAnimation
    {
        private Indicator.Factory _indicatorFactory;

        [Inject]
        private void Construct(Indicator.Factory factory)
        {
            _indicatorFactory = factory;
        }
        
        public void AnimationProgress(float amount, float duration,Transform parent,Vector3 position, Action action)
        {
            var indicator = _indicatorFactory.Create();
            var indicatorTransform = indicator.transform;
            
            indicatorTransform.SetParent(parent.transform);
            indicatorTransform.position = position;

            indicator.progress.fillAmount = amount;
            indicator.progress.DOFillAmount(0, duration).OnComplete(() =>
            {
                Object.Destroy(indicator.gameObject);
                action();
            });
        }
        
        public class Factory : PlaceholderFactory<IndicatorAnimation>
        {
        }
        
    }