using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab2 : MonoBehaviour
{

    private void OnEnable()
    {
        UIDocument document = GetComponent<UIDocument>();
        VisualElement root = document.rootVisualElement;

        UQueryBuilder<VisualElement> qb = new UQueryBuilder<VisualElement>(document.rootVisualElement);

        VisualElement contenedorBotones = root.Q(name: "CharacterButtons");
        contenedorBotones.AddToClassList("seleccionado");

        VisualElement contenedorPJE = root.Q(name: "CharacterStats");

        System.Random rng = new System.Random();
        contenedorBotones.Query<Button>(className: "button").ToList().ForEach((Button ve)=>
        {
            Debug.Log(ve.name);
            ve.style.fontSize = 50;
            ve.style.color = Color.yellow;
            ve.RegisterCallback<MouseDownEvent>(ev =>
            {
                VisualElement target = ev.target as VisualElement;
                target.style.color = Color.blue;
                contenedorPJE.style.backgroundColor = new Color(rng.Next(0,100)/100f, rng.Next(0, 100) / 100f, rng.Next(0, 100) / 100f, 1f);
            }, TrickleDown.TrickleDown);
        });
        
    }
}
