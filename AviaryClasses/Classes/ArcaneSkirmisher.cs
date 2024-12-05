
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using Kingmaker.EntitySystem.Stats;


namespace AviaryClasses.Classes {
    /// <summary>
    /// New Magus class
    /// </summary>
    public class ArcaneSkirmisher {

        public static readonly string FeatName = "ArcaneSkirmisher";
        public static readonly string FeatGuid =   "027480c6-f4ac-4846-984d-f6647b74e2fc";
        private static readonly string ArchetypeName = "ArcaneSkirmisher.Name";
        private static readonly string ArchetypeDescription = "ArcaneSkirmisher.Description";


        public static void Configure() {

            ArchetypeConfigurator archetype = ArchetypeConfigurator.New(FeatName, FeatGuid, CharacterClassRefs.MagusClass);

            archetype.SetLocalizedName(ArchetypeName);
            archetype.SetLocalizedDescription(ArchetypeDescription);

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

            //level 4
            archetype.AddToRemoveFeatures(4, FeatureRefs.MagusSpellRecallFeature.ToString());

            //level 5
            archetype.AddToRemoveFeatures(5, FeatureSelectionRefs.MagusFeatSelection.ToString());
            archetype.AddToAddFeatures(5, FeatureRefs.WeaponFocusGreaterShortbow.ToString());

            //level 7
            archetype.AddToRemoveFeatures(7, FeatureRefs.ArcaneMediumArmor.ToString());

            //level 8
            archetype.AddToAddFeatures(8, FeatureRefs.DimensionalRideFeature.ToString());

            //level 10
            archetype.AddToRemoveFeatures(10, FeatureRefs.FighterTraining.ToString());

            //level 11
            archetype.AddToAddFeatures(11, FeatureRefs.WeaponSpecializationShortbow.ToString());
            archetype.AddToRemoveFeatures(11, FeatureRefs.MagusImprovedSpellRecallFeature.ToString());
            archetype.AddToRemoveFeatures(11, FeatureSelectionRefs.MagusFeatSelection.ToString());

            //level 13
            archetype.AddToRemoveFeatures(13, FeatureRefs.ArcaneHeavyArmor.ToString());

            //level 14
            archetype.AddToAddFeatures(14, FeatureRefs.GreaterDimensionalRideFeature.ToString());

            //level 16
            archetype.AddToRemoveFeatures(16, FeatureRefs.Counterstrike.ToString());

            //level 17
            archetype.AddToAddFeatures(17, FeatureRefs.WeaponSpecializationGreaterShortbow.ToString());
            archetype.AddToRemoveFeatures(17, FeatureRefs.MagusImprovedSpellRecallFeature.ToString());
            archetype.AddToRemoveFeatures(17, FeatureSelectionRefs.MagusFeatSelection.ToString());

            //Class Skills
            archetype.ClearClassSkills();
            archetype.AddToClassSkills(
                StatType.SkillLoreNature,
                StatType.SkillKnowledgeWorld,
                StatType.SkillKnowledgeArcana,
                StatType.SkillPerception,
                StatType.SkillMobility
            );

            archetype.ClearRecommendedAttributes();
            archetype.AddToRecommendedAttributes(
                StatType.Intelligence,
                StatType.Dexterity
            );

            archetype.Configure();

        }
    }
}



