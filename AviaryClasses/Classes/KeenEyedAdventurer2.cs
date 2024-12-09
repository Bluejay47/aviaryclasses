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
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Conditions.Builder.ContextEx;
using BlueprintCore.Utils.Assets;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.ElementsSystem;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using BlueprintCore.Actions.Builder.BasicEx;
using Kingmaker.Enums.Damage;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;



namespace AviaryClasses.Classes {

    public class KeenEyedAdventurer2 {

        private static readonly LogWrapper Logger = LogWrapper.Get("KeenEyedAdventurer2");

        public static BlueprintArchetype archetypeRef;
        public static readonly string featName = "KeenEyedAdventurer2";
        public static readonly string featGuid = "830ada44-825e-4e1e-9169-dba08b7d784a";


        public static void Configure() {

            try {

                CantripSpecialization.Configure();
                OverpoweredCantrips.Configure();

                ArchetypeConfigurator archetype = ArchetypeConfigurator.New(featName, featGuid, CharacterClassRefs.WitchClass).CopyFrom(ArchetypeRefs.RayMasterWitchArchetype);

                archetype.SetLocalizedName(featName + ".Name");

                //level 1
                archetype.AddToAddFeatures(1, CantripSpecialization.featGuid);
                archetype.AddToAddFeatures(13, OverpoweredCantrips.featGuid);
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
                archetype.RemoveFromAddFeatures(20, [CantripSpecialization.featGuid]);

                //LevelEntryBuilder levelBuilder = new LevelEntryBuilder();


                archetypeRef = archetype.Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }


        public class OverpoweredCantrips {

            private static readonly LogWrapper Logger = LogWrapper.Get("OverpoweredCantrips");

            public static readonly string featName = "OverpoweredCantrips";
            public static readonly string featGuid = "171662c8-b5eb-45e7-9008-4443c6437022";

            public static readonly string buffName = "OverpoweredCantripsBuff";
            public static readonly string buffGuid = "1d4af546-8751-4cf6-8e14-3116a408ac53";


            public static readonly string abilityName = "OverpoweredCantripsAbility";
            public static readonly string abilityGuid = "9edbb2e0-caac-4d92-b6af-c26ad810c903";


            public static void Configure() {

                try {

                    BlueprintFeature baseAbility = BlueprintTool.Get<BlueprintFeature>(FeatureRefs.BolsteredSpellFeat.ToString());

                    //Overed Powered Cantrips Toggle Buff
                    BuffConfigurator.New(buffName, buffGuid)
                    .SetFlags(BlueprintBuff.Flags.HiddenInUi)
                    .Configure();

                    var toggle = ActivatableAbilityConfigurator.New(abilityName, abilityGuid)
                    .SetDisplayName(featName + ".Name")
                    .SetDescription(featName + ".Description")
                    .SetBuff(buffGuid)
                    .SetIcon(baseAbility.m_Icon)
                    .Configure();

                    FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                    feature.SetDescription(featName + ".Description")
                    .SetDisplayName(featName + ".Name")
                    .SetIsClassFeature(true)
                    .AddFacts(new() { toggle })
                    .SetIcon(baseAbility.m_Icon)
                    .AddFeatureTagsComponent(featureTags: FeatureTag.ClassSpecific | FeatureTag.Damage | FeatureTag.Magic | FeatureTag.Metamagic)
                    .AddDiceDamageBonusOnSpell(
                        spells: [AbilityRefs.Ignition.ToString(), AbilityRefs.RayOfFrost.ToString(), AbilityRefs.Jolt.ToString(), AbilityRefs.AcidSplash.ToString()],
                        value: 1
                    )
                    .Configure();


                    //Modification to Cantrips to Fire Additional Attacks
                    ContextDiceValue splashDice = new ContextDiceValue() {
                        DiceType = Kingmaker.RuleSystem.DiceType.D3,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            m_AbilityParameter = AbilityParameterType.Level
                        },
                        BonusValue = new ContextValue() {
                            ValueShared = AbilitySharedValue.Damage,
                            ValueRank = AbilityRankType.DamageDice,
                            m_AbilityParameter = AbilityParameterType.Level,
                            Property = UnitProperty.StatBonusIntelligence
                        },
                    };

                    ConditionsBuilder splashCondition = ConditionsBuilder.New()
                    .CasterHasFact(buffGuid)
                    .IsMainTarget()
                    .Build();

                    JoltSplashExt.Configure (splashDice, splashCondition);
                    IgnitionSplashExt.Configure (splashDice, splashCondition);
                    RayOfFrostSplashExt.Configure (splashDice, splashCondition);
                    AcidSplashSplashExt.Configure (splashDice, splashCondition);

                } catch (Exception ex) {
                    Logger.Error(ex.ToString());
                }

            }

        }


        internal class IgnitionSplashExt {

            public static void Configure(ContextDiceValue splashDice, ConditionsBuilder splashCondition) {

                ActionList internalSplashAction = ActionsBuilder.New()
                .Conditional(
                    conditions: ConditionsBuilder.New().IsMainTarget(),
                    ifFalse: ActionsBuilder.New().DealDamage(
                        new DamageTypeDescription() { 
                            Type = DamageType.Energy, 
                            Energy = DamageEnergyType.Fire, 
                        }, 
                        splashDice
                    )
                )
                .Build();

                ActionList splashAction = ActionsBuilder.New()
                .OnRandomTargetsAround(
                    actions: internalSplashAction,
                    onEnemies: true,
                    numberOfTargets: RandomUtils.GetRandomValue<int>([2,3]),
                    radius: 20.Feet()
                )
                .Build();

                ActionList newActions = ActionsBuilder.New()
                .Conditional(conditions: splashCondition, ifTrue: splashAction)
                .Build();

                AbilityConfigurator.For(AbilityRefs.Ignition.ToString())
                .EditComponent<AbilityEffectRunAction>(
                    c => c.Actions = new ActionList(newActions, c.Actions)
                )
                .Configure();

            }

        }


        internal class RayOfFrostSplashExt {

            public static void Configure(ContextDiceValue splashDice, ConditionsBuilder splashCondition) {

                ActionList internalSplashAction = ActionsBuilder.New()
                .Conditional(
                    conditions: ConditionsBuilder.New().IsMainTarget(),
                    ifFalse: ActionsBuilder.New().DealDamage(
                        new DamageTypeDescription() { 
                            Type = DamageType.Energy, 
                            Energy = DamageEnergyType.Cold, 
                        }, 
                        splashDice
                    )
                )
                .Build();

                ActionList splashAction = ActionsBuilder.New()
                .OnRandomTargetsAround(
                    actions: internalSplashAction,
                    onEnemies: true,
                    numberOfTargets: RandomUtils.GetRandomValue<int>([2,3]),
                    radius: 20.Feet()
                )
                .Build();

                ActionList newActions = ActionsBuilder.New()
                .Conditional(conditions: splashCondition, ifTrue: splashAction)
                .Build();

                AbilityConfigurator.For(AbilityRefs.RayOfFrost.ToString())
                .EditComponent<AbilityEffectRunAction>(
                    c => c.Actions = new ActionList(newActions, c.Actions)
                )
                .Configure();

            }

        }


        internal class AcidSplashSplashExt {

            public static void Configure(ContextDiceValue splashDice, ConditionsBuilder splashCondition) {

                ActionList internalSplashAction = ActionsBuilder.New()
                .Conditional(
                    conditions: ConditionsBuilder.New().IsMainTarget(),
                    ifFalse: ActionsBuilder.New().DealDamage(
                        new DamageTypeDescription() { 
                            Type = DamageType.Energy, 
                            Energy = DamageEnergyType.Acid, 
                        }, 
                        splashDice
                    )
                )
                .Build();

                ActionList splashAction = ActionsBuilder.New()
                .OnRandomTargetsAround(
                    actions: internalSplashAction,
                    onEnemies: true,
                    numberOfTargets: RandomUtils.GetRandomValue<int>([2,3]),
                    radius: 20.Feet()
                )
                .Build();

                ActionList newActions = ActionsBuilder.New()
                .Conditional(conditions: splashCondition, ifTrue: splashAction)
                .Build();

                AbilityConfigurator.For(AbilityRefs.AcidSplash.ToString())
                .EditComponent<AbilityEffectRunAction>(
                    c => c.Actions = new ActionList(newActions, c.Actions)
                )
                .Configure();

            }

        }


        internal class JoltSplashExt {

            public static void Configure(ContextDiceValue splashDice, ConditionsBuilder splashCondition) {

                ActionList internalSplashAction = ActionsBuilder.New()
                .Conditional(
                    conditions: ConditionsBuilder.New().IsMainTarget(),
                    ifFalse: ActionsBuilder.New().DealDamage(
                        new DamageTypeDescription() { 
                            Type = DamageType.Energy, 
                            Energy = DamageEnergyType.Electricity, 
                        }, 
                        splashDice
                    )
                )
                .Build();

                ActionList splashAction = ActionsBuilder.New()
                .OnRandomTargetsAround(
                    actions: internalSplashAction,
                    onEnemies: true,
                    numberOfTargets: RandomUtils.GetRandomValue<int>([2,3]),
                    radius: 20.Feet()
                )
                .Build();

                ActionList newActions = ActionsBuilder.New()
                .Conditional(conditions: splashCondition, ifTrue: splashAction)
                .Build();

                AbilityConfigurator.For(AbilityRefs.Jolt.ToString())
                .EditComponent<AbilityEffectRunAction>(
                    c => c.Actions = new ActionList(newActions, c.Actions)
                )
                .Configure();

            }

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

                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.m_DamageEntriesUse = Kingmaker.UnitLogic.Mechanics.Components.AdditionalDiceOnDamage.DamageEntriesUse.Simple);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.MainDamageTypeUse = true);

                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.Property = UnitProperty.StatBonusIntelligence);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.ValueRank = Kingmaker.Enums.AbilityRankType.DamageDice);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.m_AbilityParameter = Kingmaker.UnitLogic.Mechanics.AbilityParameterType.Level);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.DiceValue.BonusValue.Value = 0);

                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.ValueRank = Kingmaker.Enums.AbilityRankType.Default);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.Damage);
                    feature.EditComponent<AdditionalDiceOnDamage>(c => c.TargetValue.m_AbilityParameter = Kingmaker.UnitLogic.Mechanics.AbilityParameterType.Level);

                    feature.Configure();

                } catch (Exception ex) {
                    Logger.Error(ex.ToString());
                }

            }

        }
    }

}


// BlueprintAbility joltSplash = AbilityConfigurator.New("JoltSplash", "66e49193-67eb-491e-ad41-69beb84cefa5")
// .CopyFrom(AbilityRefs.Jolt, x => x.GetType())
// .Configure();

// // targer bugg
// BuffConfigurator.New("SplashTargetBuff", "2774b2ba-b0e1-4498-bf24-4668147bde2b")
// // //.SetFlags(BlueprintBuff.Flags.HiddenInUi)
// .Configure();


// // Apply target buff on cast
// BuffConfigurator.New("SplashTargetBuff", "2774b2ba-b0e1-4498-bf24-4668147bde2b")
// //.SetFlags(BlueprintBuff.Flags.HiddenInUi)
// .Configure();

// ActionList internalTargetBuffAction = ActionsBuilder.New()
// .ApplyBuffPermanent("2774b2ba-b0e1-4498-bf24-4668147bde2b", toCaster: false)
// .Build();

// AbilityConfigurator.For(AbilityRefs.Jolt.ToString())
// .EditComponent<AbilityEffectRunAction>(
//     c => c.Actions = new ActionList(internalTargetBuffAction, c.Actions)
// )
// .Configure();



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



// AbilityConfigurator.For(AbilityRefs.Jolt.ToString())
// .AddAbilityTargetsAround(
//     condition: ConditionsBuilder.New().HasBuff("1d4af546-8751-4cf6-8e14-3116a408ac53"),
//     includeDead: false,
//     radius: 20.Feet(),
//     targetType: TargetType.Enemy,
//     spreadSpeed: 25.Feet()
// )
// .Configure();


// .AddFactContextActions(
//     activated: ActionsBuilder.New().ApplyBuffPermanent("OverpoweredCantripsBuff", isNotDispelable: true),
//     deactivated: ActionsBuilder.New().RemoveBuff("OverpoweredCantripsBuff")
// )
// .Configure();


// BlueprintAbility splashCantrip = splashCantripConfig.Configure();

// FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

// if (feature != null) {

//     feature.SetDescription(featName + ".Description");
//     feature.SetDisplayName(featName + ".Name");
//     feature.SetIsClassFeature(true);
//     feature.AddFacts(new() {splashCantrip});

//     feature.Configure();

// }


// BlueprintActivatableAbility ability = ActivatableAbilityConfigurator.New(abilityName, abilityGuid)
// .SetDisplayName(featName + ".Name")
// .SetDescription(featName + ".Description")
// .AddFactContextActions(
//     activated: ActionsBuilder.New().OnRandomTargetsAround(
//         actions: ActionsBuilder.New().CastSpell(spell: AbilityRefs.Jolt.ToString(), castByTarget: true, markAsChild: true)
//     )
// )
// .Configure();



// .EditComponent<AbilityEffectRunAction>(c =>
//     c.Actions.Actions.Append<GameAction> (
//         ActionsBuilder.New()
//         .Conditional(
//             ConditionsBuilder.New().HasBuff("1d4af546-8751-4cf6-8e14-3116a408ac53"),
//             ifTrue: ActionsBuilder.New().OnRandomTargetsAround(
//                 actions: ActionsBuilder.New().CastSpell(spell: AbilityRefs.Jolt.ToString()),
//                 onEnemies: true,
//                 numberOfTargets: 3,
//                 radius: 20.Feet()
//             )
//         )
//         .Build()
//     )
// );




// AbilityConfigurator.For(AbilityRefs.Jolt.ToString())
// .AddFactContextActions(
//     activated: ActionsBuilder.New()
//     .Conditional(
//         ConditionsBuilder.New().HasBuff("1d4af546-8751-4cf6-8e14-3116a408ac53"),
//         ifTrue: ActionsBuilder.New().OnRandomTargetsAround(
//             actions: ActionsBuilder.New().CastSpell(spell: AbilityRefs.Jolt.ToString()),
//             onEnemies: true,
//             numberOfTargets: 3,
//             radius: 20.Feet()
//         )
//     )
// ).Configure();


// AbilityConfigurator splashCantripConfig = AbilityConfigurator.New(abilityName, abilityGuid).CopyFrom(AbilityRefs.Jolt.ToString(), x => true);

// splashCantripConfig.AddFactContextActions(); // ActionOnRandomTargetsAround

// splashCantripConfig.SetEffectOnAlly(AbilityEffectOnUnit.None);
// //splashCantripConfig.AddAbilityAoERadius(radius: 20.Feet());
// splashCantripConfig.AddAbilityTargetsAround(radius: 20.Feet(), spreadSpeed: 25.Feet(), targetType: Kingmaker.UnitLogic.Abilities.Components.TargetType.Enemy);
// //splashCantripConfig.AddAbilityDeliverProjectile();

// ContextRankConfig configAOE = new ContextRankConfig();
// configAOE.m_Type = Kingmaker.Enums.AbilityRankType.ProjectilesCount;
// configAOE.m_BaseValueType = ContextRankBaseValueType.StatBonus;
// configAOE.m_Stat = StatType.Intelligence;
// configAOE.m_Progression = ContextRankProgression.AsIs;
// configAOE.m_StepLevel = 1;
