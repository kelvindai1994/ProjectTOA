using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAndSkillBar : MonoBehaviour
{
    [Header("Weapon UI")]
    [SerializeField] private GameObject[] weapons;

    [Header("Skills UI")]
    [SerializeField] private GameObject[] skills;
    private void Start()
    {
        for(int i =0;i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }
        for(int i = 0; i< skills.Length; i++)
        {
            skills[i].SetActive(false);
        }
        if (!PlayerPrefs.HasKey("SelectedCharacter"))
        {
            Debug.LogError("No character selected !!!");
            return;
        }
        weapons[PlayerPrefs.GetInt("SelectedCharacter")].SetActive(true);
        skills[PlayerPrefs.GetInt("SelectedCharacter")].SetActive(true);         
    }
}
