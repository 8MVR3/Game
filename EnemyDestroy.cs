using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyDestroy : MonoBehaviour
{
    public Rigidbody2D rb; //подключение RB
    public float moveSpeed; //установка скорости
    public GameObject Enemy;
    //public TextMeshProUGUI countText;
    public int HP;
    public int count;

    public TextMeshProUGUI HPText;

    private Vector2 moveDirection;


    void Start()
    {
        HPText.text = "HP: " + HP.ToString();
    }

    void Update()
    {
        System.Random rnd = new System.Random();
        float value = rnd.Next()%100;
        if (value < 1)
        {
            ProcessInputs();
        }
       
    }

    void FixedUpdate()
    {
        // Физические вычисления
        Moving();
    }

    void ProcessInputs()
    {
        float moveX = Random.Range(-1.0f, 1.0f); //Движение по Y 
        float moveY = Random.Range(-1.0f, 1.0f); //Движение по X
        moveDirection = new Vector2(moveX, moveY).normalized; //Стабильное движение по диагонали
    }

        public void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                break;
            case "Bullet":
                HP--;
                
                //Instantiate(HPText, gameObject.transform.position = Enemy.position);
                if (HP == 0)
                {
                    gameObject.SetActive(false);
                    Instantiate(Enemy, gameObject.transform.position = new Vector2(Random.Range(-60.0f, 60.0f), Random.Range(-45.0f, 45.0f)), Quaternion.identity);
                    Invoke("SpawnEnemy", 0.2f);
                    HP = count;
                }
                HPText.text = "HP: " + HP.ToString();
                break;
        }
    }

    public void SpawnEnemy()
    {
        gameObject.SetActive(true);
        
    }

    void Moving()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed); //Движение помноженное на скорость
    }


}