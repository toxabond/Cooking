using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Indicator : MonoBehaviour
{
    public Image progress;


    private void Start()
    {
        gameObject.SetActive(true);
    }


    public class Factory : PlaceholderFactory<Indicator>
    {
    }
}