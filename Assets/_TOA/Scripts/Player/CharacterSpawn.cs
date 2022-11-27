using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    public GameObject[] spawnPrefab;
    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("SelectedCharacter"))
        {
            Debug.LogError("No character selected !!!");
            return;
        }
        spawnPrefab[PlayerPrefs.GetInt("SelectedCharacter")].SetActive(true);
    }
}
