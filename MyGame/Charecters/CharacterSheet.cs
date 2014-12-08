using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGame.Charecters
{
    public class CharacterSheet
    {
        DataClassesDataContext db = new DataClassesDataContext();

        /// <summary>
        /// Get the instance of the character.
        /// </summary>
        public charecter Character { get; set; }
        /// <summary>
        /// Contains whole charecter race picture image url address.
        /// </summary>
        public string CharcterRacePicture
        {
            get
            {
                return "/img/race/" + this.Character.race_picture_id + this.Character.race_picture.img_type;
            }
        }
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
        public List<charecter_stat> PrimaryStats
        {
            get
            {
               return (from cs in db.charecter_stats
                       where cs.charecter_id.Equals(this.Character.id)
                       select cs).ToList();
            }
        }
         public List<charecter_item> EquippedItems
        {
             get
            {
                return (from ci in db.charecter_items
                        where ci.charecter_id.Equals(this.Character.id) && ci.equipped.Equals(true)
                        select ci).ToList();
            }
        }

        #region Constructor
        public CharacterSheet(object CharacterId)
        {
            this.Character = (from c in db.charecters
                              where c.id.Equals(Convert.ToInt32(CharacterId))
                              select c).FirstOrDefault();

            this.Strength =
            this.Stamina =
            this.Agility =
            this.Dexterity =
            this.Intelligence =
            this.Wisdom =
            this.Health =
            this.Power =
            this.Energy =
            this.Melee_Attack_Damage =
            this.Spell_Cast_Damage =
            this.Melee_Multi_Attack =
            this.Spell_Multi_Attack =
            this.Critical_Chance =
            this.Critical_Bonus =
            this.Potency =
            this.Skill_Modifier =
            this.Avoidance =
            this.Resistance = 0;

            foreach (charecter_stat PrimeStat in this.PrimaryStats)
            {
                switch (PrimeStat.stat_id)
                {
                    case 1: // Strength
                        this.Strength += PrimeStat.amount;
                        break;
                    case 2: // Stamina
                        this.Stamina += PrimeStat.amount;
                        break;
                    case 3: // Agility
                        this.Agility += PrimeStat.amount;
                        break;
                    case 4: // Dexterity
                        this.Dexterity += PrimeStat.amount;
                        break;
                    case 5: // Intelligence
                        this.Intelligence += PrimeStat.amount;
                        break;
                    case 6: // Wisdom
                        this.Wisdom += PrimeStat.amount;
                        break;
                    case 7: // Melee atk dmg
                        this.Melee_Attack_Damage += PrimeStat.amount;
                        break;
                    case 8: // spell cast dmg
                        this.Spell_Cast_Damage += PrimeStat.amount;
                        break;
                    case 9: // melee multi atk
                        this.Melee_Multi_Attack += PrimeStat.amount;
                        break;
                    case 10: // spell multi atk
                        this.Spell_Multi_Attack += PrimeStat.amount;
                        break;
                    case 11: // crit chance
                        this.Critical_Chance += PrimeStat.amount;
                        break;
                    case 12: // crit bonus
                        this.Critical_Bonus += PrimeStat.amount;
                        break;
                    case 13: // Potency
                        this.Potency += PrimeStat.amount;
                        break;
                    case 14: // Skill modificer
                        this.Skill_Modifier += PrimeStat.amount;
                        break;
                    case 15: // Avoidance
                        this.Avoidance += PrimeStat.amount;
                        break;
                    case 16: // Resistance
                        this.Resistance += PrimeStat.amount;
                        break;
                    case 17: // Health
                        this.Health += PrimeStat.amount;
                        break;
                    case 18: // Power
                        this.Power += PrimeStat.amount;
                        break;
                    case 19: // Energy
                        this.Energy += PrimeStat.amount;
                        break;
                }
            }

            List<item_stat> AllEquippedItemsAndEnchantmentsStats = new List<item_stat>();

            foreach(charecter_item EquippedItem in this.EquippedItems)
            {
                List<item_stat> ItemStats = (from iss in db.item_stats
                                             where iss.item_id.Equals(EquippedItem.item_id)
                                             select iss).ToList();
                foreach(item_stat ItemStat in ItemStats)
                {
                    AllEquippedItemsAndEnchantmentsStats.Add(ItemStat);
                }

                List<charecter_item_enchantment> ItemEnchantments = (from cie in db.charecter_item_enchantments
                                                                     where cie.charecter_item_id.Equals(EquippedItem.id)
                                                                     select cie).ToList();

                foreach(charecter_item_enchantment ItemEnchantment in ItemEnchantments)
                {
                    List<item_stat> ItemEnchantmentStats = (from iss in db.item_stats
                                                            where iss.item_id.Equals(ItemEnchantment.item_id)
                                                            select iss).ToList();

                    foreach (item_stat ItemEnchantmentStat in ItemEnchantmentStats)
                    {
                        AllEquippedItemsAndEnchantmentsStats.Add(ItemEnchantmentStat);
                    }

                }
            }

            foreach (item_stat AllEquippedItemsAndEnchantmentsStat in AllEquippedItemsAndEnchantmentsStats)
            {
                switch (AllEquippedItemsAndEnchantmentsStat.stat_id)
                {
                    case 1: // Strength
                        this.Strength += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 2: // Stamina
                        this.Stamina += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 3: // Agility
                        this.Agility += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 4: // Dexterity
                        this.Dexterity += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 5: // Intelligence
                        this.Intelligence += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 6: // Wisdom
                        this.Wisdom += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 7: // Melee atk dmg
                        this.Melee_Attack_Damage += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 8: // spell cast dmg
                        this.Spell_Cast_Damage += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 9: // melee multi atk
                        this.Melee_Multi_Attack += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 10: // spell multi atk
                        this.Spell_Multi_Attack += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 11: // crit chance
                        this.Critical_Chance += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 12: // crit bonus
                        this.Critical_Bonus += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 13: // Potency
                        this.Potency += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 14: // Skill modificer
                        this.Skill_Modifier += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 15: // Avoidance
                        this.Avoidance += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 16: // Resistance
                        this.Resistance += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 17: // Health
                        this.Health += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 18: // Power
                        this.Power += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                    case 19: // Energy
                        this.Energy += AllEquippedItemsAndEnchantmentsStat.amount;
                        break;
                }
            }

            // Exstra Attributes Set
            int ExstraAttributes = Convert.ToInt32(Math.Round(this.Wisdom * 0.1, 0));
            this.Strength += ExstraAttributes;
            this.Stamina += ExstraAttributes;
            this.Agility += ExstraAttributes;
            this.Dexterity += (Convert.ToInt32(Math.Round(this.Agility * 0.5, 0)) + ExstraAttributes);
            this.Intelligence += ExstraAttributes;

            // Melee Attack Damage Calculater
            double Calc0 = (this.Strength * 0.02) + (this.Dexterity * 0.01);
            double Calc1 = (this.Melee_Attack_Damage / 100 * Calc0) + (this.Strength * 0.7);
            this.Melee_Attack_Damage += Convert.ToInt32(Math.Round(Calc1, 0));

            // Health Calculator
            this.Health += (this.Stamina * 10);

            // Energy Calculator
            double Calc2 = this.Stamina * 0.4;
            this.Energy += Convert.ToInt32(Math.Round(Calc2, 0));

            // Avoidance
            double Calc3 = Math.Round(this.Agility * 0.01);
            this.Avoidance += Convert.ToInt32(Calc3);
            

            // Melee Multi Calculator
            double Calc4 = Math.Round(this.Dexterity * 0.04, 0);
            this.Melee_Multi_Attack += Convert.ToInt32(Calc4);

            // Spell Multi Calculator
            double Calc5 = Math.Round(this.Dexterity * 0.04, 0);
            this.Spell_Multi_Attack += Convert.ToInt32(Calc5);

            // Spell Cast Damage Calculator
            double Calc6 = (this.Intelligence * 0.02) + (this.Dexterity * 0.01);
            double Calc7 = (this.Spell_Cast_Damage / 100 * Calc6) + (this.Intelligence * 0.7);
            this.Spell_Cast_Damage += Convert.ToInt32(Math.Round(Calc7, 0));

            // Resistance Calculator
            double Calc8 = Math.Round(this.Wisdom * 1.1, 0);
            double Calc9 = this.Resistance / 100 * (this.Wisdom * 0.01);
            this.Resistance += Convert.ToInt32(Calc8) + Convert.ToInt32(Math.Round(Calc9, 0));

            // Power
            double Calc10 = this.Intelligence * 1.1;
            this.Power += Convert.ToInt32(Math.Round(Calc10, 0));

            // MAX %
            if (this.Critical_Chance > 100) // MAX 100%
            {
                this.Critical_Chance = 100;
            }
            if (this.Avoidance > 50) // MAX 50%
            {
                this.Avoidance = 50;
            }
            if(this.Spell_Multi_Attack > 300) // MAX 300%
            {
                this.Spell_Multi_Attack = 300;
            }
            if (this.Melee_Multi_Attack > 300) // MAX 300%
            {
                this.Melee_Multi_Attack = 300;
            }
        }
        #endregion
    }
   
}