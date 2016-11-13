using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletController : MonoBehaviour {
    PathGrapher grapher;
    bool movementEnbabled;
    void Start () {
        grapher = new PathGrapher("x");
        movementEnbabled = false;
        //StartCoroutine("MoveBullet");
    }

    public void OnClick(Text function) {
        Debug.Log("FUNCTION: " + function.text);
        transform.localPosition = Vector3.zero;
        grapher.SetFunction(function.text);
        movementEnbabled = true;
        //StartCoroutine("MoveBullet");
    }

    IEnumerator MoveBullet() {
        //lineRenderer.SetPositions(grapher.Points.ToArray());

        for (int i = 0; i < grapher.Points.Count - 1; i++) {
            transform.localPosition = grapher.GetNextPosition();
            Vector3 movement = grapher.GetNextPosition() - transform.localPosition;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, movement);
            Debug.Log("Point " + i + ": " + grapher.Points[i]);
            yield return new WaitForSeconds(0.02f);
        }

        //for (int i = 0; i < grapher.Points.Count-1; i++) {
        //    transform.localPosition = grapher.Points[i];
        //    Vector3 movement = grapher.Points[i + 1] - grapher.Points[i];
        //    transform.rotation = Quaternion.FromToRotation(Vector3.right, movement);
        //    Debug.Log("Point " + i + ": " + grapher.Points[i]);
        //    yield return new WaitForSeconds(0.02f);
        //}
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
