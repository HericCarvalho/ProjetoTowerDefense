using UnityEngine;

public class EffectStun : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * 200f * Time.deltaTime);
    }
}
