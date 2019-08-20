using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public int requiredLvl = 1;

    public virtual void TakeDamage(float amount)
    {
        health -= amount;

        Debug.Log(gameObject.transform.name + " health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
