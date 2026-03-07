using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Lab5 : MonoBehaviour
{
    private VisualTreeAsset tarjetaTemplate;

    private List<Individuo> individuos = new List<Individuo>();
    private Individuo selecIndividuo;

    private TextField input_nombre;
    private TextField input_apellido;
    private VisualElement contenedorImagenes;

    private VisualElement contenedorTarjetas;

    void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        contenedorTarjetas = root.Q<VisualElement>("Dcha");

        input_nombre = root.Q<TextField>("InputNombre");
        input_apellido = root.Q<TextField>("InputApellido");
        input_nombre.RegisterCallback<ChangeEvent<string>>(CambioNombre);
        input_apellido.RegisterCallback<ChangeEvent<string>>(CambioApellido);

        contenedorImagenes = root.Q<VisualElement>("header");
        contenedorImagenes.RegisterCallback<ClickEvent>(CambioImagen);

        tarjetaTemplate = Resources.Load<VisualTreeAsset>("Lab5/tarjeta");

        contenedorTarjetas.RegisterCallback<ClickEvent>(seleccionTarjeta);

        CrearDatos();
        CrearTarjetas();
    }

    private void CambioImagen(ClickEvent evt)
    {
        VisualElement img = evt.target as VisualElement;

        string bg = "Lab5/"+img.resolvedStyle.backgroundImage.sprite.name;
        Debug.Log(bg);

        if (selecIndividuo != null)
            selecIndividuo.Imagen = bg;
    }

    private void CambioApellido(ChangeEvent<string> evt)
    {
        if(selecIndividuo != null)
            selecIndividuo.Apellido = evt.newValue;
    }

    private void CambioNombre(ChangeEvent<string> evt)
    {
        if (selecIndividuo != null)
            selecIndividuo.Nombre = evt.newValue;
    }

    private void seleccionTarjeta(ClickEvent evt)
    {
        VisualElement tarjeta = evt.target as VisualElement;

        selecIndividuo = tarjeta.userData as Individuo;


        if (selecIndividuo != null)
        {
            input_nombre.SetValueWithoutNotify(selecIndividuo.Nombre);
            input_apellido.SetValueWithoutNotify(selecIndividuo.Apellido);
        }
    }

    void CrearDatos()
    {
        individuos.Add(new Individuo("Ana", "Ríos", "Lab5/img1"));
        individuos.Add(new Individuo("Luis", "Calvete", "Lab5/img2"));
        individuos.Add(new Individuo("Marta", "Martínez", "Lab5/img3"));
    }

    void CrearTarjetas()
    {
        foreach (var individuo in individuos)
        {
            VisualElement tarjetaVisual = tarjetaTemplate.Instantiate();
            contenedorTarjetas.Add(tarjetaVisual);

            Tarjeta tarjeta = new Tarjeta(tarjetaVisual.Q<VisualElement>("tarjeta"));
            tarjeta.SetData(individuo);
        }
    }
}

