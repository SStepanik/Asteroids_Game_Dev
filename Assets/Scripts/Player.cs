using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float thrust_speed = 1.0f;
    public float turn_speed = 1.0f;
    private Rigidbody2D rigid_body;
    private bool thrusting;
    private float turn_direction;

    private void Awake() 
    {
        rigid_body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            turn_direction = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            turn_direction = -1.0f;
        } else {
            turn_direction = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void FixedUpdate() 
    {
        if (thrusting) {
            rigid_body.AddForce(this.transform.up * this.thrust_speed);
        }

        if (turn_direction != 0.0f) {
            rigid_body.AddTorque(this.turn_direction * this.turn_speed);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid") {
            rigid_body.velocity = Vector3.zero;
            rigid_body.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
