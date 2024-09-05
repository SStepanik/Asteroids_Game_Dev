using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bullet_speed = 500.0f;
    public float bullet_max_lifetime = 10.0f;
    private Rigidbody2D rigid_body;

    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        rigid_body.AddForce(direction * this.bullet_speed);

        Destroy(this.gameObject, this.bullet_max_lifetime);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
