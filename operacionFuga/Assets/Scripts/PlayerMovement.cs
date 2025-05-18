using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Transform objetivo; // Referencia al objetivo (para el movimiento del jugador)
    public NavMeshAgent agente; // Componente que maneja el movimiento del jugador

    // Variable est�tica para marcar si el jugador esta capturado
    public static bool jugadorCapturado = false;
    public static bool jugadorDerrotado = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>(); // Obtener el componente NavMeshAgent
    }

    void Update()
    {
        if (jugadorCapturado) return; // Si el jugador esta capturado, no se mueve

        // Movimiento del jugador con el click del mouse
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agente.destination = hit.point; // Establecer el destino al lugar donde se hace clic
            }
        }
    }
}