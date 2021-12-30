using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

  public float speed = 2.5f;
  public bool vertical = false;
  int direction = 1;
  float movingTime = 1.5f;
  public float movingTimer;

  Rigidbody2D rb2d;

  Animator animator;

  // Start is called before the first frame update
  void Start() {
    rb2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    movingTimer = movingTime;
  }

  private void Update() {
    movingTimer -= Time.deltaTime;

    if (movingTimer <= 0) {
      direction *= -1;
      movingTimer = movingTime;
    }

    if (vertical) {
      animator.SetFloat("Move X", 0);
      animator.SetFloat("Move Y", direction);
    } else {
      animator.SetFloat("Move X", direction);
      animator.SetFloat("Move Y", 0);
    }
  }

  private void FixedUpdate() {
    Vector2 pos = rb2d.position;

    if (vertical) {
      pos.y += (speed * Time.deltaTime * direction);
    } else {
      pos.x += (speed * Time.deltaTime * direction);
    }

    rb2d.MovePosition(pos);
  }

  private void OnTriggerEnter2D(Collider2D orther) {
    RubyController rubyController = orther.GetComponent<RubyController>();

    if (rubyController != null) {
      rubyController.ChangeHealth(-1);
    }

    Debug.Log("EnemyController - Ruby Heal: " + rubyController.health);
  }
}