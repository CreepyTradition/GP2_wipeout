using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 12f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player =
                other.GetComponent<PlayerMovement>();

            StartCoroutine(Boost(player));

            Destroy(gameObject);
        }
    }

    IEnumerator Boost(PlayerMovement player)
    {
        float originalSpeed = player.moveSpeed;

        player.moveSpeed = boostAmount;

        yield return new WaitForSeconds(duration);

        player.moveSpeed = originalSpeed;
    }
}