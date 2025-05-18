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


    private controladorAudio audioControlador;

    void Start()
    {
        tiempoInicio = Time.time;
        panelVictoria.SetActive(false);
        audioControlador = GameObject.Find("Controlador").GetComponent<controladorAudio>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float tiempoFinal = Time.time - tiempoInicio;

            string nombre = PlayerPrefs.GetString("val_nombre", "Jugador");
            textoNombre.text = "Jugador: " + nombre;
            textoTiempo.text = "Tiempo: " + tiempoFinal.ToString("F2") + "s";

            panelVictoria.SetActive(true);
            Time.timeScale = 0f;

            // Reproducir música de victoria
            audioControlador.loadClip(2);
        }
    }
}
