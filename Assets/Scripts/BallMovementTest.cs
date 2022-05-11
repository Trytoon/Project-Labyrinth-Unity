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

    Vector3[] SpawnPoints = new Vector3[2];
    int SceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        SpawnPoints[0] = new Vector3(0.06f, 3.5f, 0.06f);
        SpawnPoints[1] = new Vector3(0.634f, 3.115f, 0.688f);

        transform.position = SpawnPoints[SceneIndex];
        checkpointCoordinate = transform.position;
        rb = GetComponent<Rigidbody>();
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

        

        if (newPos.y < 0)
        {
            Debug.Log("done");
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            transform.position = checkpointCoordinate;
            GameManager.SetLives(GameManager.GetLives() - 1);
            hud.Refresh();
        }

        rb.MovePosition(newPos);
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
            Destroy(other);
            GameManager.IncrementScore();
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
        }
        else if (other.CompareTag("Trampo")) {
            Destroy(other);
            GameObject troux = GameObject.Find("Troux");
            float y = troux.transform.position.y + 10;
            troux.transform.position = new Vector3(troux.transform.position.x, y, troux.transform.position.z);

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
}
