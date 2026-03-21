using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab8 : MonoBehaviour
{
    VisualElement contenidoAzul;
    VisualElement contenidoVerde;
    VisualElement contenidoAmarillo;

    VisualElement navAzul;
    VisualElement navVerde;
    VisualElement navAmarillo;

    private void NoContenido()
    {
        contenidoAzul.style.display = DisplayStyle.None;
        contenidoVerde.style.display = DisplayStyle.None;
        contenidoAmarillo.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        UIDocument uiDoc = GetComponent<UIDocument>();
        VisualElement root = uiDoc.rootVisualElement;

        VisualElement nav = root.Q<VisualElement>("Nav");
        VisualElement body = root.Q<VisualElement>("Body");

        contenidoAzul = root.Q<VisualElement>("P1");
        contenidoVerde = root.Q<VisualElement>("P2");
        contenidoAmarillo = root.Q<VisualElement>("P3");

        navAzul = root.Q<VisualElement>("Page1Nav");
        navVerde = root.Q<VisualElement>("Page2Nav");
        navAmarillo = root.Q<VisualElement>("Page3Nav");

        navAzul.RegisterCallback<ClickEvent>((evt) =>
        {
            Debug.Log("Azul");
            NoContenido();
            contenidoAzul.style.display = DisplayStyle.Flex;
        });
        navVerde.RegisterCallback<ClickEvent>((evt) =>
        {
            Debug.Log("Verde");
            NoContenido();
            contenidoVerde.style.display = DisplayStyle.Flex;
        });
        navAmarillo.RegisterCallback<ClickEvent>((evt) =>
        {
            Debug.Log("Amarillo");
            NoContenido();
            contenidoAmarillo.style.display = DisplayStyle.Flex;
        });

        Label text = contenidoAzul.Q<Label>("Story");
        text.text = @"<line-indent=15%>En un lugar de <smallcaps>La Mancha</smallcaps> </line-indent><br>
de cuyo nombre <rotate=""45"">no quiero acordarme</rotate>,
<b><gradient=""lab8TxtGradient"">no hacia mucho que vivia un hidalgo</gradient></b>
de los de lanza en astillero,
<b><color=""black""><gradient=""lab8TxtGradient"">adarga antigua</gradient></b>,
<i>rocin flaco y galgo corredor.";
    }
}
