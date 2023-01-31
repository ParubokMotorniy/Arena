using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private GameObject weaponLogic;
        public void EnableWeapon()
        {
            weaponLogic.SetActive(true);
        }
        public void DisableWeapon()
        {
            weaponLogic.SetActive(false);
        }
    }
}


