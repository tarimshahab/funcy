using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class BulletController : MonoBehaviour {
    PathGrapher grapher;
    bool movementEnbabled;

    void Start () {
        grapher = new PathGrapher();
        movementEnbabled = false;
    }

    public void GoButtonOnClick(Text function) {        
        transform.localPosition = Vector3.zero;
        grapher.SetFunction(function.text);
        movementEnbabled = true;
    }

    IEnumerator MoveBullet() {
        for (int i = 0; i < grapher.Points.Count - 1; i++) {
            transform.localPosition = grapher.GetNextPosition();
            Vector3 movement = grapher.GetNextPosition() - transform.localPosition;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, movement);
            yield return new WaitForSeconds(0.02f);
        }
        yield return null;
    }

    void FixedUpdate() {
        if (movementEnbabled) {
            transform.localPosition = grapher.GetNextPosition();
            Vector3 movement = grapher.GetNextPosition() - transform.localPosition;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, movement);
        }
    }
}
