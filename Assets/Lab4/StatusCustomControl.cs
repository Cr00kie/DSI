using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
public class StatusCustomControl : VisualElement
{
    public new class UxmlFactory : UxmlFactory<StatusCustomControl, UxmlTraits> { }

    VisualElement container;

    int value;
    public int Value
    {
        get => value;
        set
        {
            this.value = value;
            RegenerateIcons();
        }
    }

    string statName;
    public string StatName
    {
        get => statName;
        set
        {
            this.statName = value;
            Label label = container.Q("StatName") as Label;
            if (label != null)
            {
                label.text = value;
            }
        }
    }

    string iconName;
    public string IconName
    {
        get => iconName;
        set
        {
            this.iconName = value;
            RegenerateIcons();
        }
    }

    public StatusCustomControl()
    {
        VisualTreeAsset statPanel = Resources.Load<VisualTreeAsset>("StatPanel");

        container = statPanel.Instantiate();

        RegenerateIcons();

        hierarchy.Add(container);
    }

    void RegenerateIcons()
    {
        VisualElement iconCont = container.Q("IconContainer");
        iconCont.Clear();
        for (int i = 0; i < 5; i++)
        {
            Image image = new Image();

            image.style.width = 100;
            image.style.height = 100;

            if (i >= value) image.style.opacity = 0.5f;

            Debug.Log(iconCont);
            image.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(iconName));

            iconCont.Add(image);
        }
    }
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlIntAttributeDescription value = new UxmlIntAttributeDescription { name = "Valor", defaultValue = 0 };
        UxmlStringAttributeDescription name = new UxmlStringAttributeDescription { name = "Nombre", defaultValue = "Stat Name" };
        UxmlStringAttributeDescription icon = new UxmlStringAttributeDescription { name = "Icon", defaultValue = "swordIcon" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var statusCustomControl = ve as StatusCustomControl;
            if (statusCustomControl != null)
            {
                statusCustomControl.IconName = icon.GetValueFromBag(bag, cc);
                statusCustomControl.StatName  = name.GetValueFromBag(bag, cc);
                statusCustomControl.Value = value.GetValueFromBag(bag, cc);
            }
        }
    }
}
