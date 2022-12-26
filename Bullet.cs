using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb; //подключение RB
    public GameObject impactEffect;


    public void OnTriggerEnter2D(Collider2D other) //обработка столкновений
    {
        switch(other.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                Destroy(gameObject);
                break;
        }    
    }

}
