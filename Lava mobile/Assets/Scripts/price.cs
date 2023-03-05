using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class price : MonoBehaviour
{
    private TextMeshProUGUI price_text;
    public int _price;
        void Start()
    {
        price_text = transform.Find("price text").GetComponent<TextMeshProUGUI>();
        price_text.text = _price.ToString();
    }

    void Update()
    {
        
    }
}
