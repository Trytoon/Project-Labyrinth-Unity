using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class BallMovementTest : MonoBehaviour
{

    public HudManager hud;

    public float speed;
    public float gravity;
    Rigidbody rb;

    Vector3 checkpointCoordinate;


    bool ToggleTeleport = true;

    Vector3 toTeleport;

    Vector3[] SpawnPoints = new Vector3[11];
    int SceneIndex;

    private AudioSource collectibleSound;

    public GameObject troux;


    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        SpawnPoints[1] = new Vector3(-0.3774f, 0.5f, -0.328f); //Lvl 1
        SpawnPoints[2] = new Vector3(-1.255f, 0.5f, 0.775f); //Lvl 2
        SpawnPoints[3] = new Vector3(-0.285f, 0.5f, 0.318f); //Lvl 3
        SpawnPoints[4] = new Vector3(0.162f, 0.5f, -0.367f); //Lvl Animation
        SpawnPoints[5] = new Vector3(-0.407f, 0.5f, 0.462f); //Lvl 4
        SpawnPoints[6] = new Vector3(-0.433f, 0.5f, 0.384f); //Lvl 5
        SpawnPoints[7] = new Vector3(-0.444f, 0.5f, 0.502f); //Lvl 6
        SpawnPoints[8] = new Vector3(-0.293f, 0.5f, 0.561f); //Lvl 7
        SpawnPoints[9] = new Vector3(-1.153f, 0.5f, 0.056f); //Lvl 8
        SpawnPoints[10] = new Vector3(-0.408f, 0.5f, 0.38f); //Lvl 9

        transform.position = SpawnPoints[SceneIndex];
        checkpointCoordinate = transform.position;
        rb = GetComponent<Rigidbody>();

        gravity = -50f;
       Physics.gravity = new Vector3(0, gravity, 0);
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(transform.position);

        if (Application.platform != RuntimePlatform.Android || Application.platform != RuntimePlatform.IPhonePlayer)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            float distance = speed * Time.deltaTime;

            float hInput = Input.GetAxis("Horizontal");
            float vInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(hInput * distance, 0f, vInput * distance);

            Vector3 pos = transform.position;

            Vector3 newPos = pos + movement;

            rb.MovePosition(newPos);

            rb.velocity = rb.velocity * 5;
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 3f);



            if (newPos.y < -1)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                transform.position = checkpointCoordinate;
                GameManager.SetLives(GameManager.GetLives() - 1);
                hud.Refresh();
            }

            rb.MovePosition(newPos);
        }

        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = rb.velocity * 20 * Time.deltaTime;

        if (transform.position.y < -1)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            transform.position = checkpointCoordinate;
            GameManager.SetLives(GameManager.GetLives() - 1);
            hud.Refresh();
        }

        if (transform.position.y > 1)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }


        if (GameManager.GetLives() < 0)
        {
            hud.DisplayMainMenu();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        GameObject other = collision.gameObject;
        ContactPoint contact = collision.contacts[0];

        if (other.CompareTag("Checkpoint"))
        {
            checkpointCoordinate = contact.point;
            checkpointCoordinate.y += 2f;
            Destroy(other);
        }
        else if (other.CompareTag("Coin"))
        {
            Destroy(other);
            GameManager.IncrementScore(1);
            hud.Refresh();
        }
        else if (other.CompareTag("Teleporter") && ToggleTeleport)
        {

            string name = other.name;
            StringBuilder sb = new StringBuilder(name);


            if (name[name.Length - 1] == '1')
            {
                sb[name.Length - 1] = '2';
                name = sb.ToString();
            }
            else
            {
                sb[name.Length - 1] = '1';
                name = sb.ToString();
            }

            toTeleport = GameObject.Find(name).transform.position;
            toTeleport.y += 0.1f;

            this.transform.position = toTeleport;

            ToggleTeleport = false;
            Invoke("ToggleTeleportStatus", 5f);
        } else if(other.CompareTag("Finish"))
        {
           
            Destroy(other);
            if(SceneIndex + 1 == 11)
            {
                SceneManager.LoadScene(0);
            } else
            {
                SceneManager.LoadScene(SceneIndex + 1);
            }
            PlayerPrefs.SetInt("level", SceneIndex + 1);
        }
        else if (other.CompareTag("Trampo")) {
            Destroy(other);
            StartCoroutine(MovePlatformsToHoles());
        }
        else if (other.CompareTag("oneUp")) {
            Destroy(other);
            if (GameManager.GetLives() <= 5)
            {
                GameManager.setLives(GameManager.GetLives() + 1); 
            }
            hud.Refresh();
        }
    }


    public void ToggleTeleportStatus()
    {
        if (ToggleTeleport)
            ToggleTeleport = false;
        else
        {
            ToggleTeleport = true;
        }
    }

    IEnumerator MovePlatformsToHoles()
    {
        print(troux.transform.position);
        //float y = GameObject.Find("level").transform.position.y - 0.01f;
        hud.DisplayLoadBar(0.2f);
        //troux.transform.position = new Vector3(troux.transform.position.x, y, troux.transform.position.z);
        troux.SetActive(true);
        yield return new WaitForSeconds(5);
        //troux.transform.position = new Vector3(troux.transform.position.x, -10f, troux.transform.position.z);
        troux.SetActive(false);
    }

    IEnumerator ChangeSpeed(float speedNew)
    {
        float speed_before = this.speed;
        this.speed = speedNew;
        hud.DisplayLoadBar(0.4f);
        yield return new WaitForSeconds(2.5f);
        this.speed = speed_before;
    }

}
