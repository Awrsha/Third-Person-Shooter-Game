//zombie character script 
//third person shooter game
//Mohammad Mohsen Moradi / Amir Mohammad Parvizi / Morteza Pourasgar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public GameObject Swat;
    public NavMeshAgent nm;
    public float ZombieHealth = 100f;
    public Animator zombie;
    public bool attack1;
    public bool chase;
    public bool Dead;
    // Start is called before the first frame update
    void Start()
    {
        Swat = GameObject.Find("Swat");
        nm = GetComponent<NavMeshAgent> ();
        zombie = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(Dead == false)
        {
            float distance = Vector3.Distance(Swat.transform.position, transform.position);
        
            if(distance <= 10)
            {
                if(distance <= 3)
                {
                    chase = false;
                    attack1 = true;
                }
                else
                {
                    chase = true;
                }
            }
        

            if(attack1 == true && chase == false)
            {
                attack();
            }

            if(chase == true)
            {
                chasing();
            }

            if(ZombieHealth <= 0)
            {
                die();
            }
        }
    }

    public void TakeDamage(float amount)
    {
        ZombieHealth -= amount;
        if(ZombieHealth <= 0f)
        {
            die();
        }
    }
    
    void chasing()
    {
        attack1 = false;
        nm.SetDestination(Swat.transform.position);
        zombie.SetInteger("Z",1);
        nm.speed = 1;
    }

    void die()
    {
        nm.enabled = false;
        zombie.enabled = false;
        GetComponent<Rigidbody>().freezeRotation = false;
        Dead = true;
    }
    
    void attack()
    {
        attack1 = true;
        zombie.SetInteger("Z",2);
        nm.speed = 0;
    }
}
