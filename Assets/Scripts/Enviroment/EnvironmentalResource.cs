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

    private void OnEnable()
    {
        EnviromentalStatusManager.ChangeSeasonEvent += ChangeBySeason;
    }

    private void OnDisable()
    {
        EnviromentalStatusManager.ChangeSeasonEvent -= ChangeBySeason;
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        
        if (damageable.Health == 20)
        {
            animator.SetBool(AnimationStrings.hasBeenCut, true);
            DropItem();
        }else animator.SetTrigger("Hit");
    }


    public void ChangeBySeason(ESeason season)
    {
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

    public void DropItem()
    {
        if (numItem > 0)
        {
            for (int i = 0; i < numItem; i++)
            {
                Vector3 randomDir = UtilsClass.GetRandomDir();
                Vector3 position = this.transform.position + randomDir * 0.2f;
                GameObject transform = Instantiate(item, position, Quaternion.identity);

                transform.gameObject.GetComponent<Rigidbody2D>().AddForce(randomDir * 5f, ForceMode2D.Impulse);
                ItemWorld itemWorld = transform.GetComponent<ItemWorldControl>().GetItemWorld();
                ItemWorldManager.Instance.AddItemWorld(itemWorld);
            }
        }      
    }
}
