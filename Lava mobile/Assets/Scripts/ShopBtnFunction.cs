﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopBtnFunction : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameManager.Instance.OpenShop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
