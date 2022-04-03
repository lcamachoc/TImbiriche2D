using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sizetext;
    public void SetSize()
    {
        sizetext.text = "BOARD SIZE: " + slider.value;
        PlayerPrefs.SetInt("size", (int)slider.value);
    }
}
