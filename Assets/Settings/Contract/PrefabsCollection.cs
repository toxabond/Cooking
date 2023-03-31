using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrefabsCollection
{
    public List<GameObject> characterCollection;
    
    public FoodPrefab food;
    public FoodIconPrefab foodIcon;
    
    [Header("Elements")] 
    public GameObject simpleIndicator;
}

[Serializable]
public class FoodIconPrefab
{
    public GameObject glassIcon;
    public GameObject burgerIcon;
    public GameObject hotdogIcon;
    public GameObject frenchFriesIcon;

}

[Serializable]
public class FoodPrefab
{
    public GameObject glass;
    public GameObject burger;
    public GameObject meat;
    public GameObject hotdog;
    public GameObject sausage;
    public GameObject frenchFries;

}