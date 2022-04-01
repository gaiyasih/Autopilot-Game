using UnityEngine;

public class Camera : MonoBehaviour {
    public Transform carPosition;
    public Transform mainCamera;

    void Start() {

    }

    void Update() {
        mainCamera.position = carPosition.position + new Vector3(0, 0, -10);
    }
}
