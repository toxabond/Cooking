using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Core.Interface
{
    public interface IUIElements
    {
        public List<Button> InputGroup0 { get; }
        public List<Button> InputGroup1 { get; }
        public List<Button> InputGroup2 { get; }
        public List<Button> InputGroup3 { get; }
        public List<Button> InputGroup4 { get; }
        public List<Button> InputGroup5 { get; }
        public List<Button> InputGroup6 { get; }
        public List<Button> InputGroup7 { get; }
        public List<Button> InputGroup8 { get; }
        
        public Transform StartCharacterPlace { get; }
        public List<Transform> Place0 { get; }
        public List<Transform> Place1 { get; }
        public List<Transform> Place2 { get; }
        public List<Transform> Place3 { get; }
        public List<Transform> Place4 { get; }
        public List<Transform> Place5 { get; }
        public List<Transform> Place6 { get; }
        
    }
}