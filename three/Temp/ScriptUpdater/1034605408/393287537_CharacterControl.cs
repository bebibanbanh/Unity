using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float speed;
    public sphereSpawner SphereSpawnerA;
    public Renderer renderer1;
    public float waitTime = 1f;
    public bool game;
    public int score;

    private void Start()
    {
        game = true;
        speed = 30f;
        renderer1 = GetComponent<Renderer>();

        ChangeColor();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        transform.position += new Vector3(x, 0, z) * Time.deltaTime * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            Debug.Log("Touch");

            if(collision.gameObject.GetComponent<Renderer>().material.color == renderer1.material.color)
            {
                Game();
            }

            SphereSpawnerA.DestroySpheres();

            StartCoroutine(WaitAndSpawn(waitTime));
            
        }

        IEnumerator WaitAndSpawn(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            SphereSpawnerA.SpawnSpheres();
            ChangeColor();
        }
    }

    public void ChangeColor()
    {
        // Create an array of Color objects containing the three colors
        Color[] colors = { Color.red, Color.blue, Color.yellow };

        // Choose a random color from the array
        Color randomColor = colors[Random.Range(0, colors.Length)];

        // Change the color to the randomly selected color
        renderer1.material.color = randomColor;
    }

    public void Game()
    {
        Debug.Log("like");
    }
}
