using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ERType
{
    Tree,
    Stone
}

public class EnvironmentalResource : MonoBehaviour
{
    private Animator animator;
    private Damageable damageable;

    [SerializeField]
    private ERType eRType;

    private EWeatherType eWeather;

    void Start()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        eWeather = EWeatherType.Spring;
        ChangeState();
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        animator.SetTrigger("Hit");
    }

    public void ChangeState()
    {
        if (damageable.Health == 10)
        {
            animator.Play("Root_Idle");
        }
    }

    public void ChangeBySeasion()
    {
        switch(eWeather)
        {
            case EWeatherType.Spring:
                {
                    animator.Play(AnimationStrings.springIdle);
                    break;
                }
            case EWeatherType.Summer:
                {
                    animator.Play(AnimationStrings.summerIdle);
                    break;
                }
            case EWeatherType.Winter:
                {
                    animator.Play(AnimationStrings.winterIdle);
                    break;
                }
        }
    }
}
