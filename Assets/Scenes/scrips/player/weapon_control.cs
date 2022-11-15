using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_control : MonoBehaviour
{
    public Transform firepoint;
    public GameObject stairPrefeb;
    public GameObject bulletPrefeb;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2")&&player_controler.stair_amount>0)
        {
            Shoot_stair();
            player_controler.stair_amount--;
        }
        if(Input.GetButtonDown("Fire1")&&player_controler.ammo_amount>0)
        {
            Shoot_ammo();
            player_controler.ammo_amount--;
        }
    }

    void Shoot_stair()
    {
        Instantiate(stairPrefeb,firepoint.position,firepoint.rotation);
    }
    void Shoot_ammo()
    {
        Instantiate(bulletPrefeb,firepoint.position,firepoint.rotation);
    }
}
