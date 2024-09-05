/*using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;*/
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    public float trajectory_variance = 15.0f;
    public float spawn_rate = 2.0f;
    public float spawn_distance = 15.0f;
    public int spawn_amount = 1;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawn_rate, this.spawn_rate);
    }

    private void Spawn()
    {
        for (int i = 0; i < this.spawn_amount; i++)
        {
            Vector3 spawn_direction = Random.insideUnitCircle.normalized * this.spawn_distance;
            Vector3 spawn_point = this.transform.position + spawn_direction;

            float variance = Random.Range(-this.trajectory_variance, this.trajectory_variance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawn_point, rotation);
            asteroid.size = Random.Range(asteroid.minsize, asteroid.maxsize);
            asteroid.SetTrajectory(rotation * -spawn_direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
