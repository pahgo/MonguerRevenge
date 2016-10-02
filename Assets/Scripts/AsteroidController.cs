using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

    public GameObject player;
    private Vector3 rotation;
    private Rigidbody body;
    private Boundaries bounds;
    private float ownSpeed;
    private static ArrayList zetas;
	// Use this for initialization
	void Start () {
        rotation = new Vector3(Random.value - 0.5f, Random.value -0.5f, Random.value - 0.5f) * 360;
        body = GetComponent<Rigidbody>();
        bounds = player.GetComponent<Controller>().GetBoundaries();
        ownSpeed = Random.Range(3, 5);
        zetas = new ArrayList();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.Rotate(rotation * Time.deltaTime);
        body.velocity = new Vector3(-(player.GetComponent<Rigidbody>().velocity.x + ownSpeed), 0, 0);
        if (0.2 + transform.position.x < -3.2)
        {
            Respawn();
            player.GetComponent<Controller>().IncreasePoints(250);
        }
        else if (transform.position.x < 1)
        {
            zetas.Remove(Mathf.Round(transform.position.z));
        }
    }

    private void Respawn()
    {
        float z;
        do
        {
            if (Random.value <= 0.5)
            {
                z = player.transform.position.z + 0.5f;
            }
            else
            {
                z = Random.Range(bounds.zMin, bounds.zMax);
            }
        } while (zetas.Contains(Mathf.Round(z)));

        transform.position = new Vector3(8, 5, z);
        zetas.Add(Mathf.Round(z));
        ownSpeed = Random.Range(3, 5);
        rotation = new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f) * 360;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Viper") && !player.GetComponent<Controller>().isInvulnerable())
        {
            Respawn();
        }
    }
}
