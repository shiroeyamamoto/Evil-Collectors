using UnityEngine;
using UnityEngine.UI;

public class EnemyBody : MonoBehaviour
{
    private float currentHealth;
    private float currentMana;
    private float currentStamina;
    private float currentCrit;
    private float currentCritDmg;
    private float currentAlibility;
    private float currentDefense;

    [HideInInspector] public float currentDamage, parryDamaged;

    [SerializeField] SO_EnemyType enemyType;
    [SerializeField] SO_EnemyData enemyData;

    [SerializeField] private Slider enemyHealthBar;

    public Transform groundCheck;
    [HideInInspector] public bool isGrounded, isFacingRight;
    [HideInInspector] public SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemyType.EnemySpawn();

        currentHealth = enemyType.health;
        currentMana = enemyType.mana;
        currentStamina = enemyType.stamina;
        currentCrit = enemyType.crit;
        currentCritDmg = enemyType.critDmg;
        currentAlibility = enemyType.alibility;
        currentDefense = enemyType.defense;
        currentDamage = enemyType.damage;

        isFacingRight = true;

        parryDamaged = currentDamage * 0.1f + Player.Instance.playerData.damage * 0.1f;

        enemyHealthBar.maxValue = enemyType.health;
        enemyHealthBar.value = currentHealth;
        enemyHealthBar.minValue = 0;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Debug.Log("Enemy max health: "+enemyType.health);
        //Debug.Log("Enemy current health: " + currentHealth);
        //Debug.Log("Enemy default heath: " + enemyData.defaultHealth);
    }
    /// <summary>
    /// Enemy chết
    /// </summary>
   public void EnemyDie()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Enemy Nhận damage
    /// </summary>
    /// <param name="dmg"></param>
    public void EnemyTakeDamge(float dmg)
    {
        if (currentHealth > 0)
        {
            currentHealth -= dmg;
            enemyHealthBar.value = currentHealth;
        }
        if (currentHealth <= 0)
            EnemyDie();
    }
}
