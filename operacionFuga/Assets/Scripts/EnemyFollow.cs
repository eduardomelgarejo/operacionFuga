using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador (asignar en el Inspector)
    private NavMeshAgent agente;
    private float temporizador = 0f;
    public float intervalo = 1f;
    private int colisionesConJugador = 0;
    private float velocidadOriginal;
    private bool velocidadReducida = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        velocidadOriginal = agente.speed;
    }

    void Update()
    {
        if (PlayerMovement.jugadorCapturado || PlayerMovement.jugadorDerrotado)
        {
            agente.isStopped = true;
            return;
        }

        temporizador += Time.deltaTime;
        if (temporizador >= intervalo)
        {
            agente.SetDestination(jugador.position);
            temporizador = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colisionesConJugador++;

            if (colisionesConJugador == 1)
            {
                StartCoroutine(ReducirVelocidadTemporal());
            }
            else if (colisionesConJugador == 2)
            {
                // El jugador pierde sin necesidad de GameManager
                PlayerMovement.jugadorDerrotado = true;
                Destroy(gameObject);
                Debug.Log("¡El jugador ha perdido la partida!");
                return;
            }

            agente.isStopped = true;
            PlayerMovement.jugadorCapturado = true;

            PlayerMovement movimiento = collision.gameObject.GetComponent<PlayerMovement>();
            if (movimiento != null)
            {
                movimiento.enabled = false;
            }

            Debug.Log("¡Jugador capturado!");
        }
    }

    IEnumerator ReducirVelocidadTemporal()
    {
        if (!velocidadReducida)
        {
            velocidadReducida = true;
            agente.speed = velocidadOriginal * 0.5f;
            yield return new WaitForSeconds(3f);
            agente.speed = velocidadOriginal;
            velocidadReducida = false;
        }
    }
}
