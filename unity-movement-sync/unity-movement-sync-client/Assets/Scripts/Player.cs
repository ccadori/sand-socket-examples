using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Main game;

    public Character character;
    public float positionSyncRate = 0.1f;
    public float minPositionToSync = 0.2f;

    private void Start()
    {
        game = FindObjectOfType<Main>();
        StartCoroutine(SyncPosition());
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 mod = (new Vector2(horizontal, vertical) * character.velocity * Time.deltaTime);
        transform.position = (Vector2)transform.position + mod;
    }

    private IEnumerator SyncPosition()
    {
        Vector2 lastUpdatedPosition = transform.position;

        while (true)
        {
            if (Vector2.Distance(lastUpdatedPosition, transform.position) > minPositionToSync)
            {
                game.client.Write("position", JsonUtility.ToJson((Vector2)transform.position));
                lastUpdatedPosition = transform.position;
            }

            yield return new WaitForSeconds(positionSyncRate);
        }
    }
}
