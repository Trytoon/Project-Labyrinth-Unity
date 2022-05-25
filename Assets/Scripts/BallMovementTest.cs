using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using UnityEngine.SceneManagement;

public class BallMovementTest : MonoBehaviour
{

    public HudManager hud;

    public float speed = 8f;
    Rigidbody rb;

    Vector3 checkpointCoordinate;


    bool ToggleTeleport = true;

    Vector3 toTeleport;

    Vector3[] SpawnPoints = new Vector3[3];
    int SceneIndex;

    private AudioSource collectibleSound;


    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        SpawnPoints[1] = new Vector3(0.06f, 1.2f, 0.06f);
        SpawnPoints[2] = new Vector3(0.634f, 3.115f, 0.688f);

        transform.position = SpawnPoints[SceneIndex];
        checkpointCoordinate = transform.position;
        rb = GetComponent<Rigidbody>();

        collectibleSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        float distance = speed * Time.deltaTime;

        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(hInput * distance, 0f, vInput * distance);

        Vector3 pos = transform.position;

        Vector3 newPos = pos + movement;

        rb.MovePosition(newPos);

        

        if (newPos.y < -1)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            transform.position = checkpointCoordinate;
            GameManager.SetLives(GameManager.GetLives() - 1);
            hud.Refresh();
        }

        rb.MovePosition(newPos);

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
            Destroy(other);
        }
        else if (other.CompareTag("Coin"))
        {
            collectibleSound.Play();
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
            Invoke("ToggleTeleportStatus", 1f);
        } else if(other.CompareTag("Finish"))
        {
           
            Destroy(other);
            SceneManager.LoadScene(SceneIndex + 1);
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
        else if (other.CompareTag("slow")) {
            Destroy(other);
            StartCoroutine(ChangeSpeed(0.1f));
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
        GameObject troux = GameObject.Find("Troux");
        print(troux.transform.position);
        float y = GameObject.Find("level").transform.position.y - 0.01f;
        hud.DisplayLoadBar(0.2f);
        troux.transform.position = new Vector3(troux.transform.position.x, y, troux.transform.position.z);
        yield return new WaitForSeconds(5);
        troux.transform.position = new Vector3(troux.transform.position.x, -10f, troux.transform.position.z);
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
