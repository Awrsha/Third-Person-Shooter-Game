//firing with a gun script 
//third person shooter game
//Mohammad Mohsen Moradi / Amir Mohammad Parvizi / Morteza Pourasgar

using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 25f;
    public float range = 100f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
    }

    void shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Zombie zombie = hit.transform.GetComponent<Zombie>();
            if(zombie != null)
            {
                zombie.TakeDamage(damage);
            }
        }
    }
}
