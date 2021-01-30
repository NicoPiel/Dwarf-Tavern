using System.Collections;
using System.Collections.Generic;
using Inventory;
using Simulation.Core;
using UnityEngine;
using UnityEngine.UI;

public class ItemBeer : ComplexItem
{
    private readonly Attribute _taste1;
    private readonly Attribute _taste2;
    private readonly Type _type;
    private Color _beerColor;
    
    [SerializeField] private readonly int price;

    public ItemBeer(StaticItem reference, Attribute taste1, Attribute taste2, Type type, Color beerColor, int beerPrice) : base(reference)
    {
        _taste1 = taste1;
        _taste2 = taste2;
        _type = type;
        _beerColor = beerColor;
        price = beerPrice;
    }

    public bool hasAttribute(Attribute attr)
    {
        return _taste1 == attr || _taste2 == attr;
    }

    public Attribute[] GetAttributes()
    {
        return new Attribute[] {_taste1, _taste2};
    }

    public Type GetDrinkType()
    {
        return _type;
    }

    public Color GetColorModifier()
    {
        return _beerColor;
    }

    public override bool Equals(object other)
    {
        if (!(other is ItemBeer)) return false;
        var oBeer = (ItemBeer) other;
        return base.Equals(other) && _taste1 == oBeer._taste1 && _taste2 == oBeer._taste2 && _type == oBeer._type;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = base.GetHashCode();
            hashCode = (hashCode * 397) ^ (int) _taste1;
            hashCode = (hashCode * 397) ^ (int) _taste2;
            hashCode = (hashCode * 397) ^ (int) _type;
            return hashCode;
        }
    }

    public override string ToString()
    {
        return $"ID:{GetId()}, Type:{GetDrinkType()}, Attributes:{GetAttributes()[0]}, {GetAttributes()[1]}, Color:{GetColorModifier()}";
    }


    public enum Type
    {
        Unused = 0,
        Beer,
        Schnapps,
        Wodka,
        Whiskey,
        Wine
    }

    public enum Attribute
    {
        Unused = 0,
        Strength,
        Dexterity,
        Intelligence,
        Vitality,
        Will,
        Courage
    }

    public string GetAttributeCombinationDenominator()
    {
        return SimulationManager.AttributeCombinations[AttributeToString(_taste1)][(int) _taste2];
    }

    public static string TypeToString(Type type)
    {
        return type switch
        {
            Type.Beer => "Bier",
            Type.Schnapps => "Schnaps",
            Type.Wodka => "Wodka",
            Type.Whiskey => "Whiskey",
            Type.Wine => "Wein",
            Type.Unused => "Ungenutzt",
            _ => throw new UnityException("Something went wrong.")
        };
    }

    public static string AttributeToString(Attribute attribute)
    {
        return attribute switch
        {
            Attribute.Strength => "Stärke",
            Attribute.Dexterity => "Geschick",
            Attribute.Intelligence => "Intelligenz",
            Attribute.Vitality => "Vitalität",
            Attribute.Will => "Wille",
            Attribute.Courage => "Mut",
            Attribute.Unused => "Ungenutzt",
            _ => throw new UnityException("This attribute does not exist.")
        };
    }

    public static Type GetRandomType()
    {
        var random = Random.Range(0, 5);

        return random switch
        {
            0 => Type.Beer,
            1 => Type.Schnapps,
            2 => Type.Wodka,
            3 => Type.Whiskey,
            4 => Type.Wine,
            _ => throw new UnityException("Something went wrong")
        };
    }

    public int GetPrice()
    {
        return price;
    }
}