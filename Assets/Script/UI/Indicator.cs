using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Indicator : MonoBehaviour//, IPoolable<IMemoryPool>
{
    public Image progress;
    IMemoryPool _pool;
    

    private void Start()
    {
        gameObject.SetActive(true);
    }
    //
    // void Reset()
    // {
    //     gameObject.SetActive(true);
    // }
    //
    //
    // public void Dispose()
    // {
    //     if (_pool != null)
    //     {
    //         gameObject.SetActive(false);
    //         _pool?.Despawn(this);
    //     }
    // }
    //
    // public void OnDespawned()
    // {
    //     _pool = null;
    // }
    //
    // public void OnSpawned(IMemoryPool pool)
    // {
    //     _pool = pool;
    //     Reset();
    // }
    
    public class Factory : PlaceholderFactory<Indicator>
    {
    }
}
