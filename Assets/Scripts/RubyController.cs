using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RubyController : MonoBehaviour {
  private Rigidbody2D rb2d;
  // Move Speed
  public float speed = 2.5f;

  //Health
  public int maxHealth = 10;
  int currentHealth;
  public int health {
    get { return currentHealth; }
    set { currentHealth = value; }
  }

  // Timer for Damege
  float timeInvincible = 2.0f;
  bool isInvincible;
  float invincibleTimer;


  void Start() {
    //Get Rigidbody2D component
    rb2d = GetComponent<Rigidbody2D>();

    // Setup Health
    currentHealth = maxHealth;
  }

  // Update is called once per frame
  void Update() {
    if (isInvincible) {
      invincibleTimer -= Time.deltaTime;
      if (invincibleTimer <= 0) {
        isInvincible = false;
      }
    }
  }

  void FixedUpdate() {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    Vector2 position = transform.position;
    position.x += speed * horizontal * Time.deltaTime;
    position.y += speed * vertical * Time.deltaTime;

    rb2d.MovePosition(position);
  }

  public void ChangeHealth(int amount) {
    if (amount < 0) {
      if (isInvincible) {
        return;
      }
      isInvincible = true;
      invincibleTimer = timeInvincible;
    }
    currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
  }
}
