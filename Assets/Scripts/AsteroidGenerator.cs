using UnityEngine;
using System.Collections;

[System.Serializable]
public class Asteroids
{
    public GameObject a1;
    public GameObject a2;
    public GameObject a3;

    private static int pos = 1;
    /*public Asteroids()
    {
        a1.AddComponent<Rigidbody>();
        a2.AddComponent<Rigidbody>();
        a3.AddComponent<Rigidbody>();
        a1.AddComponent<AsteroidController>();
        a2.AddComponent<AsteroidController>();
        a3.AddComponent<AsteroidController>();

    }*/
    public GameObject GetGameObject(Vector3 playerPosition)
    {
        GameObject result = a1;
        if (++pos > 3)
        {
            pos = 1;
        }
        switch (pos)
        {
            case 1:
                result = a1;
                break;
            case 2:
                result = a2;
                break;
            case 3:
                result = a3;
                break;
        }
        result.transform.position = new Vector3(3, 5, 2);
        return result;
    }
}
public class AsteroidGenerator : MonoBehaviour {

    public Asteroids asteroids;
    private GameObject asteroid;
    // Use this for initialization
    void Start () {
        asteroid = asteroids.GetGameObject(transform.position);
        asteroid.AddComponent<Rigidbody>();
        asteroid.AddComponent<AsteroidController>();
    }

    // Update is called once per frame
    void Update () {
    }
}

