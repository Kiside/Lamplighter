using Godot;
using System;

[GlobalClass]
[Serializable]
public partial class DamageData : EffectData
{
	[Export] public float Damage { get; set; }
    [Export] public float PushbackForce { get; set; }
    [Export] public float AtbPositionDamage { get; set; }

    public DamageData() : this(0f, 0f, 0f) {}

    public DamageData(float damage, float pushback, float atbPositionDamage)
    {
        Damage = damage;
        PushbackForce = pushback;
        AtbPositionDamage = atbPositionDamage;
    }

    public override void Apply()
    {
        
    }
}

