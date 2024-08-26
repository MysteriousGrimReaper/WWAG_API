using GGECS.Unity;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityPooler;

namespace WizGunBulletAPI
{
    public class Spell
    {
        public GameObject gameObject;
        public ECSGameObject spellGameObject;
        public ECSProjectileComponent spellProjectile;
        public ECSPositionComponent spellPosition;
        public ECSSimChunkEntityComponent spellSim;
        public ECSHeadingComponent spellHeading;
        public ECSOwnerComponent spellOwner;
        public PresentationLifetimeManager presentationLifetimeManager;
        public ECSFactionedComponent spellFaction;
        public ECSImpactorComponent spellImpactor;
        public ECSInterpolatePositionBehaviour spellInterpolator;
        public List<IImpactor> impactors;

        public string bulletType;
        public string bulletTier;
        public string upgradeTier;
        public Spell(String bulletType, String bulletTier, String upgradeTier, List<IImpactor> impactors) {
            this.bulletTier = bulletTier;
            this.upgradeTier = upgradeTier;
            this.bulletType = bulletType;
            this.impactors = impactors ?? new List<IImpactor>();
        }
        public void CreateUnityObject()
        {

            gameObject = new GameObject(string.Format("primary-{0}-{1}-{2}", bulletType.ToLower().Replace(" ", "-"), bulletTier, upgradeTier));
            spellGameObject = gameObject.AddComponent<ECSGameObject>();
            spellProjectile = gameObject.AddComponent<ECSProjectileComponent>();
            spellPosition = gameObject.AddComponent<ECSPositionComponent>();
            spellSim = gameObject.AddComponent<ECSSimChunkEntityComponent>();
            spellHeading = gameObject.AddComponent<ECSHeadingComponent>();
            spellOwner = gameObject.AddComponent<ECSOwnerComponent>();
            presentationLifetimeManager = gameObject.AddComponent<PresentationLifetimeManager>();
            gameObject.AddComponent<PrefabPool>();
            spellFaction = gameObject.AddComponent<ECSFactionedComponent>();
            spellImpactor = gameObject.AddComponent<ECSImpactorComponent>();
            spellInterpolator = gameObject.AddComponent<ECSInterpolatePositionBehaviour>();
            // gameObject.AddComponent<DamageImpactor>();
            WizGunBulletAPI.Log.LogInfo(string.Format("Added spell primary-{0}-{1}-{2}", bulletType.ToLower().Replace(" ", "-"), bulletTier, upgradeTier));
            /*
            impactors = impactors ?? new List<IImpactor>(); // Ensure impactors is not null
            foreach (var impactor in impactors)
            {
                if (impactor != null)
                {
                    Type type = impactor.GetType();
                    gameObject.AddComponent(type);
                }
                else
                {
                    WizGunBulletAPI.Log.LogError("impactor is null");
                }
            }
            */
        }
    }
}
