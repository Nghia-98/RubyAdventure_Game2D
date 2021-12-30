using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RubyController : MonoBehaviour {
  private Rigidbody2D rb2d;
  // Move Speed
  public float speed = 2.5f;

  float horizontal;
  float vertical;

  //Look direction Vector2
  Vector2 lookDirection = new Vector2(0, 0);

  // Animator property
  Animator animator;

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
    animator = GetComponent<Animator>();

    // Setup Health
    currentHealth = maxHealth;
  }

  // Update is called once per frame
  void Update() {
    horizontal = Input.GetAxis("Horizontal");
    vertical = Input.GetAxis("Vertical");

    if (isInvincible) {
      invincibleTimer -= Time.deltaTime;
      if (invincibleTimer <= 0) {
        isInvincible = false;
      }
    }

    Vector2 move = new Vector2(horizontal, vertical);
    if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
      lookDirection.Set(move.x, move.y);
      lookDirection.Normalize();
    }

    animator.SetFloat("Look X", lookDirection.x);
    animator.SetFloat("Look Y", lookDirection.y);
    animator.SetFloat("Speed", move.magnitude);
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
