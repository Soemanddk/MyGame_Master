using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MyGame.App_Code
{
    public class StatIndex
    {
        #region Attributes

        public string Type { get; private set; }
        public int Slot_Id { get; private set; }
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Agility { get; set; }
        public int Dexterity { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Health { get; set; }
        public int Power { get; set; }
        public int Energy { get; set; }
        public int Melee_Attack_Damage { get; set; }
        public int Spell_Cast_Damage { get; set; }
        public int Melee_Multi_Attack { get; set; }
        public int Spell_Multi_Attack { get; set; }
        public int Critical_Chance { get; set; }
        public int Critical_Bonus { get; set; }
        public int Potency { get; set; }
        public int Skill_Modifier { get; set; }
        public int Avoidance { get; set; }
        public int Resistance { get; set; }
        
        #endregion

        #region Constructors

        public StatIndex(string Prefix, DataRow Row)
        {
            switch (Prefix)
            {
                case "item":
                    this.Type = "Equipment";
                    this.Slot_Id = Convert.ToInt32(Row["item_slot_id"]);
                    break;
                case "char":
                    this.Type = "Charecter";
                    this.Slot_Id = 0;
                    break;
                case "enchant":
                    this.Type = "Enchantment";
                    this.Slot_Id = 0;
                    break;
            }

            this.Id = Convert.ToInt32(Row[Prefix + "_id"]);
            this.Name = Row[Prefix + "_name"].ToString();
            this.Strength = Convert.ToInt32(Row[Prefix + "strength"]);
            this.Stamina = Convert.ToInt32(Row[Prefix + "stamina"]);
            this.Agility = Convert.ToInt32(Row[Prefix + "agility"]);
            this.Dexterity = Convert.ToInt32(Row[Prefix + "dexterity"]);
            this.Wisdom = Convert.ToInt32(Row[Prefix + "wisdom"]);
            this.Intelligence = Convert.ToInt32(Row[Prefix + ""]);
            this.Health = Convert.ToInt32(Row[Prefix + "intelligence"]);
            this.Power = Convert.ToInt32(Row[Prefix + "power"]);
            this.Energy = Convert.ToInt32(Row[Prefix + "energy"]);
            this.Melee_Attack_Damage = Convert.ToInt32(Row[Prefix + "melee_attack_damage"]);
            this.Spell_Cast_Damage = Convert.ToInt32(Row[Prefix + "spell_cast_damage"]);
            this.Melee_Multi_Attack = Convert.ToInt32(Row[Prefix + "melee_multi_attack"]);
            this.Spell_Multi_Attack = Convert.ToInt32(Row[Prefix + "spell_multi_attack"]);
            this.Critical_Chance = Convert.ToInt32(Row[Prefix + "critical_chance"]);
            this.Critical_Bonus = Convert.ToInt32(Row[Prefix + "critical_bonus"]);
            this.Potency = Convert.ToInt32(Row[Prefix + "potency"]);
            this.Skill_Modifier = Convert.ToInt32(Row[Prefix + "skill_modficer"]);
            this.Avoidance = Convert.ToInt32(Row[Prefix + "avoidance"]);
            this.Resistance = Convert.ToInt32(Row[Prefix + "resistance"]);
        }

        #endregion
    }
}