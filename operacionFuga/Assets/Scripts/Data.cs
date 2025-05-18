using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data : MonoBehaviour
{
    public TMPro.TMP_InputField input;    
    public TMPro.TMP_Dropdown dropdown;
    public Slider slider;
    public AudioSource audioSource;

    private void Start()
    {
        setAudioInicial();
    }

    private void Awake()
    {
       
    }

    public void getinput()
    {
        string name = input.text;
        Debug.Log("El nombre es: " + name);
        PlayerPrefs.SetString("val_nombre", name);
    }

    public void getDropDown()
    {
        int index = dropdown.value;
        TMPro.TMP_Dropdown.OptionData seleccion = dropdown.options[index];
        string select = seleccion.text;
        Debug.Log("Dificultad escogida: " + select);
        
        PlayerPrefs.SetString("val_dificultad", select);
    }

    public void setAudioInicial()
    {
        float volumen = 0.3f;
        slider.value = volumen;
        audioSource.volume = volumen;
    }
    public void setAudio()
    {
        float volumen;
        volumen = slider.value;
        audioSource.volume = volumen;

        PlayerPrefs.SetFloat("val_volumen", volumen);
    }
}
