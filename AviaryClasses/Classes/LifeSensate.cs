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
using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.Designers.Mechanics.Facts;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Utility;
using System;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
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

    public class LifeSensate {

        private static readonly LogWrapper Logger = LogWrapper.Get("LifeSensate");

        public static BlueprintArchetype archetypeRef;
        public static readonly string featName = "LifeSensate";
        public static readonly string featGuid = "9690f4ec-a4ad-4a9c-aaee-56251fbbea3c";


        public static void Configure() {

            try {

                SelectiveHealing.Configure();
                LifeBubble.Configure();
                LingeringEnergiesResource.Configure();
                LingeringEnergies.Configure();
                CorrectedSupercharge.Configure();
                HealingWildTalentSelection.Configure();
                EmpoweredHealing.Configure();
                LifeAttunement.Configure();
                LifeStudies.Configure();
                LifeFont.Configure();
                KineticHealerBuff.Configure();
                HealingBurstBuff.Configure();

                ArchetypeConfigurator archetype = ArchetypeConfigurator.New(LifeSensate.featName, LifeSensate.featGuid, CharacterClassRefs.KineticistClass);

                archetype.EditComponents<AddKineticistPart>(
                    c => c.MainStat = StatType.Intelligence,
                    x => true
                );

                archetype.SetBaseAttackBonus(StatProgressionRefs.BABMedium.ToString()); 
                archetype.SetFortitudeSave(StatProgressionRefs.SavesLow.ToString()); 
                archetype.SetReflexSave(StatProgressionRefs.SavesLow.ToString()); 
                archetype.SetWillSave(StatProgressionRefs.SavesHigh.ToString()); 

                archetype.SetLocalizedName(featName + ".Name");
                archetype.SetLocalizedDescription(featName + ".Description");
                archetype.AddPrerequisiteAlignment(AlignmentMaskType.TrueNeutral | AlignmentMaskType.Good, mergeBehavior: ComponentMerge.Merge);

                //level 1
                archetype.AddToAddFeatures(1, LifeAttunement.featGuid);
                archetype.AddToAddFeatures(1, LifeStudies.featGuid); 
                archetype.AddToAddFeatures(1, FeatureRefs.KineticHealerFeature.ToString());
                archetype.AddToAddFeatures(1, LingeringEnergies.featGuid);
                archetype.AddToRemoveFeatures(1, FeatureRefs.BurnFeature.ToString());
                archetype.AddToRemoveFeatures(1, ProgressionRefs.ElementalOverflowProgression.ToString());
                archetype.AddToRemoveFeatures(1, FeatureSelectionRefs.InfusionSelection.ToString());

                // level 2
                archetype.AddToAddFeatures(2, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToRemoveFeatures(2, FeatureSelectionRefs.WildTalentSelection.ToString());

                // level 3
                archetype.AddToAddFeatures(3, LifeFont.featGuid);
                archetype.AddToAddFeatures(3, FeatureRefs.HealingBUrstFeature.ToString());
                archetype.AddToRemoveFeatures(3, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToRemoveFeatures(3, FeatureRefs.ElementalOverflowBonusFeature.ToString());

                //level 4
                archetype.AddToAddFeatures(4, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToRemoveFeatures(4, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(4, FeatureSelectionRefs.AnimalCompanionSelectionDruid.ToString());

                //level 5
                archetype.AddToAddFeatures(5, FeatureRefs.OverwhelmingSoulMentalProwessAdditionalUseFeature.ToString());
                archetype.AddToRemoveFeatures(5, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(5, HealingWildTalentSelection.featGuid);
                archetype.AddToAddFeatures(5, FeatureRefs.AnimalCompanionRank.ToString());

                //level 6
                archetype.AddToAddFeatures(6, LifeFont.featGuid);
                archetype.AddToAddFeatures(6, EmpoweredHealing.featGuid);
                archetype.AddToRemoveFeatures(6, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(6, FeatureRefs.AnimalCompanionRank.ToString());

                //level 7
                archetype.AddToRemoveFeatures(7, FeatureSelectionRefs.SecondatyElementalFocusSelection.ToString());
                archetype.AddToAddFeatures(7, FeatureRefs.AnimalCompanionRank.ToString());

                //level 8
                archetype.AddToAddFeatures(8, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToRemoveFeatures(8, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(8, FeatureRefs.AnimalCompanionRank.ToString());

                //level 9
                archetype.AddToAddFeatures(9, LifeFont.featGuid);
                archetype.AddToRemoveFeatures(9, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(9, HealingWildTalentSelection.featGuid);
                archetype.AddToAddFeatures(9, FeatureRefs.AnimalCompanionRank.ToString());

                //level 10
                archetype.AddToAddFeatures(10, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(10, FeatureRefs.OverwhelmingSoulMentalProwessAdditionalUseFeature.ToString());
                archetype.AddToRemoveFeatures(10, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(10, FeatureRefs.KineticRevificationFeature.ToString());
                archetype.AddToAddFeatures(10, FeatureRefs.AnimalCompanionRank.ToString());

                //level 11
                archetype.AddToAddFeatures(11, CorrectedSupercharge.featGuid);
                archetype.AddToRemoveFeatures(11, FeatureRefs.Supercharge.ToString());
                archetype.AddToRemoveFeatures(11, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(11, FeatureRefs.AnimalCompanionRank.ToString());

                //level 12
                archetype.AddToAddFeatures(12, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(12, LifeFont.featGuid);
                archetype.AddToRemoveFeatures(12, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(12, FeatureRefs.AnimalCompanionRank.ToString());

                //level 13
                archetype.AddToRemoveFeatures(13, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(13, HealingWildTalentSelection.featGuid);
                archetype.AddToAddFeatures(13, FeatureRefs.AnimalCompanionRank.ToString());

                //level 14
                archetype.AddToAddFeatures(14, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(14, FeatureRefs.OverwhelmingSoulMentalProwessAdditionalUseFeature.ToString());
                archetype.AddToRemoveFeatures(14, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(14, FeatureRefs.AnimalCompanionRank.ToString());
                archetype.AddToAddFeatures(14, KineticHealerBuff.featGuid);

                //level 15
                archetype.AddToAddFeatures(15, LifeFont.featGuid);
                archetype.AddToAddFeatures(15, FeatureSelectionRefs.SecondatyElementalFocusSelection.ToString());
                archetype.AddToAddFeatures(15, FeatureSelectionRefs.WaterBlastSelection.ToString());
                archetype.AddToAddFeatures(15, FeatureRefs.AnimalCompanionRank.ToString());
                archetype.AddToRemoveFeatures(15, FeatureSelectionRefs.ThirdElementalFocusSelection.ToString());

                //level 16
                archetype.AddToAddFeatures(16, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToRemoveFeatures(16, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(16, FeatureRefs.AnimalCompanionRank.ToString());

                //level 17
                archetype.AddToRemoveFeatures(17, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(17, HealingWildTalentSelection.featGuid);
                archetype.AddToAddFeatures(17, FeatureRefs.AnimalCompanionRank.ToString());

                //level 18
                archetype.AddToAddFeatures(18, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(18, LifeFont.featGuid);
                archetype.AddToAddFeatures(18, FeatureRefs.OverwhelmingSoulMentalProwessAdditionalUseFeature.ToString());
                archetype.AddToRemoveFeatures(18, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(18, FeatureRefs.AnimalCompanionRank.ToString());
                archetype.AddToAddFeatures(18, HealingBurstBuff.featGuid);

                //level 19
                archetype.AddToRemoveFeatures(19, FeatureSelectionRefs.InfusionSelection.ToString());
                archetype.AddToAddFeatures(19, FeatureRefs.AnimalCompanionRank.ToString());

                //level 20
                archetype.AddToRemoveFeatures(20, FeatureSelectionRefs.WildTalentSelection.ToString());
                archetype.AddToAddFeatures(20, HealingWildTalentSelection.featGuid);
                archetype.AddToAddFeatures(20, FeatureRefs.AnimalCompanionRank.ToString());

                //Class Skills
                archetype.SetReplaceClassSkills(true);
                archetype.SetClassSkills(
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillPerception,
                    StatType.SkillUseMagicDevice
                );

                archetype.SetOverrideAttributeRecommendations(true);
                archetype.SetRecommendedAttributes(
                    StatType.Intelligence,
                    StatType.Dexterity
                );

                LifeSensate.archetypeRef = archetype.Configure();

                FeatureConfigurator.For(ProgressionRefs.ElementalFocusAir_0.ToString()).AddPrerequisiteNoArchetype(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString()).Configure();
                FeatureConfigurator.For(ProgressionRefs.ElementalFocusEarth_0.ToString()).AddPrerequisiteNoArchetype(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString()).Configure();
                FeatureConfigurator.For(ProgressionRefs.ElementalFocusFire_0.ToString()).AddPrerequisiteNoArchetype(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString()).Configure();

                FeatureConfigurator.For(ProgressionRefs.SecondaryElementAir.ToString()).AddPrerequisiteNoArchetype(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString()).Configure();
                FeatureConfigurator.For(ProgressionRefs.SecondaryElementEarth.ToString()).AddPrerequisiteNoArchetype(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString()).Configure();
                FeatureConfigurator.For(ProgressionRefs.SecondaryElementFire.ToString()).AddPrerequisiteNoArchetype(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString()).Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }
    }


    public class HealingWildTalentSelection {

        private static readonly LogWrapper Logger = LogWrapper.Get("HealingWildTalentSelection");

        public static readonly string featName = "HealingWildTalentSelection";
        public static readonly string featGuid = "2e71dd65-8606-4401-bc46-1e9708e71a18";


        public static void Configure() {

            try {

                FeatureSelectionConfigurator featureSelection = FeatureSelectionConfigurator.New(featName, featGuid)
                    .CopyFrom(FeatureSelectionRefs.WildTalentSelection);

                featureSelection.SetDisplayName("HealingWildTalentSelection.Name");

                featureSelection.ClearAllFeatures();
                featureSelection.AddToAllFeatures(FeatureRefs.KineticRestorationFeature.ToString());
                featureSelection.AddToAllFeatures(FeatureRefs.HealingBUrstFeature.ToString());
                featureSelection.AddToAllFeatures(FeatureRefs.KineticRevificationFeature.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.SkillFocusSelection.ToString());
                featureSelection.AddToAllFeatures(FeatureRefs.SkilledKineticistFeature.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.ElementalWhispersSelection.ToString());
                featureSelection.AddToAllFeatures(FeatureRefs.SlickFeature.ToString());
                featureSelection.AddToAllFeatures(LifeBubble.featGuid);
                //featureSelection.AddToAllFeatures(SelectiveHealing.featGuid);
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.WildTalentBonusFeatWater.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.WildTalentBonusFeatWater1.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.WildTalentBonusFeatWater2.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.WildTalentBonusFeatWater3.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.WildTalentBonusFeatWater4.ToString());
                featureSelection.AddToAllFeatures(FeatureSelectionRefs.WildTalentBonusFeatWater5.ToString());

                featureSelection.Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public class KineticHealerBuff {

        private static readonly LogWrapper Logger = LogWrapper.Get("KineticHealerBuff");

        public static readonly string featName = "KineticHealerBuff";
        public static readonly string featGuid = "c80abc3e-5cf9-484f-9a38-edb70699e1b5";


        public static void Configure() {

            try {

                FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                feature.SetDescription(featName + ".Description");
                feature.SetDisplayName(featName + ".Name");
                feature.SetIsClassFeature(true);
                feature.AddAutoMetamagic(
                    [
                        AbilityRefs.KineticHealerAbility.ToString(),
                        AbilityRefs.HealingBurstAbility.ToString()
                    ]
                    , null, null, null, null, null, 20, Metamagic.Maximize
                );
                feature.Configure();


                AbilityConfigurator.For(AbilityRefs.KineticHealerAbility.ToString())
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.DazeBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Stunned.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.BlindnessBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.GlitterdustBlindnessBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.DazzledBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.InsanityBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.FeeblemindBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Ecorche_Buff_SeizeSkin.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Confusion.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Fatigued.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Exhausted.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Nauseated.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.PoisonBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Sickened.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .RemoveBuffsByDescriptor(
                        new SpellDescriptorWrapper(SpellDescriptor.StatDebuff)
                    )
                )
                .Configure();
             
            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


        public class HealingBurstBuff {

        private static readonly LogWrapper Logger = LogWrapper.Get("HealingBurstBuff");

        public static readonly string featName = "HealingBurstBuff";
        public static readonly string featGuid = "71b4f75a-1d20-4653-89a5-90ce257d0cbc";


        public static void Configure() {

            try {

                FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                feature.SetDescription(featName + ".Description");
                feature.SetDisplayName(featName + ".Name");
                feature.SetIsClassFeature(true);
                feature.AddAutoMetamagic(
                    [
                        AbilityRefs.HealingBurstAbility.ToString()
                    ]
                    , null, null, null, null, null, 20, Metamagic.Maximize
                );
                feature.Configure();

                AbilityConfigurator.For(AbilityRefs.HealingBurstAbility.ToString())
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.DazeBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Stunned.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.BlindnessBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.GlitterdustBlindnessBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.DazzledBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.InsanityBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.FeeblemindBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Ecorche_Buff_SeizeSkin.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Confusion.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Fatigued.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Exhausted.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Nauseated.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.PoisonBuff.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New().RemoveBuff(BuffRefs.Sickened.ToString()))
                .AddAbilityEffectRunAction(ActionsBuilder.New()
                    .RemoveBuffsByDescriptor(
                        new SpellDescriptorWrapper(SpellDescriptor.StatDebuff)
                    )
                )
                .Configure();


            //"$ContextActionRemoveBuffsByDescriptor$0e050631-31d1-46dc-8ae9-f9f26fac1dec"
             
            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public class LifeStudies {

        private static readonly LogWrapper Logger = LogWrapper.Get("LifeStudies");

        public static readonly string featName = "LifeStudies";
        public static readonly string featGuid = "f05245c8-7233-4bad-ad10-9bd691f4e750";


        public static void Configure() {

            try {

                FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                if (feature != null) {

                    feature.SetDescription(featName + ".Description");
                    feature.SetDisplayName(featName + ".Name");
                    feature.SetIsClassFeature(true);

                    feature.Configure();

                }

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public class LifeBubble {

        private static readonly LogWrapper Logger = LogWrapper.Get("LifeBubble");

        public static readonly string featName = "LifeBubble";
        public static readonly string featGuid = "a09e1d2b-88f4-4378-8f34-a030cf9483c7";
        public static readonly string abilityName = "LifeBubbleAbility";
        public static readonly string abilityGuid = "6094a64d-8773-4e39-bddd-8da375bd3fd7";



        public static void Configure() {

            try {

                BlueprintAbility baseAbility = BlueprintTool.Get<BlueprintAbility>(AbilityRefs.ResistEnergyCommunal.ToString());

                BlueprintAbility fireAbility = AbilityConfigurator.New("LifeBubbleResistFire", "6552f952-523b-42ea-a94a-0209ea08a821").CopyFrom(AbilityRefs.ResistFireCommunal.ToString(), x => true)
                .SetDisplayName(featName + "Fire.Name")
                .SetDescription(featName + ".Description")
                .AddAbilityAcceptBurnOnCast(1)
                //.AddSpellLevelByClassLevel(clazz: CharacterClassRefs.KineticistClass.ToString())
                .Configure();

                BlueprintAbility acidAbility = AbilityConfigurator.New("LifeBubbleResistAcid", "ec3408c9-fda9-40d6-aa6b-72e10d053616").CopyFrom(AbilityRefs.ResistAcidCommunal.ToString(), x => true)
                .SetDisplayName(featName + "Acid.Name")
                .SetDescription(featName + ".Description")
                .AddAbilityAcceptBurnOnCast(1)
                .Configure();

                BlueprintAbility coldAbility = AbilityConfigurator.New("LifeBubbleResistCold", "025f3549-00cf-41a5-b14f-782a3bd54ca9").CopyFrom(AbilityRefs.ResistColdCommunal.ToString(), x => true)
                .SetDisplayName(featName + "Cold.Name")
                .SetDescription(featName + ".Description")
                .AddAbilityAcceptBurnOnCast(1)
                .Configure();

                BlueprintAbility elecAbility = AbilityConfigurator.New("LifeBubbleResistElec", "2b66f785-a2d9-4c1f-b4d3-3744f4fdd981").CopyFrom(AbilityRefs.ResistElectricityCommunal.ToString(), x => true)
                .SetDisplayName(featName + "Elec.Name")
                .SetDescription(featName + ".Description")
                .AddAbilityAcceptBurnOnCast(1)
                .Configure();

                BlueprintAbility sonicAbility = AbilityConfigurator.New("LifeBubbleResistSonic", "221c7859-e10e-4603-9413-fd1da916ef4a").CopyFrom(AbilityRefs.ResistSonicCommunal.ToString(), x => true)
                .SetDisplayName(featName + "Sonic.Name")
                .SetDescription(featName + ".Description")
                .AddAbilityAcceptBurnOnCast(1)
                .Configure();
     
                BlueprintAbility ability = AbilityConfigurator.New(abilityName, abilityGuid)
                .SetDisplayName(featName + ".Name")
                .SetDescription(featName + ".Description")
                .AddAbilityVariants([fireAbility, acidAbility, coldAbility, elecAbility, sonicAbility])
                .SetIcon(baseAbility.m_Icon)
                .Configure();

                FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid, FeatureGroup.KineticWildTalent);

                feature.SetDescription(featName + ".Description")
                .SetDisplayName(featName + ".Name")
                .SetIsClassFeature(true)
                .SetIcon(baseAbility.m_Icon)
                .AddKineticistAcceptBurnTrigger()
                .AddPrerequisiteArchetypeLevel(LifeSensate.featGuid, CharacterClassRefs.KineticistClass.ToString(), level: 5)
                .AddFacts(new() { ability })
                .Configure();


            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public class CorrectedSupercharge {

        private static readonly LogWrapper Logger = LogWrapper.Get("CorrectedSupercharge");

        public static readonly string featName = "Supercharge";
        public static readonly string featGuid = "f238fee8-e3a6-44bf-a1cb-b8feab911c35";
        public static readonly string featureDescription = "CorrectedSupercharge.Description";
        public static readonly string featureName = "CorrectedSupercharge.Name";


        public static void Configure() {

            try {

                FeatureConfigurator feature = Utils.CopyFromFeature(FeatureRefs.Supercharge.ToString(), featName, featGuid);

                if (feature != null) {

                    feature.SetDescription(featureDescription);
                    feature.SetDisplayName(featureName);

                    feature.Configure();

                }

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public class LifeFont {

        private static readonly LogWrapper Logger = LogWrapper.Get("LifeFont");

        public static readonly string featName = "LifeFont";
        public static readonly string featGuid = "6b05a12b-5dc9-4757-90bb-6dcd7a444cac";


        public static readonly string featureName = featName + ".Name";
        public static readonly string featureDescription = featName + ".Description";


        public static void Configure() {

            try {

                FeatureConfigurator feature = Utils.CopyFromFeature(FeatureRefs.OverwhelmingSoulOverwhelmingPowerFeature.ToString(), featName, featGuid);

                if (feature != null) {

                    feature.SetDescription(featureDescription);
                    feature.SetDisplayName(featureName);
                    feature.SetIsClassFeature(true);

                    feature.Configure();

                }

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }

    // Custom burn component based on Overwhelming Soul
    public class LifeAttunement {

        private static readonly LogWrapper Logger = LogWrapper.Get("LifeAttunement");

        public static readonly string featName = "LifeAttunement";
        public static readonly string featGuid = "f3503d52-5fa9-45dc-b0e0-1e8d7583ee41";


        public static readonly string featureName = featName + ".Name";
        public static readonly string featureDescription = featName + ".Description";


        public static void Configure() {

            try {

                FeatureConfigurator feature = Utils.CopyFromFeature(FeatureRefs.BurnFeature.ToString(), featName, featGuid);

                feature.SetDescription(featureDescription);
                feature.SetDisplayName(featureName);
                feature.SetIsClassFeature(true);

                feature.EditComponents<RecalculateOnStatChange>(
                    c => c.UseKineticistMainStat = false,
                    x => true
                );

                feature.EditComponents<RecalculateOnStatChange>(
                    c => c.Stat = StatType.Intelligence,
                    x => true
                );

                feature.EditComponents<AddKineticistPart>(
                    c => c.MainStat = StatType.Intelligence,
                    x => true
                );

                feature.Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }

    public class EmpoweredHealing {

        private static readonly LogWrapper Logger = LogWrapper.Get("EmpoweredHealing");

        public static readonly string featName = "EmpoweredHealing";
        public static readonly string featGuid = "53905737-9454-4e3a-bc42-38e21e4f5e52";


        public static void Configure() {

            try {

                FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                feature.SetDescription(featName + ".Description");
                feature.SetDisplayName(featName + ".Name");
                feature.SetIsClassFeature(true);
                feature.AddAutoMetamagic(
                    [
                        AbilityRefs.KineticHealerAbility.ToString(),
                        AbilityRefs.HealingBurstAbility.ToString()
                    ]
                    , null, null, null, null, null, 20, Metamagic.Empower
                );
                feature.Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public class SelectiveHealing {

        private static readonly LogWrapper Logger = LogWrapper.Get("SelectiveHealing");

        public static readonly string featName = "SelectiveHealing";
        public static readonly string featGuid = "400c95eb-cee3-4137-b334-43ea64ba3556";

        public static readonly string abilityName = "SelectiveHealingAbility";
        public static readonly string abilityGuid = "007be405-824d-44cd-8593-9f21bda60170";


        public static void Configure() {

            try {

                BlueprintFeature baseFeature = BlueprintTool.Get<BlueprintFeature>(FeatureRefs.SelectiveSpellFeat.ToString());

                BlueprintActivatableAbility ability = ActivatableAbilityConfigurator.New(abilityName, abilityGuid)
                .SetDisplayName(featName + ".Name")
                .SetDescription(featName + ".Description")
                .SetIcon(baseFeature.m_Icon)
                .AddAutoMetamagic(
                    [
                        AbilityRefs.HealingBurstAbility.ToString()
                    ]
                    ,null, null, null, null, null, 20, Metamagic.Selective
                )
                .Configure();


                FeatureConfigurator feature = FeatureConfigurator.New(featName, featGuid);

                feature.SetDescription(featName + ".Description");
                feature.SetDisplayName(featName + ".Name");
                feature.SetIcon (baseFeature.m_Icon);
                feature.SetIsClassFeature(true);
                feature.AddFacts(new() { ability });
                feature.Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

        }

    }


    public static class LingeringEnergiesResource {

        private static readonly LogWrapper Logger = LogWrapper.Get("LingeringEnergiesResource");

        public static BlueprintAbilityResource ResourcePool { get; private set; }

        // Resource
        public static readonly string featName = "LingeringEnergiesResource";
        public static readonly string featGuid = "94b6e209-0297-4d2e-892d-c131a11a1dab";

        public static void Configure() {

            try {

                LingeringEnergiesResource.ResourcePool = AbilityResourceConfigurator.New(
                    featName, // Unique internal name
                    featGuid // Unique GUID for the resource
                )
                .SetMaxAmount(
                    ResourceAmountBuilder.New(0).IncreaseByStat(StatType.Intelligence)
                )
                .SetUseMax(true) // Enforce max size
                .Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }


        }
    }


    public static class LingeringEnergies {

        private static readonly LogWrapper Logger = LogWrapper.Get("LingeringEnergies");

        
        public static BlueprintFeature Feature { get; private set; }

        //Feature
        public static readonly string featName = "LingeringEnergies";
        public static readonly string featGuid = "b72fba98-2aa8-4113-bbc2-600601c3e890";


        public static void Configure() {

            try {

                //Resource Assignment
                LingeringEnergies.Feature = FeatureConfigurator.New(
                    featName,
                    featGuid
                )
                .SetDisplayName(featName + ".Name")
                .SetDescription(featName + ".Description")
                .AddAbilityResources(
                    resource: LingeringEnergiesResource.ResourcePool.ToReference<BlueprintAbilityResourceReference>(),
                    restoreAmount: true // Restore the pool when resting
                ).AddRestTrigger (ActionsBuilder.New().HealBurn())
                .SetIsClassFeature(true)
                .Configure();

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }
        }

    }

}


  // public class LifeInfusion {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("LifeInfusion");

    //     public static BlueprintFeature featureRef;

    //     public static readonly string featName = "LifeInfusionFeature";
    //     public static readonly string featGuid = "ab36a0a8-480e-4a65-91dd-232e6a466853";

    //     public static readonly string abilityName = "LifeInfusionAbility";
    //     public static readonly string abililtyGuid = "01d9e260-3891-4ada-ab8f-ebd1246f0f7b";


    //     public static void Configure() {

    //         try {

    //             // AbilityConfigurator abilityConfig = Utils.CopyFromAbility(AbilityRefs.KineticHealerAbility.ToString(), abilityName, abililtyGuid);


    //             // BlueprintAbility baseFeature = BlueprintTool.Get<BlueprintAbility>(AbilityRefs.KineticHealerAbility.ToString());
    //             // List<BlueprintComponent> components = baseFeature.CollectComponents();

    //             // AbilityConfigurator feature = AbilityConfigurator.New("KNHealerInt", "01d9e260-3891-4ada-ab8f-ebd1246f0f7b").CopyFrom(AbilityRefs.KineticHealerAbility.ToString(), x => true);
    //             // abilityConfig.SetDisplayName("Kinetic Healer (Int)");
    //             // abilityConfig.EditComponents<ContextRankConfig>(c => c.m_Stat=StatType.Intelligence, x => x.m_Type==AbilityRankType.DamageBonus);
    //             // BlueprintAbility ability = abilityConfig.Configure();

    //             //                abilityConfig.RemoveComponents(c => c.GetType() == typeof(ContextRankConfig));


    //             // Logger.Info (ability.ToJson());
        
    //             // FeatureConfigurator featureConfig = Utils.CopyFromFeature(FeatureRefs.KineticHealerFeature.ToString(), featName, featGuid);
    //             // featureConfig.SetDisplayName(featName + ".Name");
    //             // featureConfig.SetGroups(FeatureGroup.KineticWildTalent);
    //             // featureConfig.EditComponent<AddFacts>(c => c.m_Facts = new BlueprintUnitFactReference[0]);
    //             // featureConfig.AddFacts(new() {ability});
    //             // featureConfig.SetHideInCharacterSheetAndLevelUp(true);
    //             // featureConfig.SetIsClassFeature(true);
    //             // LifeInfusion.featureRef = featureConfig.Configure();

    //         } catch (Exception ex) {
    //             Logger.Error(ex.ToString());
    //         }

    //     } 
    

    // }


// EventBus.RaiseEvent<IKineticistBurnValueHandler>(base.Owner.Unit, delegate(IKineticistBurnValueHandler h)
// {
// h.HandleKineticistBurnValueChanged(this, prev, null);
// });
// EventBus.RaiseEvent<IKineticistGlobalHandler>(delegate(IKineticistGlobalHandler h)
// {
// h.HandleKineticistBurnValueChanged(this, prev, null);
// }, true);



//  public class LifeSensateElementalFocusSelection {

//         private static readonly LogWrapper Logger = LogWrapper.Get("LifeSensateElementalFocusSelection");

//         public static readonly string featName = "LifeSensateElementalFocusSelection";
//         public static readonly string featGuid = "423221f1-3eec-4f41-bad4-bf52820ea93a";

//         public static readonly string baseFeatGuid = "1f3a15a3ae8a5524ab8b97f469bf4e3d";

//         public static readonly string featureDescription = "LifeSensateElementalFocusSelection.Description";


//         public static void Configure() {

//             try {

//                 FeatureSelectionConfigurator feature = FeatureSelectionConfigurator.New(featName, featGuid)
//                     .CopyFrom(FeatureSelectionRefs.ElementalFocusSelection);

//                 feature.SetDescription(featureDescription);
//                 feature.ClearAllFeatures();
//                 feature.AddToAllFeatures(ProgressionRefs.ElementalFocusWater_0.ToString());

//                 feature.Configure();

//                 Utils.AddIsPrequisiteFor(featGuid, "cb2d9e6355dd33940b2bef49e544b0bf");

//                 //FeatureConfigurator.For("cb2d9e6355dd33940b2bef49e544b0bf").AddPrerequisiteFeature(featGuid, ComponentMerge: ComponentMerge.Merge).Configure();
//                 //FeatureConfigurator.For("cb2d9e6355dd33940b2bef49e544b0bf").EditComponent<PrerequisiteFeature>(c => c. = true).AddPrerequisiteFeature(featGuid);

//             } catch (Exception ex) {
//                 Logger.Info(ex.ToString());
//             }

//         }

//     }



// BlueprintArchetypeReference addArchetype = BlueprintTool.GetRef<BlueprintArchetypeReference>(LifeSensate.featGuid);

// if (addArchetype == null) {
//     Logger.Error("Life Sensate ref was not found");
// } else {
//     Logger.Error("Life Sensate ref was found");
//     UnitPropertyConfigurator.For(UnitPropertyRefs.KineticSharpshooterQuiverProperty)
//         .EditComponent<ClassLevelGetter>(c => c.m_Archetype=addArchetype)
//         .
//         .Configure();

// }



    // //Buff Creation
    //             BuffConfigurator buff = Utils.MakeBuff(buffFeatGuid, buffFeatName, false);
    //             buff.SetIsClassFeature(true);
    //             buff.Configure();

    //             //Create Feature
    //             ActionsBuilder action = ActionsBuilder.New().ApplyBuffPermanent(buffFeatGuid, isNotDispelable: true);

    //             FeatureConfigurator feature = Utils.MakeFeature(featGuid, featName);
    //             feature.AddBuffActions(action);
    //             feature.AddFeatureTagsComponent(FeatureTag.ClassSpecific);
    //             feature.Configure();




                    //BlueprintArchetype darkArchetype = BlueprintTool.Get<BlueprintArchetype>(ArchetypeRefs.DarkElementalistArchetype.ToString());
                //UnitPartKineticist part = darkArchetype.GetComponent<UnitPartKineticist>();
                //part.m_Settings.MainStat = StatType.Intelligence;



                    // FeatureConfigurator.For(FeatureRefs.ExtendedRangeInfusion.ToString())
                    //     .EditComponent<PrerequisiteFeature>(c => c.m_Feature.)
                    //     .Configure();
                  
                    //BlueprintFeature baseFeature = BlueprintTool.Get<BlueprintFeature>(FeatureRefs.ExtendedRangeInfusion.ToString());
                    //baseFeature.Components.Remove<PrerequisiteFeature>(PrerequisiteFeature, c => ((PrerequisiteFeature)c).m_Feature = elementalFocusSelection);
                     //baseFeature.Components.RemoveAll( c => c.GetType() == typeof(PrerequisiteFeature));
    //             // List<BlueprintComponent> components = baseFeature.CollectComponents();





    //  public class TorrentFocusSelection {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("TorrentFocusSelection");

    //     public static readonly string featName = "TorrentFocusSelection";
    //     public static readonly string featGuid = "990fc568-9c0b-41e2-9764-1c6406370d81";


    //     public static void Configure() {

    //         try {

    //             FeatureConfigurator feature = Utils.CopyFromFeature(FeatureSelectionRefs.ElementalFocusSelection.ToString(), featName, featGuid);

    //             if (feature != null) {

    //                 feature.SetDescription(featName + ".Description");
    //                 feature.SetDisplayName(featName + ".Name");

    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.CloudInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.CycloneInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.ExtendedRangeInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.FragmentationInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.SpindleInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.SprayInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.WallInfusion.ToString());
    //                 feature.AddToIsPrerequisiteFor (FeatureRefs.TorrentInfusionFeature.ToString());

    //                 feature.Configure();

    //                 FeatureConfigurator.For(FeatureRefs.ExtendedRangeInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.CloudInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.CycloneInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.FragmentationInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.SpindleInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.SprayInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.WallInfusion.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.TorrentInfusionFeature.ToString()).RemoveComponents (c => c.GetType() == typeof(PrerequisiteFeature)).Configure();

    //                 FeatureConfigurator.For(FeatureRefs.ExtendedRangeInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.CloudInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.CycloneInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.FragmentationInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.SpindleInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.SprayInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.WallInfusion.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();
    //                 FeatureConfigurator.For(FeatureRefs.TorrentInfusionFeature.ToString()).AddPrerequisiteFeaturesFromList([featGuid, FeatureSelectionRefs.ElementalFocusSelection.ToString()]).Configure();

    //             }

    //         } catch (Exception ex) {
    //             Logger.Error(ex.ToString());
    //         }

    //     }

    // }

    // public class SecondTorrentFocusSelection {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("SecondTorrentFocusSelection");

    //     public static readonly string featName = "SecondTorrentFocusSelection";
    //     public static readonly string featGuid = "927a3e41-25cb-4fd2-b35c-5baed6065051";


    //     public static void Configure() {

    //         try {

    //             FeatureConfigurator feature = Utils.CopyFromFeature(FeatureSelectionRefs.SecondatyElementalFocusSelection.ToString(), featName, featGuid);

    //             if (feature != null) {

    //                 feature.SetDescription(featName + ".Description");
    //                 feature.SetDisplayName(featName + ".Name");

    //                 feature.Configure();

    //             }

    //         } catch (Exception ex) {
    //             Logger.Error(ex.ToString());
    //         }

    //     }

    // }