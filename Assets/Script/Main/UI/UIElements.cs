using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour, IUIElements
{
    [SerializeField] private List<Button> inputGroup0;
    [SerializeField] private List<Button> inputGroup1;
    [SerializeField] private List<Button> inputGroup2;
    [SerializeField] private List<Button> inputGroup3;
    [SerializeField] private List<Button> inputGroup4;
    [SerializeField] private List<Button> inputGroup5;
    [SerializeField] private List<Button> inputGroup6;
    [SerializeField] private List<Button> inputGroup7;
    [SerializeField] private List<Button> inputGroup8;

    [SerializeField] private Transform startCharacterPlace;
    [SerializeField] private List<Transform> place0;
    [SerializeField] private List<Transform> place1;
    [SerializeField] private List<Transform> place2;
    [SerializeField] private List<Transform> place3;
    [SerializeField] private List<Transform> place4;
    [SerializeField] private List<Transform> place5;
    [SerializeField] private List<Transform> place6;

    public Transform StartCharacterPlace => startCharacterPlace;
    public IReadOnlyList<Button> InputGroup0 => inputGroup0;
    public IReadOnlyList<Button> InputGroup1 => inputGroup1;
    public IReadOnlyList<Button> InputGroup2 => inputGroup2;
    public IReadOnlyList<Button> InputGroup3 => inputGroup3;
    public IReadOnlyList<Button> InputGroup4 => inputGroup4;
    public IReadOnlyList<Button> InputGroup5 => inputGroup5;
    public IReadOnlyList<Button> InputGroup6 => inputGroup6;
    public IReadOnlyList<Button> InputGroup7 => inputGroup7;
    public IReadOnlyList<Button> InputGroup8 => inputGroup8;

    public IReadOnlyList<Transform> Place0 => place0;
    public IReadOnlyList<Transform> Place1 => place1;
    public IReadOnlyList<Transform> Place2 => place2;
    public IReadOnlyList<Transform> Place3 => place3;
    public IReadOnlyList<Transform> Place4 => place4;
    public IReadOnlyList<Transform> Place5 => place5;
    public IReadOnlyList<Transform> Place6 => place6;
}