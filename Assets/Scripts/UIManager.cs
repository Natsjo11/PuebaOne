using UnityEngine;
using TMPro; // Importante para usar TMP_Text

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TMP_Text textoContador; // <-- Usa TMP_Text si es TextMeshPro

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ActualizarContador(int cantidad)
    {
        if (textoContador != null)
        {
            textoContador.text = "Objetos activos: " + cantidad;
        }
    }
}