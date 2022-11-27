using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAndSkillBar : MonoBehaviour
{
    [Header("Weapon UI")]
    [SerializeField] private GameObject weaponParent;
    [SerializeField] private List<GameObject> weapons;

    [Header("Skills UI")]
    [SerializeField] private GameObject skillParent;
    [SerializeField] private List<GameObject> skills;



    private void Start()
    {
        for (int i = 0; i < weaponParent.transform.childCount; i++)
        {
            GameObject weapon = weaponParent.transform.GetChild(i).gameObject;
            weapon.SetActive(false);
            weapons.Add(weapon);
        }
        for (int i = 0; i < skillParent.transform.childCount; i++)
        {
            GameObject skill = skillParent.transform.GetChild(i).gameObject;
            skill.SetActive(false);
            skills.Add(skill);
        }

        weapons[PlayerPrefs.GetInt("SelectedCharacter")].SetActive(true);
        skills[PlayerPrefs.GetInt("SelectedCharacter")].SetActive(true);
    }
}
