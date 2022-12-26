using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera; //������
    public float moveSpeed; //��������� ��������
    public Rigidbody2D rb; //����������� RB
    public Weapon weapon; //������ �� �������� ������
    public TextMeshProUGUI AmmoCountText;
    public TextMeshProUGUI CurAmmoText;
    public TextMeshProUGUI ReloadTimerText;

    public int AmmoCount; // �������� � �������
    public int Ammo; // ���-�� �������� � 1�� ������
    public int CurAmmo; // ���-�� ��������
    public float ReloadSpeed; // �������� ����������� 
    public float ShootSpeed; // ����������������
    public float ReloadTimer = 0.0f; // ����������� ����� �����������
    public float ShootTimer = 0.0f; // ����������� ����� ��������


    private Vector2 moveDirection; //��� ��������
    private Vector2 mousePosition; //��� �������� �����

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
        // ���������� ����������
        Move();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); //�������� �� X
        float moveY = Input.GetAxisRaw("Vertical"); //�������� �� Y
        if(Input.GetMouseButton(0) & CurAmmo > 0 & ReloadTimer <= 0 & ShootTimer <= 0)//������� �� ����� ������ ����
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
        moveDirection = new Vector2(moveX, moveY).normalized; //���������� �������� �� ���������
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition); //��� �������� � ����
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed); //�������� ����������� �� ��������
        Vector2 aimDirection = mousePosition - rb.position; //������� ������ ��� �������� �� �����
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f; //������� ��� ���� ������������
        rb.rotation = aimAngle;
    }
}
