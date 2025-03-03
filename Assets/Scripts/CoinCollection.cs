using UnityEngine;
using TMPro;
public class CoinCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private float score = 0;

    void OnTriggerEnter(Collider other)
    {
        // Check if the player touched the coin
        if (other.transform.tag == "Coin")
        {
            // Increment score
            score++;

            // Update the score text
            ScoreText.text = "Score: " + score.ToString();

            // Destroy the coin after being collected
            Destroy(other.gameObject);
        }
    }
}
