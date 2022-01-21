using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    
    [SerializeField] private Slider sliderReference;
    [SerializeField] private String units;

    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        ChangeValue();
    }

    public void ChangeValue()
    {
        _text.text = sliderReference.value.ToString("F1") + " " + units;
    }
}
