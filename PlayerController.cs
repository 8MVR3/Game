using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera; //камера
    public float moveSpeed; //установка скорости
    public Rigidbody2D rb; //подключение RB
    public Weapon weapon; //ссылка на сценарий оружия
    public TextMeshProUGUI AmmoCountText;
    public TextMeshProUGUI CurAmmoText;
    public TextMeshProUGUI ReloadTimerText;

    public int AmmoCount; // Патронов в обоймах
    public int Ammo; // Кол-во патронов в 1ой обойме
    public int CurAmmo; // Кол-во патронов
    public float ReloadSpeed; // Скорость перезарядки 
    public float ShootSpeed; // Скорострельность
    public float ReloadTimer = 0.0f; // Стандартное время перезарядки
    public float ShootTimer = 0.0f; // Стандартное время выстрела


    private Vector2 moveDirection; //для движения
    private Vector2 mousePosition; //для поворота мышью

    void Start()
    {
        AmmoCountText.text = AmmoCount.ToString();
        CurAmmoText.text = CurAmmo.ToString();
        //ReloadTimerText.text = "Reload: " + ReloadTimer.ToString() + " sec.";
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }


    void FixedUpdate()
    {
        // Физические вычисления
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); //Движение по X
        float moveY = Input.GetAxisRaw("Vertical"); //Движение по Y
        if(Input.GetMouseButton(0) & CurAmmo > 0 & ReloadTimer <= 0 & ShootTimer <= 0)//выстрел на левую кнопку мыши
        {
            weapon.Fire();
            CurAmmo = CurAmmo - 1;
            ShootTimer = ShootSpeed;
        }
        if (ShootTimer > 0)
        {
            ShootTimer -= Time.deltaTime;
        }
        if (CurAmmo == 0 & AmmoCount > 0)
        {
            ReloadTimer = ReloadSpeed;
            CurAmmo = Ammo;
            AmmoCount -= CurAmmo;
        }
        if (ReloadTimer > 0)
        {
            ReloadTimer -= Time.deltaTime;
            ReloadTimerText.text = "Reload: " + ReloadTimer.ToString("0.00") + " sec.";
        }
        else
        {
            ReloadTimerText.text = " ";
        }
        AmmoCountText.text = "Ammo: " + AmmoCount.ToString();
        CurAmmoText.text = "CurAmmo:" + CurAmmo.ToString();
        moveDirection = new Vector2(moveX, moveY).normalized; //Стабильное движение по диагонали
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition); //Для поворота к мыши
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed); //Движение помноженное на скорость
        Vector2 aimDirection = mousePosition - rb.position; //Поворот игрока для слежения за мышью
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f; //Формула для угла прицеливания
        rb.rotation = aimAngle;
    }
}
