using Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;


namespace WizGunBulletAPI
{
    public class BulletManager
    {
        internal static List<Bullet> bullets = new List<Bullet>();
        internal static List<GameObject> bulletGameObjects { get { return bullets.Select(x => x.gameObject).ToList(); } }
        internal static List<RecipeSubCollection> schools = new List<RecipeSubCollection>();
        internal static List<RecipeList[]> recipeLists = new List<RecipeList[]>();
        internal static List<RecipeBase> bulletRecipeBases { get { return bullets.Where(x => x.recipe != null).Select(x => x.recipe).ToList<RecipeBase>(); } }
        internal static List<CraftingRecipe> bulletRecipes
        {
            get { return bullets.Where(x => x.recipe != null).Select(x => x.recipe).ToList(); }
        }
        public static Bullet New(string bulletType, string bulletTier, string itemDescription, List<IImpactor> impactorList, string school = "sorcerer", int upgradeLevels = 1)
        {
            Bullet bullet = new Bullet(bulletType, bulletTier, itemDescription, impactorList, school, upgradeLevels);
            bullets.Add(bullet);
            return bullet;
        }
        
        public static RecipeSubCollection New(string schoolName, Sprite iconOn, Sprite iconOff, RecipeList[] bulletRecipes, string categoryName = "")
        {
            string bString = categoryName;
            if (categoryName == "")
            {
                bString = schoolName + " Bullets";
            }
            WizGunBulletAPI.Log.LogInfo(string.Format("Adding school {0}", schoolName));
            RecipeSubCollection school = new RecipeSubCollection
            {
                categoryName = new DisplayString { backupString = bString },
                collectionIcon = iconOn,
                collectionIconOff = iconOff,
                collections = bulletRecipes
            };
            schools.Add(school);
            return school;
        }
        public static void CreateUnityObjects()
        {
            bullets.ForEach(delegate (Bullet x)
            {
                x.CreateUnityObject();
            });
            /*
            spells.ForEach(delegate (Spell s)
            {
                s.CreateSpellObject();
            });
            */
        }
        public static RecipeList AddBulletsToRecipeLists()
        {
            RecipeList bulletRecipeList = new RecipeList()
            {
                listName = new DisplayString
                {
                    backupString = "Modded Bullets"
                },
                collection = bullets.Select(b => b.recipe).ToArray()
            };
            // edit this to be able to define own lists later
            /*
            foreach (Bullet b in BulletManager.bullets)
            {
                RecipeList[] matchingRecipeList = BulletManager.recipeLists.Find(l => l.listName.backupString);
            }
            */
            return bulletRecipeList;
        }

        
    }
};
