using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.MLAgents;
//using Unity.MLAgents.Actuators;

public class MoveLowerJaw : MonoBehaviour
{
    protected HingeJoint jointController;

    private GameObject bottomJaw;
    private Vector3 position;
    private float velocity;
    private int frameCount;
    private float force;
    private float targetVelocity;
    // Start is called before the first frame update
    void Start()
    {
        print("hi "+this.name);
       // GameObject bottomJaw = GameObject.Find("bottom_jaw"); 
        jointController = GetComponent<HingeJoint>();
        if (jointController == null) print("Couldn't find HingeJoint");
        else print("got a joint controller");
        force = 100;
        var motor = jointController.motor;
        motor.force = force;
        motor.freeSpin = false;
        jointController.motor = motor;
        jointController.useMotor = true;
      
    }

    // Update is called once per frame
    void Update()
    {
        
        var motor = jointController.motor;
        motor.targetVelocity = targetVelocity;
        motor.force = force;
        jointController.motor = motor; 
        /*
        if (Input.GetKeyDown("o")) {
    
            motor.targetVelocity = -100;
            motor.force = force;
            jointController.motor = motor;   
         
            print("OPEN  "+jointController.transform.rotation);
        } 
        else if (Input.GetKeyDown("c")) {
            
            
            motor.targetVelocity = 100;
            motor.force = force*3; // needs to be quite strong to beat gravity!
            jointController.motor = motor; 
         
            print("CLOSE "+jointController.transform.rotation);    
        
        }
        */
    }

    public void OpenJaw() {
        //var motor = jointController.motor;
        targetVelocity = -100;
        force = 100;
       // jointController.motor = motor;    
        print("OPEN  "+jointController.transform.rotation);
        return;
    }

    public void CloseJaw() {
        
        targetVelocity = 100;
        force = 300;
        print("CLOSE "+jointController.transform.rotation);
        return;
    }    
}

