using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using UnityEngine;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using BlueprintCore.Blueprints.Configurators.UnitLogic.ActivatableAbilities;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Abilities;
using Kingmaker.Localization;
using Kingmaker.ResourceLinks;
using System.Reflection;
using BlueprintCore.Blueprints.CustomConfigurators;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Aviary;
using BlueprintCore.Blueprints.Configurators;


namespace AviaryClasses {

    public class Utils() {

        private static readonly LogWrapper Logger = LogWrapper.Get("AviaryClasses.Utils");


        public static AbilityConfigurator CopyFromAbility(string baseFeatureGuid, string featName, string featGuid) {

            AbilityConfigurator feature = null;

            try {

                feature = AbilityConfigurator.New(featName, featGuid).CopyFrom(baseFeatureGuid, x => true);

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return feature;

        }

        public static FeatureConfigurator CopyFromFeature(string baseFeatureGuid, string featName, string featGuid) {

            FeatureConfigurator feature = null;

            try {

                feature = FeatureConfigurator.New(featName, featGuid).CopyFrom(baseFeatureGuid, x => true);

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return feature;

        }

        public static BuffConfigurator MakeBuff(string guidStr, string systemName, bool hide = false, Sprite icon = null) {

            BuffConfigurator buff = null;

            try {

                Kingmaker.Blueprints.BlueprintGuid guid = BlueprintGuid.Parse(guidStr);

                buff = BuffConfigurator.New(systemName, guid.ToString());
                buff.SetDisplayName(systemName + ".Name");
                buff.SetDescription(systemName + ".Description");
                buff.SetFxOnStart(new Kingmaker.ResourceLinks.PrefabLink());
                buff.SetFxOnRemove(new Kingmaker.ResourceLinks.PrefabLink());

                if (icon != null) {
                    buff.SetIcon(icon);
                }

                if (hide) {
                    buff.SetFlags(BlueprintBuff.Flags.HiddenInUi);
                }

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return buff;

        }


        public static FeatureConfigurator MakeFeature(string guidStr, string systemName, bool hide = false, Sprite icon = null, params FeatureGroup[] groups) {

            FeatureConfigurator feature = null;

            try {

                Kingmaker.Blueprints.BlueprintGuid guid = BlueprintGuid.Parse(guidStr);

                feature = FeatureConfigurator.New(systemName, guid.ToString(), groups);

                feature.SetDisplayName(systemName + ".Name");
                feature.SetDescription(systemName + ".Description");

                if (hide) {
                    feature.SetHideInUI(true);
                    feature.SetHideInCharacterSheetAndLevelUp(true);
                }

                if (icon != null) {
                    feature.SetIcon(icon);
                }


            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return feature;

        }


        public static AbilityResourceConfigurator MakeResource(string guidStr, string systemName) {

            AbilityResourceConfigurator resource = null;

            try {

                BlueprintGuid guid = BlueprintGuid.Parse(guidStr);
                resource = AbilityResourceConfigurator.New(systemName, guid.ToString());

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return resource;

        }


        public static ActivatableAbilityConfigurator MakeToggle(string guidStr, string systemName, Sprite icon = null) {

            ActivatableAbilityConfigurator made = null;

            try {

                Kingmaker.Blueprints.BlueprintGuid guid = BlueprintGuid.Parse(guidStr);

                made = ActivatableAbilityConfigurator.New(systemName, guid.ToString());
                made.SetDisplayName(systemName + ".Name");
                made.SetDescription(systemName + ".Description");

                if (icon != null) {
                    made.SetIcon(icon);
                }

            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return made;

        }

        public static AbilityConfigurator MakeAbility(string guidStr, string systemName, Sprite icon, LocalizedString savestring, LocalizedString durationString) {

            AbilityConfigurator made = null;

            try {

                Kingmaker.Blueprints.BlueprintGuid guid = BlueprintGuid.Parse(guidStr);

                made = AbilityConfigurator.New(systemName, guid.ToString());
                made.SetDisplayName(systemName + ".Name");
                made.SetDescription(systemName + ".Description");

                if (icon != null) {
                    made.SetIcon(icon);
                }

                if (!String.IsNullOrEmpty(savestring)) {
                    made.SetLocalizedSavingThrow(savestring);
                }

                if (!String.IsNullOrEmpty(durationString)) {
                    made.SetLocalizedSavingThrow(durationString);
                }

                            } catch (Exception ex) {
                Logger.Error(ex.ToString());
            }

            return made;

        }

    }

}


                // BlueprintFeature baseFeature = BlueprintTool.Get<BlueprintFeature>(baseFeatureGuid);
                // BlueprintFeature clone = (Kingmaker.Blueprints.Classes.BlueprintFeature)ObjectDeepCopier.Clone(baseFeature);
                // clone.name = featName;
                // clone.AssetGuid = BlueprintGuid.Parse(featGuid);
                // feature = FeatureConfigurator.For(clone);




        // // CopyFromAbility to fix issues with CopyFrom in Blueprint Core
        // public static AbilityConfigurator CopyFromAbility2(string baseFeatureGuid, string featName, string featGuid) {
 
        //     AbilityConfigurator feature = null;

        //     try {

        //         BlueprintAbility baseFeature = BlueprintTool.Get<BlueprintAbility>(baseFeatureGuid);
        //         List<BlueprintComponent> components = baseFeature.CollectComponents();

        //         feature = AbilityConfigurator.New(featName, featGuid).CopyFrom(baseFeatureGuid, x => true);

        //     } catch (Exception ex) {
        //         Logger.Error(ex.ToString());
        //     }

        //     return feature;

        // }



        //         // CopyFromFeature to fix issues with CopyFrom in Blueprint Core
        // public static FeatureConfigurator CopyFromFeature(string baseFeatureGuid, string featName, string featGuid) {

        //     FeatureConfigurator feature = null;

        //     try {

        //         BlueprintFeature baseFeature = BlueprintTool.Get<BlueprintFeature>(baseFeatureGuid);
        //         List<BlueprintComponent> components = baseFeature.CollectComponents();

        //         feature = FeatureConfigurator.New(featName, featGuid).CopyFrom(baseFeatureGuid);

        //         foreach (BlueprintComponent c in components) {
        //             feature.AddComponent(c);
        //         }

        //     } catch (Exception ex) {
        //         Logger.Error(ex.ToString());
        //     }

        //     return feature;

        // }


