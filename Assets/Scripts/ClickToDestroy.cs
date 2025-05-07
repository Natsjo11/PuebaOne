using UnityEngine;

public class ClickToDestroy : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Esta función se ejecuta automáticamente cuando hago clic en el objeto (si tiene collider)
        
        Destroy(gameObject); // Me destruyo a mí mismo (es decir, destruye la esfera)

        // Busco el script que gestiona los spawns y le aviso que uno fue destruido
        ObjectSpawner spawner = FindAnyObjectByType<ObjectSpawner>(); // (Nuevo método recomendado por Unity)

        if (spawner != null)
        {
            spawner.NotificarDestruccion(); // Le aviso que destruí una esfera, así actualiza el contador
        }
    }
}