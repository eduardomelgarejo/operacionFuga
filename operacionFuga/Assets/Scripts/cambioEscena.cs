using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cambioEscena : MonoBehaviour
{
    public void siguienteEscena()
    {
        SceneManager.LoadScene("Juego");
    }
    public void volverAlMenu()
    {
        Time.timeScale = 1f;        
        SceneManager.LoadScene("Menu");
    }
    public void reiniciarJuego()
    {
        Time.timeScale = 1f;        
        SceneManager.LoadScene("Juego"); 
    }   
}
