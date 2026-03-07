using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab3Script : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        List<VisualElement> izq = root.Q("izqa").Children().ToList();
        List<VisualElement> der = root.Q("dcha").Children().ToList();

        izq.ForEach(elem => { elem.AddManipulator(new ResizerManipulator()); elem.AddManipulator(new Lab3Manipulator()); });
        der.ForEach(elem => { elem.AddManipulator(new ResizerManipulator()); elem.AddManipulator(new Lab3Manipulator()); });
    }
}
