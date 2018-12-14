using UnityEngine;
using UnityEngine.Networking;

public class PlayerSphere : NetworkBehaviour {

    [SerializeField]
    private float speedMultiplier = 1f;

    private void Update()
    {
        if (hasAuthority == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                this.transform.Translate(Vector3.up * Time.deltaTime * speedMultiplier);
            }
        }
    }
}
