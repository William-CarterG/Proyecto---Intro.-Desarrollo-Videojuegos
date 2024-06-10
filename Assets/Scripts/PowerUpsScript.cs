using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
    private PlayerHealth healthScript;
    private float shootingCooldown = 15.0f, startOfTaser;
    private bool canShoot = true;
    public GameObject proyectile;
    private GameObject ui;
    private float volantinTimer, volantinDuration = 10.0f;
    private bool isUsingVolantin = false;
    private GameObject Knife, Watch, Taser;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("PowerUps");
        if (ui != null)
        {
            Knife = ui.transform.Find("PUKnife").gameObject;
            Watch = ui.transform.Find("PUWatch").gameObject;
            Taser = ui.transform.Find("PUTaserGun").gameObject;
            Knife.SetActive(false);
            Watch.SetActive(false);
            Taser.SetActive(false);
        }
        healthScript = GetComponent<PlayerHealth>();
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
            if(collectedPowerUps.Count > 0)
            {
                ChangePU();
            }
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
            else if (powerUp == "PUTaserGun" && canShoot)
            {
                canShoot = false;
                startOfTaser = Time.time;
                shoot(moveHorizontal, moveVertical);
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

        if (!canShoot)
        {
            if ((Time.time - startOfTaser) > shootingCooldown)
            {
                canShoot = true;
            }
        }

        if (isUsingVolantin)
        {
            if ((Time.time - volantinTimer) > volantinDuration)
            {
                isUsingVolantin = false;
                healthScript.setInvulnerable(false);
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
            setUI();
        }
    }

    void setUI()
    {
        Knife.SetActive(false);
        Watch.SetActive(false);
        Taser.SetActive(false);
        if(powerUp == "PUKnife")
        {
            Knife.SetActive(true);
        }
        else if(powerUp == "PUWatch")
        {
            Watch.SetActive(true);
        }
        else
        {
            Taser.SetActive(true);
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

    void UseSneaker()
    {
        UsingSneakers = true;
        Starttimer = Time.time;
        controllerScript.multiplySpeed(speedRateUp);
    }

    void UseHotDog()
    {
        healthScript.RecuperateDamage(1);
    }

    void UseVolantin()
    {
        healthScript.setInvulnerable(true);
        isUsingVolantin = true;
        volantinTimer = Time.time;
    }

    public void UseConsumable(string name)
    {
        if(CheckIfStartsWith(name, "ConsumableSneakers"))
        {
            UseSneaker();
        }
        else if(CheckIfStartsWith(name, "ConsumableHotDog"))
        {
            UseHotDog();
        }
        else if (CheckIfStartsWith(name, "ConsumableVolantin"))
        {
            UseVolantin();
        }

    }

    public bool CheckIfStartsWith(string input, string start)
    {
        if (input.Length >= start.Length)
        {
            return input.StartsWith(start, System.StringComparison.Ordinal);
        }
        return false;
    }

    private void shoot(float horizontal, float vertical)
    {
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        direction.Normalize();
        GameObject newObject = Instantiate(proyectile, transform.position + direction * 2, transform.rotation);
        newObject.GetComponent<TaserProyectileScript>().setDirection(direction);
    }
}
