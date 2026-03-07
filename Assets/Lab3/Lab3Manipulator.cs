using UnityEngine;
using UnityEngine.UIElements;

public class Lab3Manipulator : Manipulator
{
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
        target.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
        target.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
    }

    private void OnMouseEnter(MouseEnterEvent mev)
    {
        target.style.borderBottomColor = Color.white;
        target.style.borderLeftColor = Color.white;
        target.style.borderRightColor = Color.white;
        target.style.borderTopColor = Color.white;
        //mev.StopPropagation();
    }

    private void OnMouseLeave(MouseLeaveEvent mev)
    {
        target.style.borderBottomColor = Color.black;
        target.style.borderLeftColor = Color.black;
        target.style.borderRightColor = Color.black;
        target.style.borderTopColor = Color.black;
        //mev.StopPropagation();
    }
}
