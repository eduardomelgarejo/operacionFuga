using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyFollow : MonoBehaviour
{
    public Transform jugador; // Asignar al Player
    public GameObject panelDerrota; // Asignar el panel para derrota por kamikaze
    public GameObject panelDerrotaTiempo; // Asignar el panel para derrota por tiempo
    private NavMeshAgent agente;
    public GameObject explosionPrefab;
    public GameObject impactoPrefab;

    private float temporizador = 0f;
    private float tiempoParaDerrota = 15f;
    public float intervalo = 1f;

    private int colisionesConJugador = 0;
    private float velocidadOriginal;
    private bool panelMostrado = false;
    private bool velocidadReducida = false;
    private controladorAudio audioControlador;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        
        PlayerMovement.jugadorCapturado = false;
        PlayerMovement.jugadorDerrotado = false;

        if (!PlayerPrefs.HasKey("val_dificultad"))
        {
            PlayerPrefs.SetString("val_dificultad", "Facil");
            PlayerPrefs.Save();
        }

        // Obtener dificultad desde PlayerPrefs
        string dificultad = PlayerPrefs.GetString("val_dificultad", "Facil");

        float multiplicador = 1f;
        switch (dificultad)
        {
            case "Facil":
                multiplicador = 1f;
                break;
            case "Media":
                multiplicador = 2f;
                break;
            case "Dificil":
                multiplicador = 3f;
                break;
        }

        float velocidadBase = 3f;
        velocidadOriginal = velocidadBase * multiplicador;
        agente.speed = velocidadOriginal;
        agente.isStopped = false;
        colisionesConJugador = 0;
        panelDerrota.SetActive(false);
        panelDerrotaTiempo.SetActive(false);
        panelMostrado = false;        
        velocidadReducida = false;

        audioControlador = GameObject.Find("Controlador").GetComponent<controladorAudio>();
    }

    void Update()
    {
        if (PlayerMovement.jugadorCapturado || PlayerMovement.jugadorDerrotado)
        {
            agente.isStopped = true;
            return;
        }

        temporizador += Time.deltaTime;

        if (temporizador >= tiempoParaDerrota && !panelMostrado)
        {
            panelMostrado = true;
            panelDerrotaTiempo.SetActive(true);
            agente.isStopped = true;
            // Cambiar música a "Perdiste"
            audioControlador.loadClip(1);

        }

        if (jugador != null)
            agente.SetDestination(jugador.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (panelMostrado) return;

            colisionesConJugador++;

            if (colisionesConJugador == 1)
            {
                // Partículas de impacto
                ContactPoint contact = collision.contacts[0];
                Instantiate(impactoPrefab, contact.point, Quaternion.identity);

                StartCoroutine(ReducirVelocidadTemporal());
            }
            else if (colisionesConJugador == 2)
            {
                panelMostrado = true;
                PlayerMovement.jugadorDerrotado = true;

                Instantiate(explosionPrefab, jugador.position, Quaternion.identity);
                audioControlador.loadClip(3);  // sonido explosión

                StartCoroutine(MostrarDerrota(0.5f));
            }
        }
    }
    IEnumerator ReducirVelocidadTemporal()
    {
        if (!velocidadReducida)
        {
            agente.speed = velocidadOriginal * 0.5f;
            yield return new WaitForSeconds(3f);
            agente.speed = velocidadOriginal;
        }
    }
    IEnumerator MostrarDerrota(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        audioControlador.loadClip(1);  // música derrota
        panelDerrota.SetActive(true);
        Time.timeScale = 0f;

        // Ahora sí desactivamos el enemigo, para que no interfiera después
        this.gameObject.SetActive(false);
    }
}
