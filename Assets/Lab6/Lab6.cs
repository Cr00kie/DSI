using Lab6_namespace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace lab6
{

    public class Lab6 : MonoBehaviour
    {
        private VisualTreeAsset tarjetaTemplate;

        private List<Individuo> individuos = new List<Individuo>();
        private Individuo selecIndividuo;

        private TextField input_nombre;
        private TextField input_apellido;
        private VisualElement botonCrear;
        private VisualElement botonGuardar;
        private Toggle toggleModificar;

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


            botonCrear = root.Q<VisualElement>("BotonCrear");
            botonGuardar = root.Q<VisualElement>("BotonGuardar");
            toggleModificar = root.Q<Toggle>("ToggleModificar");
            botonCrear.RegisterCallback<ClickEvent>(NuevaTarjeta);
            botonGuardar.RegisterCallback<ClickEvent>(GuardarDatos);

            tarjetaTemplate = Resources.Load<VisualTreeAsset>("Lab6/tarjeta");

            contenedorTarjetas.RegisterCallback<ClickEvent>(seleccionTarjeta);

            //CrearDatos();
            LeerDatos();
            CrearTarjetas();
        }

        private void LeerDatos()
        {
            StreamReader sr = new StreamReader("datos-lab5.json");
            string json = sr.ReadToEnd();
            individuos = JsonHelperIndividuo.FromJson<Individuo>(json);
            sr.Close();
        }

        private void GuardarDatos(ClickEvent evt)
        {
            string listaToJSON = JsonHelperIndividuo.ToJson(individuos, true);
            StreamWriter sw = new StreamWriter("datos-lab5.json");
            sw.Write(listaToJSON);
            sw.Close();
        }

        private void NuevaTarjeta(ClickEvent evt)
        {
            if (toggleModificar.value)
            {
                VisualElement nuevaTarjeta = tarjetaTemplate.Instantiate();

                contenedorTarjetas.Add(nuevaTarjeta);

                tarjetas_borde_negro();
                tarjeta_borde_blanco(nuevaTarjeta);

                Individuo individuo = new Individuo(input_nombre.value, input_apellido.value, "Lab6/img1");
                individuos.Add(individuo);
                
                Tarjeta tarjeta = new Tarjeta(nuevaTarjeta.Q<VisualElement>("tarjeta"));
                tarjeta.SetData(individuo);
                selecIndividuo = individuo;
            }
        }

        private void CambioImagen(ClickEvent evt)
        {
            if (toggleModificar.value)
            {
                VisualElement img = evt.target as VisualElement;

                string bg = "Lab6/" + img.resolvedStyle.backgroundImage.sprite.name;
                Debug.Log(bg);
                Debug.Log(selecIndividuo);

                if (selecIndividuo != null)
                    selecIndividuo.Imagen = bg;
            }
        }

        private void CambioApellido(ChangeEvent<string> evt)
        {
            if (toggleModificar.value)
            {
                if (selecIndividuo != null)
                    selecIndividuo.Apellido = evt.newValue;
            }
        }

        private void CambioNombre(ChangeEvent<string> evt)
        {
            if (toggleModificar.value)
            {
                if (selecIndividuo != null)
                    selecIndividuo.Nombre = evt.newValue;
            }
        }

        private void seleccionTarjeta(ClickEvent evt)
        {
            VisualElement tarjeta = evt.target as VisualElement;

            selecIndividuo = tarjeta.userData as Individuo;

            tarjetas_borde_negro();
            tarjeta_borde_blanco(tarjeta);

            if (selecIndividuo != null)
            {
                input_nombre.SetValueWithoutNotify(selecIndividuo.Nombre);
                input_apellido.SetValueWithoutNotify(selecIndividuo.Apellido);
                toggleModificar.value = true;
            }
        }

        void CrearDatos()
        {
            individuos.Add(new Individuo("Ana", "Ríos", "Lab6/img1"));
            individuos.Add(new Individuo("Luis", "Calvete", "Lab6/img2"));
            individuos.Add(new Individuo("Marta", "Martínez", "Lab6/img3"));
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

        void tarjetas_borde_negro()
        {
            List<VisualElement> lista_tarjetas = contenedorTarjetas.Children().ToList();
            lista_tarjetas.ForEach(elem =>
            {
                VisualElement tarjeta = elem.Q<VisualElement>("tarjeta");

                tarjeta.style.borderBottomColor = Color.black;
                tarjeta.style.borderRightColor = Color.black;
                tarjeta.style.borderLeftColor = Color.black;
                tarjeta.style.borderTopColor = Color.black;
            });
        }

        void tarjeta_borde_blanco(VisualElement tar)
        {
            VisualElement tarjeta = tar.Q<VisualElement>("tarjeta");

            tarjeta.style.borderBottomColor = Color.white;
            tarjeta.style.borderRightColor = Color.white;
            tarjeta.style.borderLeftColor = Color.white;
            tarjeta.style.borderTopColor = Color.white;
        }
    }

}