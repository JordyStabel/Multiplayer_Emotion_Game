using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

// Derived of networkbehaviour to get access to all networking stuff. 
// It also derives from Monobehaviour, so that can still be used aswell
public class Player : NetworkBehaviour {

    [SerializeField]
    private float speedMultiplier = 1f;

    public GameObject playerCamera;

    public Transform bulletFiringPoint;
    public GameObject bullet;

    public GameObject playerSphere;
    public GameObject spawnEffect;

    private bool triggeringAnotherPlayer;
    private GameObject otherPlayer;

    public float health = 100f;

    private void Start()
    {
        if (isLocalPlayer == true)
        {
            playerCamera.GetComponent<Camera>().enabled = true;
            this.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        }
        else
        {
            playerCamera.GetComponent<Camera>().enabled = false;
            this.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        }
    }

    private void OnEnable()
    {
        Vector3 randomPos = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
        this.transform.Translate(randomPos);
    }

    private void Update()
    {
        // Check if the current client is the 'owner' of this object
        if (isLocalPlayer == true)
        {
            // Move to the right
            if (Input.GetKey(KeyCode.D))
                this.transform.Translate(Vector3.right * Time.deltaTime * speedMultiplier);

            // Move to the left
            if (Input.GetKey(KeyCode.A))
                this.transform.Translate(Vector3.left * Time.deltaTime * speedMultiplier);

            // Spawn new object
            if (Input.GetKeyDown(KeyCode.I))
            {
                //CmdSpawn();
            }

            if (Input.GetMouseButtonDown(0))
            {
                CmdFireBullet();
            }

            if (triggeringAnotherPlayer && Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Players colided");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringAnotherPlayer = true;
            otherPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggeringAnotherPlayer = false;
            otherPlayer = null;
        }
    }

    [Command]
    public void CmdFireBullet()
    {
        GameObject _bullet = (GameObject)Instantiate(bullet, bulletFiringPoint.transform.position, Quaternion.identity);
        _bullet.transform.rotation = bulletFiringPoint.transform.rotation;

        // Spawn there gameObject and have authority over it 
        NetworkServer.SpawnWithClientAuthority(_bullet, connectionToClient);
    }
}
