using UnityEngine;
using TMPro;
public class SpinningCoin : MonoBehaviour
{
    public float spinSpeed = 100f; // Speed of the coin spin

    void Update()
    {
        // Make the coin spin
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime);
    }

}
