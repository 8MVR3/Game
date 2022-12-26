using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet; //ссылка на пулю
    public Transform firePoint; //появление пули
    public float fireForce; // сила выстрела

    public void Fire() //выстрел
    {
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation); //пропись выстрела
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }


}
