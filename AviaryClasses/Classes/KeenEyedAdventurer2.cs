using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;
using BlueprintCore.Utils;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Mechanics;


namespace AviaryClasses.Classes {
    /// <summary>
    /// New Magus class
    /// </summary>
    public class KeenEyedAdventurer2 {

        public static readonly string featName = "KeenEyedAdventurer2";
        public static readonly string featGuid =   "830ada44-825e-4e1e-9169-dba08b7d784a";


        public static void Configure() {

            CantripSpecialization.Configure();

            ArchetypeConfigurator archetype = ArchetypeConfigurator.New(featName, featGuid, CharacterClassRefs.WitchClass)
                .CopyFrom(ArchetypeRefs.RayMasterWitchArchetype);

            archetype.SetLocalizedName(featName + ".Name");

            //level 1
            archetype.AddToAddFeatures(1, CantripSpecialization.featGuid);
            archetype.AddToAddFeatures(4, FeatureSelectionRefs.AnimalCompanionSelectionSylvanSorcerer.ToString());
            archetype.AddToAddFeatures(5, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(6, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(7, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(8, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(9, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(10, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(11, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(12, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(13, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(14, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(15, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(16, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(17, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(18, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(19, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToAddFeatures(20, FeatureRefs.AnimalCompanionRank.ToString());
            archetype.AddToRemoveFeatures(20, CantripSpecialization.featGuid);

            archetype.Configure();

        }

        public class CantripSpecialization {

            private static readonly LogWrapper Logger = LogWrapper.Get("CantripSpecialization");

            public static readonly string featName = "CantripSpecialization";
            public static readonly string featGuid = "3ffc60b2-6cb3-4c97-9be9-f3e74b100d53";


            public static readonly string featureName = featName + ".Name";
            public static readonly string featureDescription = featName + ".Description";


            public static void Configure() {

                try {

                    BlueprintFeature baseFeature = BlueprintTool.Get<BlueprintFeature>(FeatureRefs.CantripMasteryFeature.ToString());
                    FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                    feature.SetDescription(featureDescription);
                    feature.SetDisplayName(featureName);
                    feature.SetIsClassFeature(true);
                    feature.SetIcon(baseFeature.m_Icon);

                    var statConfig = ContextRankConfigs.StatBonus(stat: StatType.Intelligence, type: AbilityRankType.DamageDice, max: 15, min: 0).WithBonusValueProgression(0);
                    feature.AddContextRankConfig(statConfig);

                    feature.AddRecalculateOnStatChange(null, ComponentMerge.Replace, StatType.Intelligence, useKineticistMainStat: false);

                    feature.AdditionalDiceOnDamage(
                    [AbilityRefs.Ignition.ToString(), AbilityRefs.RayOfFrost.ToString(), AbilityRefs.Jolt.ToString(), AbilityRefs.AcidSplash.ToString()],
                        abilityType: null,
                        applyCriticalModifier: true,
                        compareType: Kingmaker.UnitLogic.Mechanics.CompareOperation.Type.Equal,
                        checkWeaponType: false,
                        isOneAtack: false,
                        checkAbilityType: false,
                        checkSpellDescriptor: false,
                        checkEnergyDamageType: false,
                        energyType: Kingmaker.Enums.Damage.DamageEnergyType.Fire,
                        mainDamageTypeUse: true,
                        ignoreDamageFromThisFact: true
                    );

                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.m_DamageEntriesUse= Kingmaker.UnitLogic.Mechanics.Components.AdditionalDiceOnDamage.DamageEntriesUse.Simple);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.MainDamageTypeUse = true);

                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.Property=UnitProperty.StatBonusIntelligence);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.ValueType=Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.ValueRank=Kingmaker.Enums.AbilityRankType.DamageDice);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.ValueShared=Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.m_AbilityParameter=Kingmaker.UnitLogic.Mechanics.AbilityParameterType.Level);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.Value=0);

                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.ValueType=Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.ValueRank=Kingmaker.Enums.AbilityRankType.Default);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.ValueShared=Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.m_AbilityParameter=Kingmaker.UnitLogic.Mechanics.AbilityParameterType.Level);



                    feature.Configure();

                } catch (Exception ex) {
                    Logger.Error(ex.ToString());
                }

            }

        }
    }

}




                    // ContextValue cv = new ContextValue();
                    // cv.m_AbilityParameter = AbilityParameterType.CasterStatBonus;
                    // cv.Property = UnitProperty.StatBonusIntelligence;
                    // cv.ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage;
                    // cv.ValueType = ContextValueType.Rank;
                    // cv.ValueRank = AbilityRankType.DamageDice;

                    // feature.AddDiceDamageBonusOnSpell(
                    //     spells: [AbilityRefs.Ignition.ToString(), AbilityRefs.RayOfFrost.ToString(), AbilityRefs.Jolt.ToString(), AbilityRefs.AcidSplash.ToString()],
                    //     useContextBonus: true
                    // );

                    // feature.EditComponent<DiceDamageBonusOnSpell>(c => c.Value.ValueType= Kingmaker.UnitLogic.Mechanics.ContextValueType.);