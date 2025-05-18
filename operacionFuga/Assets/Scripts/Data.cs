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
        RestaurarPreferencias();
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
        string select = dropdown.options[index].text;
        Debug.Log("Dificultad escogida: " + select);
        PlayerPrefs.SetString("val_dificultad", select);
    }

    public void setAudio()
    {
        float volumen = slider.value;
        audioSource.volume = volumen;
        PlayerPrefs.SetFloat("val_volumen", volumen);
    }

    public void RestaurarPreferencias()
    {
        // Nombre
        if (PlayerPrefs.HasKey("val_nombre"))
        {
            input.text = PlayerPrefs.GetString("val_nombre");
        }

        // Dificultad
        if (PlayerPrefs.HasKey("val_dificultad"))
        {
            string dificultad = PlayerPrefs.GetString("val_dificultad");
            for (int i = 0; i < dropdown.options.Count; i++)
            {
                if (dropdown.options[i].text == dificultad)
                {
                    dropdown.value = i;
                    break;
                }
            }
        }

        // Volumen
        if (PlayerPrefs.HasKey("val_volumen"))
        {
            float volumen = PlayerPrefs.GetFloat("val_volumen");
            slider.value = volumen;
            audioSource.volume = volumen;
        }
        else
        {
            setAudioInicial(); 
        }
    }

    public void setAudioInicial()
    {
        float volumen = 0.3f;
        slider.value = volumen;
        audioSource.volume = volumen;
        PlayerPrefs.SetFloat("val_volumen", volumen);
    }
}
