using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace lab6
{
    [Serializable]
    public class Individuo
    {
        public event Action OnDataChanged;

        [SerializeField] private string nombre;
        [SerializeField] private string apellido;
        [SerializeField] private string imagen;

        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                NotifyChange();
            }
        }

        public string Apellido
        {
            get => apellido;
            set
            {
                apellido = value;
                NotifyChange();
            }
        }

        public string Imagen
        {
            get => imagen;
            set
            {
                imagen = value;
                NotifyChange();
            }
        }

        public Individuo(string nombre, string apellido, string imagen)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.imagen = imagen;
        }

        private void NotifyChange()
        {
            OnDataChanged?.Invoke();
        }
    }

}