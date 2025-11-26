using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpandableMenuItem : MonoBehaviour {

    [HideInInspector] public Image img;
    [HideInInspector] public RectTransform offset;
    private void Awake() {
        img = GetComponent<Image>();
        offset = GetComponent<RectTransform>();
    }
}