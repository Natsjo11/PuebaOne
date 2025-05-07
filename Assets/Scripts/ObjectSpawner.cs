using System.Collections.Generic;
using UnityEngine;

// Este script genera objetos (esferas) en posiciones aleatorias dentro de un área.
// También se encarga de que no se generen encima unos de otros.
// Además, asigna un color aleatorio a cada objeto al generarse.
public class ObjectSpawner : MonoBehaviour
{
    public GameObject objetoPrefab; // Prefab que vamos a instanciar (esfera)
    public float intervalo = 2f;    // Cada cuántos segundos se intenta generar una esfera
    public int maxObjetos = 10;     // Número máximo de esferas activas en escena
    public float radioChequeo = 1.2f; // Distancia mínima entre esferas (para que no se toquen)
    public float rangoGeneracion = 4f; // Área visible de generación (más pequeño para que no salgan fuera de cámara)

    private float tiempoSiguiente = 0f; // Controla el tiempo entre instancias
    private List<GameObject> objetosActivos = new List<GameObject>(); // Lista de esferas activas

    void Update()
    {
        // Aumenta el tiempo desde la última creación
        tiempoSiguiente += Time.deltaTime;

        // Si pasó el intervalo y aún no hay 10 esferas, intenta crear una nueva
        if (tiempoSiguiente >= intervalo && objetosActivos.Count < maxObjetos)
        {
            tiempoSiguiente = 0f;
            GenerarObjeto();
        }

        // Limpia la lista de esferas destruidas (eliminadas con clic)
        objetosActivos.RemoveAll(obj => obj == null);
    }

    void GenerarObjeto()
    {
        int intentosMaximos = 20;

        for (int i = 0; i < intentosMaximos; i++)
        {
            // Calcula una posición aleatoria dentro del área visible
            Vector3 posicion = new Vector3(
                Random.Range(-rangoGeneracion, rangoGeneracion),
                0.5f,
                Random.Range(-rangoGeneracion, rangoGeneracion)
            );

            // Revisa si ya hay otra esfera muy cerca
            if (!Physics.CheckSphere(posicion, radioChequeo))
            {
                // Crea la nueva esfera
                GameObject nuevo = Instantiate(objetoPrefab, posicion, Quaternion.identity);
                objetosActivos.Add(nuevo);

                // BONUS: Le pone un color aleatorio
                Renderer renderer = nuevo.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Random.ColorHSV(); // Color aleatorio bonito
                }

                // Actualiza el contador en la UI
                UIManager.Instance.ActualizarContador(objetosActivos.Count);
                break;
            }
        }
    }

    // Esta función la llaman las esferas cuando son destruidas
    public void NotificarDestruccion()
    {
        objetosActivos.RemoveAll(obj => obj == null);
        UIManager.Instance.ActualizarContador(objetosActivos.Count);
    }
}