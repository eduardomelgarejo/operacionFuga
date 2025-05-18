using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class metaFinal : MonoBehaviour
{
    public GameObject panelVictoria;
    public TMP_Text textoNombre;
    public TMP_Text textoTiempo;
    private float tiempoInicio;

    void Start()
    {
        tiempoInicio = Time.time;
        panelVictoria.SetActive(false); // Oculta el panel al inicio
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float tiempoFinal = Time.time - tiempoInicio;

            string nombre = PlayerPrefs.GetString("val_nombre", "Jugador");
            textoNombre.text = "Nombre: " + nombre;
            textoTiempo.text = "Tiempo: " + tiempoFinal.ToString("F2") + "s";

            panelVictoria.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
    }
}
