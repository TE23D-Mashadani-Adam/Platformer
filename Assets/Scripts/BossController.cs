using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{

    [SerializeField]
    Transform upChecker;
    [SerializeField]
    Transform downChecker;
    [SerializeField]
    LayerMask barrier;
    [SerializeField]
    float speed = 6;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject megaBullet;
    [SerializeField]
    Transform gunPosition;
    [SerializeField]
    Slider hpBar;
    [SerializeField]
    Slider cooldownShootSlider;

    float timeBetweenShots = 0.8f;
    float timeSinceLastShot = 0;

    bool megaAttackPhase = false;
    bool bossKilled = false;
    int megaAttackRounds = 0;
    int shootRounds = 0;
    int timesShooted = 0;
    bool antiFroze = false;
    float frozenTime;

    float currentHealth = 1;
    float damage = .08f;

    float normalSpeed = 6;

    bool frozen = false;

    float frozenCooldown = 0;


    // Update is called once per frame
    void Update()
    {
        print(currentHealth);


        if (!frozen)
        {
            // Boss movement
            Vector2 movement = new Vector2(0, speed);
            transform.Translate(movement * Time.deltaTime);

            BossShootLogicCode();

        }
        else
        {
            frozenTime += Time.deltaTime;
            cooldownShootSlider.value = frozenTime;
        }

        antiFrozeBossLogic();


        BossKillCode();



    }

    bool TouchUpBarrier()
    {
        return Physics2D.OverlapCircle(upChecker.position, .2f, barrier);
    }
    bool TouchDownBarrier()
    {
        return Physics2D.OverlapCircle(downChecker.position, .2f, barrier);
    }

    public void BossShootLogicCode()
    {
        timeSinceLastShot += Time.deltaTime;
        if (TouchUpBarrier() && timesShooted < 8)
        {
            if (shootRounds < 2)
            {
                speed = 0;
            }
            else
            {
                speed = -normalSpeed;
            }
            if (timeSinceLastShot > timeBetweenShots)
            {
                Instantiate(bullet, gunPosition.position, Quaternion.identity);
                timeSinceLastShot = 0;
                shootRounds++;
                timesShooted++;
            }


        }
        else if (TouchDownBarrier() && timesShooted < 8)
        {
            if (shootRounds < 2)
            {
                speed = 0;
            }
            else
            {
                speed = normalSpeed;
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
                megaAttackPhase = false;
                speed = normalSpeed;
                megaAttackRounds = 0;
                timesShooted = 0;
            }

            if (!TouchDownBarrier() && !TouchUpBarrier())
            {
                shootRounds = 0;
            }

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "playerBullet")
            {
                frozen = true;
                currentHealth -= damage;
                hpBar.value = currentHealth;
                Destroy(other.gameObject);
                GetComponent<AudioSource>().Play(0);
            }

        }

    }

    public void BossKillCode()
    {
        if (currentHealth < 0)
        {
            bossKilled = true;
        }

        if (bossKilled)
        {
            Destroy(gameObject);
        }
    }

    public void antiFrozeBossLogic()
    {
        if (frozenTime > 1f)
        {
            frozen = false;
            frozenTime = 0;
            antiFroze = true;
            PlayerController.gunEquipped = false;
            cooldownShootSlider.value = 0;

        }

        if (!frozen && antiFroze)
        {
            frozenCooldown += Time.deltaTime;
        }

        if (frozenCooldown > 1)
        {
            PlayerController.gunEquipped = true;
            frozenCooldown = 0;
            antiFroze = false;
        }

        if (currentHealth < .5)
        {
            FallingWallScript.wallFall = true;
        }
        else
        {
            FallingWallScript.wallFall = false;
        }
    }
}
