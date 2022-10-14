using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RightInputHandler : MonoBehaviour
{
    public SteamVR_Action_Boolean trackpadPressed;
    public GameObject swordPrefab;
    public GameObject player;

    private PlayerController pc;
    private bool notStarted = true;

    // Start is called before the first frame update
    void Start()
    {
        pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notStarted) {
            GameObject swordInstance;
            swordInstance = Instantiate(swordPrefab, transform.position, transform.rotation);
            swordInstance.transform.Rotate(new Vector3(0, -90, -90));
            swordInstance.transform.position -= new Vector3(0, 0, 0.2f);
            swordInstance.transform.parent = transform;
            notStarted = false;
        }

        if (!pc.gameActive && trackpadPressed.GetStateDown(SteamVR_Input_Sources.RightHand)) {
            pc.gameActive = true; // starts game and resets score and health
            pc.score = 0;
            pc.increaseScore(0);
            pc.health = 100;
            pc.healthBar.SetPercent(1);
            pc.centerText.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        print(other.tag);
        if (other.tag == "Enemy") {
            GameObject.Destroy(other.transform.parent.gameObject); // destroy parent since hitbox is a child
            pc.increaseScore(3); // 3 points for melee kill
        }
    }
}
