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
    private GameObject player; // �÷��̾� ������Ʈ�� ���� ����
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
        // Debug.Log("����");
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

    //����κ�(Activate�Լ� ���ڰ��� �޾Ƽ� �������̸��� ����Ʈ�� ������ ����)
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
        //������ ���� ������ ���� �߰�
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

        //������ 0,1,2 ��Ʈ ȿ�� -> �ִ� ü�� 10 ����
        if (stat.ContainsAll(0,1,2))
        {
            stat.PlusMaxHP(10);
        }

        //������ 3,4,5 ��Ʈ ȿ�� -> ���ݷ� 10 ����
        if (stat.ContainsAll(3, 4, 5))
        {
            stat.PlusDamage(10);
        }
    }

    // �߰� ������ ȿ���� ������ ���ô�
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

    // �����۰� �÷��̾� �浹�� ȿ�� ����
}
