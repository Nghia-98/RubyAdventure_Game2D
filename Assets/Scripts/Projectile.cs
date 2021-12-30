using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
  Rigidbody2D rb2d;

   void Awake() {
      rb2d = GetComponent<Rigidbody2D>();
  }


  void Start() {
  }

  // Update is called once per frame
  void Update() {

  }

  public void Launch(Vector2 direction, float force) {
    rb2d.AddForce(direction * force);
  }

  void OnCollisionEnter2D(Collision2D orther) {
    Debug.Log("Hit " + orther.gameObject.name);
    //Destroy(gameObject);
  }
}
