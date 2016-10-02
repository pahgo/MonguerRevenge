using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Boundaries
{
    public float xMin, xMax, zMin, zMax;
}
public class Controller : MonoBehaviour {

    public GameObject player;
    public Boundaries bounds;
    public float tilt;
    public GameObject shieldsGameObject;
    public GameObject pointsGameObject;
    public GameObject invulnerability;
    public GameObject sphere;

    private Rigidbody body;
    private TextMesh shieldsText;
    private int maxZSpeed = 5;
    private int lives = 3;
    private Vector3 velocity;
    private float invulnerabilityTime = 0;
    private bool invulnerable = false;
    private float off = 0f;
    private bool growing = true;
    private long points;
    // Use this for initialization
    void Start () {
        body = player.GetComponent<Rigidbody>();
        shieldsText = shieldsGameObject.GetComponent<TextMesh>();
        sphere.GetComponent<Renderer>().enabled = false;
        points = 0;
    }

    // Update is called once per frame
    void FixedUpdate () {
        GetVelocity();
        body.velocity = velocity;
        body.position = new Vector3(Mathf.Clamp(body.position.x, bounds.xMin, bounds.xMax), body.position.y, Mathf.Clamp(body.position.z, bounds.zMin, bounds.zMax));
        //body.rotation = Quaternion.Euler(0, 0, body.velocity.z * tilt);
        bool noRotate = body.position.z == bounds.zMax || body.position.z == bounds.zMin;
        body.rotation = Quaternion.Euler(0,270, -body.velocity.z * tilt * (noRotate ? 0 : 1) * Time.deltaTime);
        Invulnerability();
    }
    void OnTriggerEnter(Collider other)
    {
        if(!invulnerable)
        {
            if (--lives <= 0)
            {
                // Debug.Log("Fin del juego");
            }
            shieldsText.text = "Shields: " + lives.ToString();
            invulnerabilityTime = 3.5f;
            invulnerable = true;
        }
    }
    private void GetVelocity()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidControl();
        }
        else
        {
            PcControl();
        }
    }
    float timeScale;
    private void AndroidControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        if (Input.touchCount > 0)
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = timeScale;
            }
            Boost();
        }
        else
        {
            UnBoost();
        }
    }
    private void PcControl()
    {
        if (Input.GetKey(KeyCode.P))
        {
            timeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = timeScale;
            }
                Boost();
        }
        else
        {
            UnBoost();
        }
    }
    private void Boost()
    {
        velocity.z = Mathf.Min(velocity.z + 1, maxZSpeed);
        velocity.x = Mathf.Min(velocity.x + 1, 2);
        IncreasePoints(3);
    }
    private void UnBoost()
    {
        velocity.z = Mathf.Max(velocity.z - 1, -maxZSpeed);
        velocity.x = Mathf.Max(velocity.x - 1, -2);
        IncreasePoints(1);
    }
    private void Invulnerability()
    {
        //invulnerability.GetComponent<Rigidbody>().transform.Rotate(new Vector3(360 * Time.deltaTime, 0, 0));//chuli chuli
        if (invulnerable)
        {
            sphere.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector3(off, off, off));
            ChangeInvulnerabilityTexture();
            invulnerabilityTime = invulnerabilityTime - Time.deltaTime;
            invulnerability.GetComponent<TextMesh>().text = "Invulnerable: " + (int) (invulnerabilityTime) + "'";
            if (invulnerabilityTime > 3)
            {
                sphere.GetComponent<Renderer>().enabled = true;
                invulnerability.GetComponent<Renderer>().enabled = true;
            }
            else if (invulnerabilityTime <= 0)
            {
                invulnerability.GetComponent<Renderer>().enabled = false;
                invulnerable = false;
                sphere.GetComponent<Renderer>().enabled = false;
            }
        }
        else
        {
            invulnerability.GetComponent<Renderer>().enabled = false;
        }
    }
    private void ChangeInvulnerabilityTexture()
    {
        if (growing)
        {
            off = off + 0.01f;
            if (off >= 0.5f)
                growing = false;
        }
        else
        {
            off = off - 0.01f;
            if (off <= 0)
                growing = true;

        }
    }
    public Boundaries GetBoundaries()
    {
        return bounds;
    }
    public void IncreasePoints(int points)
    {
        this.points += points;
        pointsGameObject.GetComponent<TextMesh>().text = "Points: " + this.points.ToString();
    }
    public bool isInvulnerable()
    {
        return invulnerable;
    }
}
