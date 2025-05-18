using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consultar : MonoBehaviour
{
    public Data data;
    void Start()
    {
        data = FindObjectOfType<Data>();

        string nombreGuardado = PlayerPrefs.GetString("val_nombre", "").Trim();
        if (string.IsNullOrEmpty(nombreGuardado))
        {
            nombreGuardado = "Player";
            PlayerPrefs.SetString("val_nombre", nombreGuardado);
        }

        Debug.Log(PlayerPrefs.GetString("val_nombre"));
        Debug.Log(PlayerPrefs.GetString("val_dificultad", "Facil"));
        Debug.Log(PlayerPrefs.GetFloat("val_volumen"));
    }
}
