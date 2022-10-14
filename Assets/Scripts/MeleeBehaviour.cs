using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : MonoBehaviour
{
    public AudioClip parry;

    AudioSource audioSource;
    Animator animator;
    private float moveSpeed = 3.0f;
    private float maxDist = 3.5f;
    private float maxCollectDist = 0.5f;
    private Vector3 playerPos = new Vector3 (0.0f, 0.0f, 0.0f);
    private GameObject[] collect;
    private GameObject firstPoint;
    private bool reachedCollect = false;
    private bool stop = false;
    private float furthest = 9999f;
    private bool parried = false;

    public PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
        collect = GameObject.FindGameObjectsWithTag("CollectionPoint");
        firstPoint = collect[0];

        foreach (GameObject go in collect) { // find nearest collection point
            float currDist = Vector3.Distance(transform.position, go.transform.position);
            if (currDist < furthest){
                firstPoint = go;
                furthest = currDist;
            }
        }

        transform.LookAt(firstPoint.transform.position); // mob goes to first point
    }

    // Update is called once per frame
    void Update()
    {
        if (reachedCollect && !stop){
            float dist = Vector3.Distance(transform.position, playerPos);

            if (dist < maxDist) { // start attack anim
                animator.SetBool("playRightSwing", true);
            }
            if (dist < 1.0f){ // stop moving when close
                stop = true;
                audioSource.Stop();
            }
        }
        else if (Vector3.Distance(transform.position, firstPoint.transform.position) < maxCollectDist) { // mob reached first point, now going to player
            reachedCollect = true;
            transform.LookAt(playerPos);
        }
        
        if (!stop) transform.position += transform.forward * moveSpeed * Time.deltaTime;

        if (parried && animator.GetNextAnimatorStateInfo(0).IsName("Right Swing")) parried = false;


    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PlayerSword" && !parried) {
            animator.StopPlayback();
            audioSource.Stop();
            AudioSource.PlayClipAtPoint(parry, transform.position, 0.2f);
            pc.increaseScore(1);
            
            animator.SetTrigger("parried");   
            parried = true;
        }
    }
}
