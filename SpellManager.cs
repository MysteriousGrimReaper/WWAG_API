using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizGunBulletAPI
{
    public class SpellManager
    {
        internal static List<Spell> spells = new List<Spell>();
        internal static List<RecipeSubCollection> schools = new List<RecipeSubCollection>();
        internal static List<RecipeList[]> recipeLists = new List<RecipeList[]>();

        public static Spell New(String bulletType, String bulletTier, String upgradeTier, List<IImpactor> impactors)
        {
            Spell spell = new Spell(bulletType, bulletTier, upgradeTier, impactors);
            spells.Add(spell);
            return spell;
        }
        public static void CreateUnityObjects()
        {
            spells.ForEach(delegate (Spell x)
            {
                x.CreateUnityObject();
            });
        }
    }
}
