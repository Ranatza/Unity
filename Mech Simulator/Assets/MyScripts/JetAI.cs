using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JetState { hover, seek, fight, die}
public class JetAI : MonoBehaviour
{

    private Animator anim;
    private GameObject player;
    private JetState state;
    private float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Mech");
        state = JetState.hover;
        anim = transform.GetChild(0).GetComponent<Animator>();
        
        
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
                    //move to target
                    //Debug.Log("move in");
                    transform.LookAt(player.transform.position);
                }
                if(distanceToPlayer < 15)                    
                {
                    anim.SetBool("isAttacking", true);
                    //play animation
                    state = JetState.fight;
                }
                break;

            case JetState.fight:
                distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                //Debug.Log(distanceToPlayer);
                transform.LookAt(player.transform.position);
                break;

            case JetState.die:
                break;

        }


    }
}
