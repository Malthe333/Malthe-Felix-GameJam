using UnityEditor;
using UnityEngine;

public class DarkBolt : MonoBehaviour
{


    // Update is called once per frame

    public GameObject darkBoltPrefab;


    public void ShootDarkBolt()
    {
        // Logic to shoot the dark bolt
        GameObject darkBolt = Instantiate(darkBoltPrefab, transform.position, Quaternion.identity);
        darkBolt.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(10f, 0f);
        Invoke("DestroyDarkBolt", 2.0f);

        // This could involve enabling the projectile, setting its position, etc.
    }

    private void DestroyDarkBolt()
    {
        Destroy(gameObject);
    }
}
