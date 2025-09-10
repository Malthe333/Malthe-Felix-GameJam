using UnityEditor;
using UnityEngine;

public class DarkBolt : MonoBehaviour
{


    // Update is called once per frame
    public HeroKnight heroKnight;
    public GameObject darkBoltPrefab;


    public void ShootDarkBolt()
    {
        
        GameObject darkBolt = Instantiate(darkBoltPrefab, transform.position, Quaternion.Euler(0f, 0f, 90f));

        if (heroKnight.isFlipped == true)
        {
            darkBolt.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-10f, 0f);
        }
        else
        {
            darkBolt.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(10f, 0f);
        }
        Invoke("DestroyDarkBolt", 2.0f);

        // This could involve enabling the projectile, setting its position, etc.}
    }

    private void DestroyDarkBolt()
    {
        Destroy(GameObject.FindWithTag("DarkBolt"));
    }
}
