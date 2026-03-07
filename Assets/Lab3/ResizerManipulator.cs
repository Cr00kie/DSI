using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UIElements;

public class ResizerManipulator : PointerManipulator
{
    protected bool m_Active;
    private int m_PointerID;
    private Vector2 m_StartSize;

    public ResizerManipulator()
    {
        m_PointerID = -1;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse});
        m_Active = false;
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(OnPointerDown);
        target.RegisterCallback<WheelEvent>(OnWheelMove);
        target.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(OnPointerDown);
        target.UnregisterCallback<WheelEvent>(OnWheelMove);
        target.UnregisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown(PointerDownEvent pev)
    {
        if (m_Active)
        {
            pev.StopImmediatePropagation();
            return;
        }

        if (CanStartManipulation(pev))
        {
            m_StartSize = target.layout.size;
            m_PointerID = pev.pointerId;
            m_Active = true;
            target.CapturePointer(m_PointerID);
            pev.StopPropagation();
        }
    }

    private void OnWheelMove(WheelEvent wev)
    {
        if (!m_Active)
            return;

        target.style.height = m_StartSize.y + wev.delta.y * 5;
        target.style.width = m_StartSize.x + wev.delta.y * 5;

        wev.StopPropagation();
    }

    private void OnPointerUp(PointerUpEvent pev)
    {
        if (!m_Active || !target.HasPointerCapture(m_PointerID) || !CanStopManipulation(pev))
            return;

        m_Active = false;
        target.ReleasePointer(m_PointerID);
        m_PointerID = -1;
        pev.StopPropagation();
    }
}
