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
using Kingmaker.UnitLogic.Parts;


namespace AviaryClasses.Classes {

    public class ArcaneSkirmisher {

        public static BlueprintArchetype archetypeRef;
        public static readonly string featName = "ArcaneSkirmisher";
        public static readonly string featGuid = "027480c6-f4ac-4846-984d-f6647b74e2fc";
        private static readonly LogWrapper Logger = LogWrapper.Get(featName);

        public static void Configure() {

            try {

                ArchetypeConfigurator archetype = ArchetypeConfigurator.New(ArcaneSkirmisher.featName, ArcaneSkirmisher.featGuid, CharacterClassRefs.MagusClass);

                archetype.SetLocalizedName(featName + ".Name");
                archetype.SetLocalizedDescription(featName + ".Description");

                //level 1
                archetype.AddToAddFeatures(1, FeatureSelectionRefs.ArcaneRiderMountSelection.ToString());
                archetype.AddToAddFeatures(1, FeatureRefs.ArcaneMountFeature.ToString());
                archetype.AddToAddFeatures(1, ProgressionRefs.ArcaneRiderMountProgression.ToString());
                archetype.AddToAddFeatures(1, FeatureRefs.EldritchArcherRangedSpellCombat.ToString());
                archetype.AddToAddFeatures(1, FeatureRefs.WeaponFocusShortbow.ToString());
                archetype.AddToRemoveFeatures(1, FeatureRefs.SpellCombatFeature.ToString());

                //level 2
                archetype.AddToAddFeatures(2, FeatureRefs.EldritchArcherRangedSpellStrike.ToString());
                archetype.AddToRemoveFeatures(2, FeatureRefs.SpellStrikeFeature.ToString());

                // level 4
                archetype.AddToRemoveFeatures(4, FeatureRefs.MagusSpellRecallFeature.ToString());

                // level 5
                archetype.AddToRemoveFeatures(5, FeatureSelectionRefs.MagusFeatSelection.ToString());
                archetype.AddToAddFeatures(5, FeatureRefs.WeaponFocusGreaterShortbow.ToString());

                // level 7
                archetype.AddToRemoveFeatures(7, FeatureRefs.ArcaneMediumArmor.ToString());

                // //level 8
                archetype.AddToAddFeatures(8, FeatureRefs.DimensionalRideFeature.ToString());

                // level 10
                archetype.AddToRemoveFeatures(10, FeatureRefs.FighterTraining.ToString());

                // //level 11
                archetype.AddToAddFeatures(11, FeatureRefs.WeaponSpecializationShortbow.ToString());
                archetype.AddToRemoveFeatures(11, FeatureRefs.MagusImprovedSpellRecallFeature.ToString());
                archetype.AddToRemoveFeatures(11, FeatureSelectionRefs.MagusFeatSelection.ToString());

                // //level 13
                archetype.AddToRemoveFeatures(13, FeatureRefs.ArcaneHeavyArmor.ToString());

                // //level 14
                archetype.AddToAddFeatures(14, FeatureRefs.GreaterDimensionalRideFeature.ToString());

                // //level 16
                archetype.AddToRemoveFeatures(16, FeatureRefs.Counterstrike.ToString());

                // //level 17
                archetype.AddToAddFeatures(17, FeatureRefs.WeaponSpecializationGreaterShortbow.ToString());
                archetype.AddToRemoveFeatures(17, FeatureSelectionRefs.MagusFeatSelection.ToString());

                //Class Skills
                archetype.SetReplaceClassSkills(true);
                archetype.SetClassSkills(
                    StatType.SkillLoreNature,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillPerception,
                    StatType.SkillMobility
                );

                archetype.SetOverrideAttributeRecommendations(true);
                archetype.SetRecommendedAttributes(
                    StatType.Intelligence,
                    StatType.Dexterity
                );

                ArcaneSkirmisher.archetypeRef = archetype.Configure();

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