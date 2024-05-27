using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsScript : MonoBehaviour
{
    private List<string> collectedPowerUps = new List<string>();
    private int settedPowerUp = 0;
    public string powerUp;
    private bool isUsingKnife = false, isUsingWatch = false, canUseWatch = true;
    private PlayerController controllerScript;
    private float directionx, directiony;
    private float watchDuration = 3f, watchCooldown = 30f, startOfWatch;
    private bool UsingSneakers = false;
    private float SneakersDuration = 10f, Starttimer, speedRateUp = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = GetComponent<PlayerController>();
        if (collectedPowerUps.Count > 0)
        {
            powerUp = collectedPowerUps[settedPowerUp];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isUsingSomething())
        {
            ChangePU();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            if (powerUp == "PUKnife" && !isUsingKnife && (moveHorizontal != 0 || moveVertical != 0))
            {
                isUsingKnife = true;
                controllerScript.setStunned(true);
                directionx = moveHorizontal;
                directiony = moveVertical;
            } 
            else if (powerUp == "PUWatch" && canUseWatch)
            {
                isUsingWatch = true;
                startOfWatch = Time.time;
                canUseWatch = false;
            }
        }

        if (UsingSneakers)
        {
            if ((Time.time - Starttimer) > SneakersDuration)
            {
                UsingSneakers = false;
                controllerScript.multiplySpeed(1/speedRateUp);
            }
        }
        
        if (isUsingKnife)
        {
            controllerScript.MovePlayer(directionx,directiony);
        }

        if (!canUseWatch)
        {
            if(isUsingWatch && ((Time.time - startOfWatch) > watchDuration))
            {
                isUsingWatch = false;
                startOfWatch = Time.time;
            }
            else if (!isUsingWatch && ((Time.time - startOfWatch) > watchCooldown))
            {
                canUseWatch = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isUsingKnife)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
            controllerScript.setStunned(false);
            isUsingKnife = false;
        }
    }

    public void SetPowerUps(List<string> powerUps)
    {
        if(collectedPowerUps.Count == 0 && powerUps.Count > 0)
        {
            powerUp = powerUps[0];
        }
        collectedPowerUps = powerUps;
    }

    void ChangePU()
    {
        if (collectedPowerUps.Count > 0)
        {
            settedPowerUp = (settedPowerUp + 1) % collectedPowerUps.Count;
            powerUp = collectedPowerUps[settedPowerUp];
        }
    }

    public bool checkWatch()
    {
        return !isUsingWatch;
    }

    bool isUsingSomething()
    {
        return isUsingWatch || isUsingKnife;
    }

    public void UseSneaker()
    {
        UsingSneakers = true;
        Starttimer = Time.time;
        controllerScript.multiplySpeed(speedRateUp);
    }
}
