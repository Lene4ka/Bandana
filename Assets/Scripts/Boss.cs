using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Boss : Enemy
{
    public GameObject Clew;
    public Transform ShotPoint;
    public GameObject Key;
    public Transform KeyPoint;

    public bool IsCharacterEntered = false;
    public GameObject DoorIn;
    public int FireDelay = 3;

    public float FireRate = 0.5f;
    public float NextFire = 0.0f;
    
    public void FixedUpdate()
    {
        if (IsCharacterEntered)
        {
            Move();
            Invoke("Fire", FireDelay);

            DoorIn.SetActive(true);
        }

        if (Health.HitPoints <= 0)
        {
            GameObject key = Instantiate(Key, KeyPoint.position, KeyPoint.rotation);
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        if (Time.time > NextFire)
        {
            if (!Clew)
                return;
            NextFire = Time.time + FireRate;

            Vector3 clewPos = GetClewPosition();
            GameObject clone = Instantiate(Clew, clewPos, base.Target.rotation);
            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
            rb.AddRelativeForce(Vector3.forward * 3, ForceMode2D.Impulse);
        }
    }

    Vector3 GetClewPosition()
    {
        if (Vector3.Distance(Target.position, transform.position) < 0f)
        {
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
            return ShotPoint.position;
        }
        return ShotPoint.position;
    }
}
