using UnityEngine;
using System.Collections;

namespace Kroulis.Item{

public class ItemTypes
{
    public const int unknown = 0;// Error
    public const int potions=1; //+HP,+MP
    public const int buff=2;  //To Allies
    public const int debuff=3; //To Enemy
    public const int damage=4; //To Enemy
    public const int material=5; //Cannot use
    public const int script=6; // When using then call a script.
    public static string GetTypeName(int typeid)
    {
        switch(typeid)
        {
            case 0:
                return "unknown";
            case 1:
                return "potions";
            case 2:
                return "buff";
            case 3:
                return "debuff";
            case 4:
                return "damage";
            case 5:
                return "material";
            case 6:
                return "script";
            default:
                return "unknown";
        }
    }
    public static string GetShowTypeName(int typeid)
    {
        switch(typeid)
        {
            case 1:
                return "Potions";
            case 2:
                return "Buff Orb";
            case 3:
                return "Curse Orb";
            case 4:
                return "Damage Orb";
            case 5:
                return "Material // Quest Items";
            case 6:
                return "Special Items";
            default:
                return "Unknown Items";
        }
    }
}

public class ItemQuality
{
    public const int trash = 0;
    public const int original = 1;
    public const int unusual = 2;
    public const int excellent=3;
    public const int rare = 4;
    public const int legendary = 5;
    public const int godmade = 6;
    public static string GetShowTypeName(int typeid)
    {
        switch(typeid)
        {
            case 0:
                return "Trash";
            case 1:
                return "Original";
            case 2:
                return "Unusual";
            case 3:
                return "Excellent";
            case 4:
                return "Rare";
            case 5:
                return "Legendary";
            case 6:
                return "Godmade";
            default:
                return "Unidentified";
        }
    }
}

public class Items: MonoBehaviour {
    protected int item_id = 0;
    protected int sell_price = 0;
    protected int buy_price = 0;
    public string item_name = "";
    protected int type = 0;
    protected int item_quality = 0;
    protected string item_description = "";
    protected float Cooldown = 0;
    protected float CooldownTimer = 0;
    [HideInInspector]
    public Sprite item_icon=null;

    public virtual void Start()
    {

    }

    void Update()
    {
        //Reduce cool down timer.
        if (CooldownTimer <= 0)
        {
            CooldownTimer = 0;
        }
        else if(CooldownTimer>0)
        {
            CooldownTimer = CooldownTimer - 1 * Time.deltaTime;
        }
    }
    public void SetBaseInfo(int id, string name, int bprice=0, int sprice=0,int quality=0, float cooldown=1.0f, string description=null, Sprite icon=null)
    {
        item_id=id;
        item_name=name;
        buy_price=bprice;
        sell_price=sprice;
        item_quality = quality;
        item_description = description;
        item_icon = icon;
        Cooldown = cooldown;
    }

    public string GetName()
    {
        return item_name;
    }

    public string GetDescription()
    {
        return item_description;
    }

    public int GetID()
    {
        return item_id;
    }

    public Sprite GetIcon()
    {
        return item_icon;
    }

    public int GetBuyPrice()
    {
        return buy_price;
    }

    public int GetSellPrice()
    {
        return sell_price;
    }

    public int GetType()
    {
        return type;
    }

    public int GetQuality()
    {
        return item_quality;
    }

    public virtual bool Use_Item(GameObject Caller=null , GameObject Target = null)
    {
        //Debug.Log("Attemp to use an unknown item, id is " + item_id.ToString() + ", name is " + item_name);
        return true;
    }
}

public class Potions:Items {
    private float HPRegen_Percent = 0, 
                  HPRegen_Unit = 0, 
                  MPRegen_Percent = 0, 
                  MPRegen_Unit = 0;
    private bool HPRegen_Flag = false,
                 MPRegen_Flag = false;
    public override void Start()
    {
        base.Start();
        type = ItemTypes.potions;
    }
    public void SetPotionsInfoHP(float HPRegenPercent,float HPRegen)
    {
        HPRegen_Flag = true;
        HPRegen_Percent = HPRegenPercent;
        HPRegen_Unit = HPRegen;
    }

    public void SetPotionsInfoMP(float MPRegenPercent, float MPRegen)
    {
        MPRegen_Flag = true;
        MPRegen_Percent = MPRegenPercent;
        MPRegen_Unit = MPRegen;
    }
    public override bool Use_Item(GameObject Caller=null, GameObject Target = null)
    {
        if (Target == null)
            return false;
        if (HPRegen_Flag)
        {
            Health Target_Health=Target.GetComponent<Health>();
            if (Target_Health == false)
                return false;
            else
            {
                float maxhealth = Target_Health.GetMaxHP();
                Target_Health.AddHealth((maxhealth * HPRegen_Percent / 100) + HPRegen_Unit);
            }
        }
        if(MPRegen_Flag)
        {
            Mana Target_Mana = Target.GetComponent<Mana>();
            if (Target_Mana == false)
                return false;
            else
            {
                float maxmana = Target_Mana.GetMaxMana();
                Target_Mana.addMana((int)((maxmana * MPRegen_Percent / 100) + MPRegen_Unit));
            }
        }
        return true;
        //return base.UseItem(Caller, Target);
    }
}

public class BuffOrb:Items
{
    private Skill BuffSkill;
    public override void Start()
    {
        base.Start();
        type = ItemTypes.buff;
    }
    public void SetBuffSkill(Skill buffskill)
    {
        BuffSkill = buffskill;
    }

    public override bool Use_Item(GameObject Caller = null, GameObject Target = null)
    {
        if (Target == null)
            return false;
        Target.AddComponent(BuffSkill.GetType());
        return true;
        //return base.UseItem(Caller, Target);
    }
}

public class DebuffOrb : Items
{
    private Skill DebuffSkill;

    public override void Start()
    {
        base.Start();
        type = ItemTypes.debuff;
    }
    public void SetBuffSkill(Skill debuffskill)
    {
        DebuffSkill = debuffskill;
    }

    public override bool Use_Item(GameObject Caller = null, GameObject Target = null)
    {
        if (Target == null)
            return false;
        Target.AddComponent(DebuffSkill.GetType());
        return true;
        //return base.UseItem(Caller, Target);
    }
}

public class DamageOrb : Items
{
    private float Damage=0;
    private float physical=0;
    private float magical=0;

    public override void Start()
    {
        base.Start();
        type = ItemTypes.damage;
    }
    public void SetDamage(float damage)
    {
        Damage = damage;
    }

    public void SetPhysicalInfluence(float value)
    {
        physical=value;
    }
    public void SetMagicalInfluence(float value)
    {
        magical=value;
    }

    private float calculate()
    {
        return Damage * -1;
    }

    public override bool Use_Item(GameObject Caller = null, GameObject Target = null)
    {
        if (Target == null)
            return false;
        Health Target_Health=Target.GetComponent<Health>();
        if (Target_Health == null)
            return false;
        Target_Health.AddHealth(calculate());
        return true;
        //return base.UseItem(Caller, Target);
    }
}

public class ScriptItem : Items
{
    private MonoBehaviour Script;

    public override void Start()
    {
        base.Start();
        type = ItemTypes.script;
    }
    public void SetScript (MonoBehaviour script)
    {
        Script = script;
    }

    public override bool Use_Item(GameObject Caller = null, GameObject Target = null)
    {
        if (Target == null)
            return false;
        Target.AddComponent(Script.GetType());
        return true;
        //return base.UseItem(Caller, Target);
    }
}

public class MaterialItem:Items
{
    public override void Start()
    {
        base.Start();
        type = ItemTypes.material;
    }

    public override bool Use_Item(GameObject Caller = null, GameObject Target = null)
    {
        return false;
    }
}


public struct ItemSlot
{
    int type;
    int number;
    private Potions potion_item;
    private BuffOrb buff_item;
    private DebuffOrb debuff_item;
    private DamageOrb damage_item;
    private ScriptItem script_item;
    private MaterialItem material_item;

    public void Clear()
    {
        type = 0;
        number = 0;
        potion_item = null;
        buff_item = null;
        debuff_item = null;
        damage_item = null;
        script_item = null;
        material_item = null;
    }

    public int GetNumber()
    {
        return number;
    }

    public int GetType()
    {
        return type;
    }

    public Items GetItem()
    {
        switch(type)
        {
            case ItemTypes.potions:
                return potion_item;
            case ItemTypes.buff:
                return buff_item;
            case ItemTypes.debuff:
                return debuff_item;
            case ItemTypes.damage:
                return damage_item;
            case ItemTypes.material:
                return material_item;
            case ItemTypes.script:
                return script_item;
            default:
                return null;
        }
    }

    public void SetItem(Items item,int number)
    {
        Clear();
        type = item.GetType();
        switch(type)
        {
            case ItemTypes.potions:
                potion_item=(Potions)item;
                break;
            case ItemTypes.buff:
                buff_item=(BuffOrb)item;
                break;
            case ItemTypes.debuff:
                debuff_item=(DebuffOrb)item;
                break;
            case ItemTypes.damage:
                damage_item=(DamageOrb)item;
                break;
            case ItemTypes.material:
                material_item=(MaterialItem)item;
                break;
            case ItemTypes.script:
                script_item=(ScriptItem)item;
                break;
            default:
                break;
        }
    }

    public void AddNumbers(int amount)
    {
        number = number + amount;
    }
}

}