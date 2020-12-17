using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Texture2D texture;
    public float x;
    public float y;
    private void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2 - x / 2, Screen.height / 2 - y / 2, x, y);
        GUI.DrawTexture(rect, texture);
    }
}
