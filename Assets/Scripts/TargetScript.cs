using UnityEngine;
using System.Collections;
using System;

public class TargetScript : MonoBehaviour, ITarget {
    public int TotalHealth { get; set; }
    float direction = 1f;


    public void Start() {
        TotalHealth = 10;
        StartCoroutine(MovePosition());
    }

    public IEnumerator MovePosition() {
        while (true) {
            if(transform.localPosition.y <= 0) {
                direction = 1f;
               
            }
            else if(transform.localPosition.y >= 8) {
                direction = -1f;
            }
            transform.localPosition = new Vector3(transform.localPosition.x,
                                                transform.localPosition.y + direction*0.05f,
                                                transform.localPosition.z);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
        Debug.Log("TRIGGER");
    }
}
