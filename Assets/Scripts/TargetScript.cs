using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject);
        Debug.Log("TRIGGER");
    }
}
