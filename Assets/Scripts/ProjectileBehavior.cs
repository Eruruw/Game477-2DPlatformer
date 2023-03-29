using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {
    private Vector3 localScale;
    private Transform playerTF;

    [SerializeField] private float speed = 8f;
    [SerializeField] private Vector3 launchOffset;
    [SerializeField] private bool thrown;

    private void Start() {
        playerTF = GameObject.Find("Player").transform;
        localScale = playerTF.transform.localScale;
        if (thrown) {
            if (localScale.x > 0f) {
                var direction = transform.right + Vector3.up;
                GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            }
            else {
                var direction = -transform.right + Vector3.up;
                GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
            }
        }
        transform.Translate(launchOffset);
        Destroy(gameObject, 5);
    }

    private void Update() {
        if (!thrown) {
            if (localScale.x > 0f) {
                transform.position += transform.right * Time.deltaTime * speed;
            }
            else {
                transform.position += -transform.right * Time.deltaTime * speed;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}