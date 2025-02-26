using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentalResource : MonoBehaviour
{
    private Animator animator;
    private Damageable damageable;

    [SerializeField]
    private int numItem = 2;

    [SerializeField]
    private GameObject item;

    void Start()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        ChangeState();
        //ChangeBySeason();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        animator.SetTrigger("Hit");
    }

    public void ChangeState()
    {
        if (damageable.Health == 20)
        {
            animator.SetBool(AnimationStrings.hasBeenCut, true);
            SpawnItem();
        }
    }

    public void ChangeBySeason()
    {
        ESeason season = SeasonManager.Instance.Season;

        switch(season)
        {
            case ESeason.Spring:
                {
                    animator.Play(AnimationStrings.springIdle);
                    break;
                }
            case ESeason.Summer:
                {
                    animator.Play(AnimationStrings.summerIdle);
                    break;
                }
            case ESeason.Autumn:
                {
                    animator.Play(AnimationStrings.autumnIdle);
                    break;
                }
            case ESeason.Winter:
                {
                    animator.Play(AnimationStrings.winterIdle);
                    break;
                }
        }
    }

    public void SpawnItem()
    {
        if (numItem > 0)
        {
            GameObject itemSpawned = Instantiate(item, transform.position, Quaternion.identity);
            Rigidbody2D rb = itemSpawned.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x * 1f, rb.velocity.y * 1f);
            numItem--;
        }
    }
}
