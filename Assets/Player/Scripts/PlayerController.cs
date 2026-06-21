using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float swimSpeed = 20f; //Regular movement
    public float dashSpeed = 35f; //Shmoovin
    public float stamina = 10f; //Time the player can be dashing
    public float maxStamina = 10f;
    public float staminaRegenRate = 0.5f; //Stamina regeneration rate
    public float staminaDepletionRate = 1f;
    public Rigidbody2D rBody;
    public Transform transform;


    public float currentMaxSpeed = 10f; //The maximum speed the player can reach while swimming
    public KeyCode dashKey = KeyCode.LeftShift;
    public bool isDashing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * swimSpeed);
        currentMaxSpeed = swimSpeed;
        Dash(dashKey, dashSpeed, staminaDepletionRate, maxStamina); 
        CheckSpeed(rBody, currentMaxSpeed);
        if (!isDashing)
        {
            if (stamina < 0)
            {
                stamina = 0;
            }
            if (stamina < maxStamina)
            {
                stamina += staminaRegenRate * Time.deltaTime;
            }
            else if (stamina > maxStamina)
            {
                stamina = maxStamina;
            }
        }
        else
        {
            currentMaxSpeed = dashSpeed;
        }
        

    }

    private void CheckSpeed(Rigidbody2D rBody, float maxSpeed)
    {
        if(rBody.linearVelocity.x > maxSpeed)
        {
            rBody.linearVelocity = new Vector2(maxSpeed, rBody.linearVelocity.y);
        }
        else if (rBody.linearVelocity.x < -maxSpeed)
        {
            rBody.linearVelocity = new Vector2(-maxSpeed, rBody.linearVelocity.y);
        }
        if (rBody.linearVelocity.y > maxSpeed)
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, maxSpeed);
        }
        else if (rBody.linearVelocity.y < -maxSpeed)
        {
            rBody.linearVelocity = new Vector2(rBody.linearVelocity.x, -maxSpeed);
        }
    }
    
    private void Dash(KeyCode dashkey, float dashSpeed, float staminaDepletionRate, float maxStamina)
    {
        if (Input.GetKey(dashKey) && stamina > 0)
        {
            rBody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * dashSpeed);
            stamina -= staminaDepletionRate * Time.deltaTime;
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }
    }
}
