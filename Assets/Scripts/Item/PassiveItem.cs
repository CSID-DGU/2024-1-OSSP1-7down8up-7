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

    public Sprite Sprite;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        itemStat = GetComponent<ItemStat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        Sprite = spriteRenderer.sprite;

    }

    public void OnEnable()
    {
        GManager = GameObject.Find("GManager");

        // Start the blinking coroutine
        StartCoroutine(BlinkWhite());
    }

    private IEnumerator BlinkWhite()
    {
        // Debug.Log("깜빡");
        float blinkDuration = 1f; // Total duration of the blinking effect
        float blinkInterval = 0.5f; // Interval between blinks
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            switch (type)
            {
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

    //변경부분(Activate함수 인자값을 받아서 아이템이름을 리스트로 가지게 변경)
    public void Activate(float itemName)
    {
        player = GameObject.Find("Player");
        stat = player.GetComponent<PlayerStat>();
        stat.ChangeDamage(itemStat.GetDamage());
        stat.ChangeBulletLifeTime(itemStat.GetBulletLifeTime());
        stat.ChangeBulletSpeed(itemStat.GetBulletSpeed());
        stat.ChangeMaxHP(itemStat.GetMaxHP());
        stat.ChangeMoveSpeed(itemStat.GetMoveSpeed());
        stat.ChangeTimeBetweenShots(itemStat.GetTimeBetweenShots());
        stat.PlusitemCount();
        stat.PlusitemName(itemName);
        stat.PlusCritical(itemStat.GetCritical());
        stat.Heal(itemStat.GetCurrentHP());
        //아이템 먹을 때마다 점수 추가
        if(type == Rarity.normal)
        {
            stat.ChangeScore(10);
        }
        else if (type == Rarity.rare)
        {
            stat.ChangeScore(20);
        }
        else if (type == Rarity.unique)
        {
            stat.ChangeScore(30);
        }

        //아이템 0,1,2 세트 효과 -> 최대 체력 10 증가
        if (stat.ContainsAll(0,1,2))
        {
            stat.PlusMaxHP(10);
        }

        //아이템 3,4,5 세트 효과 -> 공격력 10 증가
        if (stat.ContainsAll(3, 4, 5))
        {
            stat.PlusDamage(10);
        }
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
        stat.PlusitemCount();
        this.gameObject.SetActive(false);
    }

    // 아이템과 플레이어 충돌시 효과 적용
}
