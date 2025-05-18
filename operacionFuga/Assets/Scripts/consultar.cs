using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class consultar : MonoBehaviour
{
    public Data data;
    void Start()
    {
        data = FindObjectOfType<Data>();

        Debug.Log(PlayerPrefs.GetString("val_nombre"));
        Debug.Log(PlayerPrefs.GetString("val_dificultad"));
        Debug.Log(PlayerPrefs.GetFloat("val_volumen"));
    }
}
