using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUIElements
{
    public IReadOnlyList<Button> InputGroup0 { get; }
    public IReadOnlyList<Button> InputGroup1 { get; }
    public IReadOnlyList<Button> InputGroup2 { get; }
    public IReadOnlyList<Button> InputGroup3 { get; }
    public IReadOnlyList<Button> InputGroup4 { get; }
    public IReadOnlyList<Button> InputGroup5 { get; }
    public IReadOnlyList<Button> InputGroup6 { get; }
    public IReadOnlyList<Button> InputGroup7 { get; }
    public IReadOnlyList<Button> InputGroup8 { get; }

    public Transform StartCharacterPlace { get; }
    public IReadOnlyList<Transform> Place0 { get; }
    public IReadOnlyList<Transform> Place1 { get; }
    public IReadOnlyList<Transform> Place2 { get; }
    public IReadOnlyList<Transform> Place3 { get; }
    public IReadOnlyList<Transform> Place4 { get; }
    public IReadOnlyList<Transform> Place5 { get; }
    public IReadOnlyList<Transform> Place6 { get; }
}