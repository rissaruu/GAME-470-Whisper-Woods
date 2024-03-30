using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Animtion_Enable : MonoBehaviour
{
    public Animator TomNPCAnimation;
    public Animator CoralNPCAnimation;
    public Animator ElmoreNPCAnimation;
    void Start()
    {
        NPCAnimationEnable();
        TomNPCAnimation = GameObject.FindWithTag("TomAnimations").GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    void NPCAnimationEnable ()
    {
        //Tom



        //Elmore

        //Coral
    }
}
