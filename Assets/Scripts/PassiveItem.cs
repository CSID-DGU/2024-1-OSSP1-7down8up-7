using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    public enum Rarity
    {
        normal,
        rare,
        unique
    }
    public Rarity type;

    private Rigidbody2D rb2D;
    private CircleCollider2D circleCollider;
    private GameObject player; // 플레이어 오브젝트에 대한 참조
    private PlayerStat stat;
    private ItemStat itemStat;
    public GameObject GManager;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        itemStat = GetComponent<ItemStat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void OnEnable()
    {
        GManager = GameObject.Find("GManager");

        // Start the blinking coroutine
        StartCoroutine(BlinkWhite());
    }

    private IEnumerator BlinkWhite()
    {
        Debug.Log("깜빡");
        float blinkDuration = 1f; // Total duration of the blinking effect
        float blinkInterval = 0.5f; // Interval between blinks
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            switch (type) {
                case Rarity.normal:
                    spriteRenderer.color = Color.grey;
                    break;
                case Rarity.rare:
                    spriteRenderer.color = Color.yellow;
                    break;
                case Rarity.unique:
                    spriteRenderer.color = Color.magenta;
                    break;

            }

            
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkInterval);

            //elapsedTime += 2 * blinkInterval;
        }

        // Ensure the color is reset to the original after blinking
        spriteRenderer.color = originalColor;
    }

    public void Activate()
    {
        player = GameObject.Find("Player");
        stat = player.GetComponent<PlayerStat>();
        stat.ChangeDamage(itemStat.GetDamage());
        stat.ChangeBulletLifeTime(itemStat.GetBulletLifeTime());
        stat.ChangeBulletSpeed(itemStat.GetBulletSpeed());
        stat.ChangeMaxHP(itemStat.GetMaxHP());
        stat.ChangeMoveSpeed(itemStat.GetMoveSpeed());
        stat.ChangeTimeBetweenShots(itemStat.GetTimeBetweenShots());
    }

    // 추가 아이템 효과를 구현해 봅시다
    public void Deactivate()
    {
        stat = player.GetComponent<PlayerStat>();
        stat.ChangeDamage(-(itemStat.GetDamage()));
        stat.ChangeBulletLifeTime(-(itemStat.GetBulletLifeTime()));
        stat.ChangeBulletSpeed(-(itemStat.GetBulletSpeed()));
        stat.ChangeMaxHP(-(itemStat.GetMaxHP()));
        stat.ChangeMoveSpeed(-(itemStat.GetMoveSpeed()));
        stat.ChangeTimeBetweenShots(-(itemStat.GetTimeBetweenShots()));
        this.gameObject.SetActive(false);
    }
}
