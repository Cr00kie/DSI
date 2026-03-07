using UnityEngine;
using UnityEngine.UIElements;

public class Tarjeta
{
    public VisualElement Root { get; private set; }

    private Label nombreLabel;
    private Label apellidoLabel;
    private VisualElement imagen;

    private Individuo individuo;

    public Tarjeta(VisualElement root)
    {
        Root = root;

        nombreLabel = Root.Q<Label>("nombreLabel");
        apellidoLabel = Root.Q<Label>("apellidoLabel");
        imagen = Root.Q<VisualElement>("ImagenSuperior");

        Root
            .Query(name: "tarjeta")
            .Descendents<VisualElement>()
            .ForEach(elem => elem.pickingMode = PickingMode.Ignore);

        Root.RegisterCallback<ClickEvent>(OnClick);
    }

    public void SetData(Individuo individuo)
    {
        this.individuo = individuo;

        Root.userData = individuo;

        individuo.OnDataChanged += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (individuo == null) return;

        nombreLabel.text = individuo.Nombre;
        apellidoLabel.text = individuo.Apellido;

        if (individuo.Imagen != null)
        {
            imagen.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(individuo.Imagen)); 
        }
    }

    private void OnClick(ClickEvent evt)
    {
        Debug.Log("Seleccionado: " + individuo.Nombre);
    }
}

