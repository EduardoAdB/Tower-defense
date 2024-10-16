using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScript : MonoBehaviour
{
    public static HomeScript main;

    public Transform[] path;
    public Transform startPoint;

    private void Awake()
    {
        main = this;
    }
}
