using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    
    public Action<int> OnHealthValueChanged;
    private bool isDead;

    private void Start()
    {
        Debug.Log($"{this.gameObject} health started");
        currentHealth = maxHealth;  
        OnHealthValueChanged?.Invoke(currentHealth);
        isDead = false;
    }

    private void OnEnable()
    {
        Debug.Log($"{this.gameObject} health enabled");
        currentHealth = maxHealth;
        OnHealthValueChanged?.Invoke(currentHealth);
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{this.gameObject} health take damage");
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);

        OnHealthValueChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{this.gameObject} health die");
        if(isDead)
            return;
        
        isDead = true;

        if (!gameObject.tag.Equals("Player"))
        {
            Enqueue();
            SeedsSpawner.Instance.EnemyDied();
        }
        else
        {
            // gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void Enqueue()
    {
        Debug.Log($"{this.gameObject} health enqueue");
        Poolable p = gameObject.GetComponent<Poolable>();
        GameObjectPoolController.Enqueue(p);
    }
}