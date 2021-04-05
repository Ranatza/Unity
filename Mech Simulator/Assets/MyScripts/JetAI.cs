using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum JetState { hover, seek, fight, die}
public class JetAI : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private JetState state;
    private float distanceToPlayer;
    private NavMeshAgent nav;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mech");
        state = JetState.hover;
        anim = transform.GetChild(0).GetComponent<Animator>();
        nav = gameObject.GetComponentInParent<NavMeshAgent>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case JetState.hover:
                state = JetState.seek;
                    break;

            case JetState.seek:
                distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                if (distanceToPlayer > 15)
                {
                    nav.isStopped = false;
                    //move to target
                    //Debug.Log("move in");
                    nav.SetDestination(player.transform.position);
                    transform.LookAt(player.transform.position);
                }
                if(distanceToPlayer < 15)                    
                {
                    nav.isStopped = true;
                    StartCoroutine(DeployGuns());
                    

                    //play animation
                    state = JetState.fight;
                }
                break;

            case JetState.fight:
                distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                //Debug.Log(distanceToPlayer);
                if(distanceToPlayer < 14)
                {
                    Debug.Log("too close");
                    transform.parent.position += (transform.parent.position + (transform.parent.position - player.transform.position)).normalized * Time.deltaTime * 5;
                    //nav.SetDestination(transform.parent.position + (transform.parent.position - player.transform.position));
                }
                transform.LookAt(player.transform.position);
                break;

            case JetState.die:
                break;

        }

        IEnumerator DeployGuns()
        {
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(.5f);
            anim.SetBool("gunsDeployed", true);
        }
    }
}
