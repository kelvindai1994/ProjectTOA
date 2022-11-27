using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;
    [Header("Floating UI")]
    public GameObject floatingTextDump;
    public Transform floatingDamagePfab;

    public Transform floatingHPPfab;
    private void Awake()
    {
        Instance = this;
    }
}
