using System.Collections.Generic;
using Script.Core.Interface;
using UnityEngine;
using UnityEngine.UI;

public class Zone00UIWrapper
{
    private readonly IUIElements _ui;

    public Zone00UIWrapper(IUIElements ui)
    {
        _ui = ui;
    }

    public List<Button> GlassButtons => _ui.InputGroup0;
    public List<Button> MeatButtons => _ui.InputGroup1;
    public Button СheeseButton => _ui.InputGroup2[0];
    public Button TomatoButton => _ui.InputGroup2[1];
    public Button BunButton => _ui.InputGroup3[0];
    public List<Button> BurgerButtons => _ui.InputGroup3;
    public Button HotdogButton => _ui.InputGroup4[0];
    public List<Button> HotdogButtons => _ui.InputGroup4;
    public Button СabbageButton => _ui.InputGroup5[0];
    public Button MustardButton => _ui.InputGroup5[1];
    public Button SausageButton => _ui.InputGroup6[0];
    public List<Button> SausageButtons => _ui.InputGroup6;
    public Button FrenchFriesButton => _ui.InputGroup7[0];
    public List<Button> FrenchFriesButtons => _ui.InputGroup7;

    public Transform StartCharacterPlace => _ui.StartCharacterPlace;
    public List<Transform> CharacterPlace => _ui.Place0;
    public List<Transform> GlassPlace => _ui.Place1;
    public List<Transform> MeatPlace => _ui.Place2;
    public List<Transform> BurgerPlace => _ui.Place3;
    public List<Transform> HotdogPlace => _ui.Place4;
    public List<Transform> SausagePlace => _ui.Place5;
    public List<Transform> FrenchFriesPlace => _ui.Place6;


    public int CharacterId = 0;
    public int GlassId = 1;
    public int MeatId = 2;
    public int BurgerId = 3;
    public int HotdogId = 4;
    public int SausageId = 5;
    public int FrenchFriesId = 6;
}