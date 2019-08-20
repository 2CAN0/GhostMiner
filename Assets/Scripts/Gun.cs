using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public int weaponLvl = 1;

    public ParticleSystem muzzleFalsh;

    public Camera fpsCam;

    public GameObject warning;

    private float nextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        muzzleFalsh.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log("Hit: " + hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null && weaponLvl >= target.requiredLvl)
            {
                target.TakeDamage(damage);
            }
            else if (target != null && weaponLvl < target.requiredLvl)
            {
                TextMeshProUGUI textWarning = warning.GetComponent<TextMeshProUGUI>();
                if (textWarning != null)
                {
                    //Debug.Log("Weapon is not strong enough");
                    textWarning.text = "You Need a level " + target.requiredLvl + " weapon to destroy this item";
                    StartCoroutine(ShowWarning());
                }
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }
        }


    }

    IEnumerator ShowWarning()
    {
        warning.SetActive(true);
        //Debug.Log("Showing warning");
        yield return new WaitForSeconds(2);
        warning.SetActive(false);
    }
}
