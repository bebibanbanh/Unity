using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        score= 0;
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
                score = score + 1;
                Debug.Log(score);
                if (score == 10)
                {
                    SceneManager.LoadScene("Win");
                }
            }
            else if(collision.gameObject.GetComponent<Renderer>().material.color != renderer1.material.color)
            {
                Debug.Log("bruh");
                game = false;
                Debug.Log(score);
                SceneManager.LoadScene("Loss");
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

    void OnGUI()
    {
        GUILayout.Label("SCORE: " + score);
    }

}
