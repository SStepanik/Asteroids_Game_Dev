using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minsize = 0.5f;
    public float maxsize = 4.0f; //1.5f
    public float asteroid_speed = 50.0f;
    public float asteroid_max_lifetime = 30.0f;
    private SpriteRenderer sprite_renderer;
    private Rigidbody2D rigid_body;

    private void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        rigid_body = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer.sprite = sprites[Random.Range(0,sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = new Vector3(this.size, this.size, this.size);

        rigid_body.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigid_body.AddForce(direction * this.asteroid_speed);

        Destroy(this.gameObject, this.asteroid_max_lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet") {
            if ((this.size * 0.5f) >= this.minsize) {
                CreateSplit();
                CreateSplit();
            }

            Destroy(this.gameObject);
        }
    }

    private void CreateSplit() 
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.asteroid_speed);
    }
}
