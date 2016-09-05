using UnityEngine;

public class PingPongMove : MonoBehaviour
{
    void Update ()
    {
        this.transform.position = new Vector3(this.transform.position.x,
                                              Mathf.PingPong(Time.time * 3, 3),
                                              this.transform.position.z);
    }
}