using System.Collections;
using UnityEngine;

public enum PlayerType
{
    GOLEM,
    GUARDIAN,
    NONE
}

public enum PlayerNum
{
    PLAYER_1,
    PLAYER_2,
    PLAYER_3,
    PLAYER_4,
    NONE
}

public enum PlayerTeam
{
    RED,
    BLUE,
    NONE
}

public enum IndicatorType
{
    NONE,
    ARROW,
    CONE,
    CIRCLE,
    POINT
}

public enum FloatingDamageSubTextType
{
    DAMAGE,
    HEAL,
    CRITICAL,
    SHIELD,
    NONE
}

public enum FloatingTextType
{
    DAMAGE,
    STATUS
}

public enum ConduitState
{
    EMPTY,
    IN_PROGRESS,
    CONTROLLED,
    DRAINING,
    CONTESTED,
    DISABLED,
    HOMEBASE
}

public enum StatusEffect
{
    NONE,
    STUN,
    SLOW,
    BLEED,
    MANA_DRAIN,
    SILENCE,
    SHIELD,
    KNOCKBACK,
    HEALOVERTIME,
    STAGGER,
    PULL
}

public enum DamageType
{
    FIRE,
    ICE,
    WIND,
    EARTH,
    SLASH,
    SMASH,
    PIERCE,
    PURE,
    NONE
}

public enum AbilityType
{
    DAMAGE,
    HEAL,
    SUPPORT,
    UTILITY
}

public enum AbilitySubType
{
    PROJECTILE,
    STATIC,
    ZONE,
    BEAM,
    SELF,
}

public enum Direction
{
    LEFT,
    RIGHT,
    UP,
    DOWN
}
