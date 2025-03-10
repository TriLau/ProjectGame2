using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Recipe")]
public class Recipe : ScriptableObject
{
    [Header("Output")]
    public Item itemOutput;

    [Header("Recipe")]
    public Item item00;
    public Item item01;
    public Item item02;
    public Item item10;
    public Item item11;
    public Item item12;
    public Item item20;
    public Item item21;
    public Item item22;
}
