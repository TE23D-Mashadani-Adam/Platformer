using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField]
    Transform upChecker;
    [SerializeField]
    Transform downChecker;
    [SerializeField]
    LayerMask barrier;
    [SerializeField]
    float speed = 3;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject megaBullet;
    [SerializeField]
    Transform gunPosition;

    float timeBetweenShots = 0.5f;
    float timeSinceLastShot = 0;

    bool megaAttackPhase = false;
    int megaAttackRounds = 0;
    int shootRounds = 0;
    int timesShooted = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        print(timesShooted);
        Vector2 movement = new Vector2(0, speed);
        transform.Translate(movement * Time.deltaTime);
        timeSinceLastShot += Time.deltaTime;
        if (touchUpBarrier() && timesShooted < 8)
        {
            if (shootRounds < 2)
            {
                speed = 0;
            }
            else
            {
                speed = -3;
            }
            if (timeSinceLastShot > timeBetweenShots)
            {
                Instantiate(bullet, gunPosition.position, Quaternion.identity);
                timeSinceLastShot = 0;
                shootRounds++;
                timesShooted++;
            }


        }
        else if (touchDownBarrier() && timesShooted < 8)
        {
            if (shootRounds < 2)
            {
                speed = 0;
            }
            else
            {
                speed = 3;
            }
            if (timeSinceLastShot > timeBetweenShots)
            {
                Instantiate(bullet, gunPosition.position, Quaternion.identity);
                timeSinceLastShot = 0;
                shootRounds++;
                timesShooted++;
            }
        }

        if (timesShooted >= 8)
        {
            megaAttackPhase = true;
        }

        if (megaAttackPhase && timesShooted >= 8)
        {
            speed = 0;
            if (timeSinceLastShot > timeBetweenShots)
            {
                Instantiate(megaBullet, gunPosition.position, Quaternion.identity);
                timeSinceLastShot = 0;
                megaAttackRounds++;
            }

            if (megaAttackRounds == 1)
            {
                print("here");
                megaAttackPhase = false;
                speed = 3;
                megaAttackRounds = 0;
                timesShooted = 0;
            }
        }

        if (!touchDownBarrier() && !touchUpBarrier())
        {
            shootRounds = 0;
        }








    }

    bool touchUpBarrier()
    {
        return Physics2D.OverlapCircle(upChecker.position, .2f, barrier);
    }
    bool touchDownBarrier()
    {
        return Physics2D.OverlapCircle(downChecker.position, .2f, barrier);
    }

}
