    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponHandler : MonoBehaviour
{
    public enum Action
    {
        Equip,
        UneEquip
    }
    public Transform Weapon;
    public Transform WeaponHandle;
    public Transform WeaponResetPose;
    public GameObject playerCrossHair;

    public CinemachineFreeLook subCam_1;
    public CinemachineFreeLook subCam_2;


    #region UnityFunction
    private void Start()
    { 
        subCam_1.enabled = true;
        subCam_2.enabled = false;
        if(playerCrossHair == null)
        {
            playerCrossHair = GameObject.FindGameObjectWithTag("Player_Crosshair");
        }

        playerCrossHair.SetActive(false);

    }
    private void OnDisable()
    {
        if (playerCrossHair != null)
        {
            playerCrossHair.SetActive(true);
        } 
    }
    #endregion

    #region PublicFunction

    public void ResetWeapon(Action action)
    {
        if(action == Action.Equip)
        {
            AudioManager.Instance.PlayAudio(AudioType.SFX_Player_Bow_Equip, false, PlayerPrefs.GetFloat(CONSTANT.PP_EFFECT_VOLUME) / 4f, 0.25f);
            Weapon.SetParent(WeaponHandle);
            subCam_1.enabled = false;
            subCam_2.enabled = true;
            playerCrossHair.SetActive(true);
        }
        else
        {
            Weapon.SetParent(WeaponResetPose);
            subCam_1.enabled = true;
            subCam_2.enabled = false;
            playerCrossHair.SetActive(false);
        }

        Weapon.localRotation = Quaternion.identity;
        Weapon.localPosition = Vector3.zero;
    }

    #endregion
}
