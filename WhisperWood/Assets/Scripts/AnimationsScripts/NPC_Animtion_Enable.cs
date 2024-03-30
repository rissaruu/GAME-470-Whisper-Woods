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
        CoralNPCAnimation = GameObject.FindWithTag("CoralAnimations").GetComponent <Animator>();
        ElmoreNPCAnimation = GameObject.FindWithTag("ElmoreAnimations").GetComponent<Animator>();
    }

    void Update()
    {
        NPCAnimationEnable();
    }

    void NPCAnimationEnable ()
    {
        //Tom
        if (TomNPCAnimation.GetBool("NPCRun") == false) //I might need to search for a NPC Run Script part.
        {
            TomNPCAnimation.SetBool("NPCRun", true);
        }
        else if (TomNPCAnimation.GetBool("NPCWalk") == false)
        {
            TomNPCAnimation.SetBool("NPCWalk", true);
        }
        else
        {
            //The fail safe for the idle animation. This script also can be split up for faster code if needed.
            TomNPCAnimation.SetBool("NPCRun", false);
            TomNPCAnimation.SetBool("NPCWalk", false);
        }

        //Elmore
        if (ElmoreNPCAnimation.GetBool("NPCDrinking") == false)
        {
            ElmoreNPCAnimation.SetBool("NPCDrinking", true);
        }
        else
        {
            ElmoreNPCAnimation.SetBool("NPCDrinking", false);
        }

        //Coral
        if (CoralNPCAnimation.GetBool("NPCSitting") == false)
        {
            CoralNPCAnimation.SetBool("NPCSitting", true);
        }
        else
        {
            CoralNPCAnimation.SetBool("NPCSitting", false);
        }

    }
}
