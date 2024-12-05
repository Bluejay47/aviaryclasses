using HarmonyLib;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Class.Kineticist;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UI.MVVM._VM.ActionBar;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UI.MVVM._PCView.ActionBar;
using Microsoft.Build.Utilities;
using System.ComponentModel;


namespace AviaryClasses.Classes {

    //Kingmaker.UI.MVVM._VM.ActionBar.KineticBurnVM
    //Token: 0x06005B27 RID: 23335 RVA: 0x00179146 File Offset: 0x00177346
    [HarmonyPatch(typeof(KineticBurnVM), "UpdateValues")]
    public static class UpdateBurnResourceIndicator {

        [HarmonyPostfix]
        public static void Postfix(KineticBurnVM __instance) {

            if (__instance.m_UnitPartKineticist != null) {

                //BlueprintArchetype archetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);
                UnitDescriptor owner = __instance.m_UnitPartKineticist.Owner;

                if (owner.Progression.IsArchetype(LifeSensate.archetypeRef)) {

                    UnitAbilityResource resource = __instance.m_UnitPartKineticist.Owner.Resources.GetResource(LingeringEnergiesResource.ResourcePool);

                    if (resource != null && resource.Amount > 0) {
                        __instance.AcceptedBurn.Value = -resource.Amount;
                        __instance.MaxBurn.Value = -resource.GetMaxAmount(__instance.m_UnitPartKineticist.Owner);
                    }

                }
            }
        }
    }

    [HarmonyPatch(typeof(UnitPartKineticist), "AcceptBurn")]
    public static class AcceptBurnPatch {

        [HarmonyPostfix]
        public static void Postfix(UnitPartKineticist __instance, int burn, AbilityData ability, ref int __result) {

            //BlueprintArchetype lifeSensate = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);

            if (ability.Caster.Progression.IsArchetype(LifeSensate.archetypeRef)) {

                UnitAbilityResource resource = ability.Caster.Resources.GetResource(LingeringEnergiesResource.ResourcePool);

                if (resource!= null && resource.Amount > 0) {

                    int healedAmount = Math.Min (burn, resource.Amount);
                    ability.Caster.Resources.Spend(LingeringEnergiesResource.ResourcePool, healedAmount);
                    __instance.HealBurn(healedAmount);

                }

                EventBus.RaiseEvent<IKineticistGlobalHandler>(delegate (IKineticistGlobalHandler h) {
                        h.HandleKineticistBurnValueChanged(__instance, 0, ability);
                }, true);     

            } 

        }

    }

}






//    // Check if the caster has a specific archetype
//             if (CalculateBurnCostPatchTools.applyLingeringEnergies(ability.Caster)) {

//                 Logger.Info("Caster has the specific archetype. Reducing burn cost.");

//                 var resourceAmount = ability.Caster.Resources.GetResourceAmount(LingeringEnergiesReourcePool.LifeSensateResource);
//                 if (resourceAmount >= __result.GetTotal(true)) {

//                     Logger.Info("Paying burn cost from LifeSensate resource pool.");
//                     ability.Caster.Resources.Spend(LingeringEnergiesReourcePool.LifeSensateResource, __result.GetTotal(true));
//                     __result = new KineticistAbilityBurnCost(0, 0, 0, 0); ; // Burn cost paid

//                 } else {
//                     Logger.Info($"Not enough resources to pay burn cost. Available: {resourceAmount}, Required: {__result}.");
//                 }

//             } else {
//                 Logger.Info("Caster does not have the specified archetype.");
//             }




// Logger.Info("Paying burn cost from LifeSensate resource pool. " + resource.Amount);
// caster.Resources.Spend(LingeringEnergiesResource.ResourcePool, __result.GetTotal(true));




    // //Kingmaker.UI.MVVM._PCView.ActionBar.KineticBurnPCView
    // //Token: 0x06005B27 RID: 23335 RVA: 0x00179146 File Offset: 0x00177346
    // [HarmonyPatch(typeof(KineticBurnPCView), "BindViewImplementation")]
    // public static class BindViewImplementationPatch {

    //     [HarmonyPostfix]
    //     public static void Postfix(KineticBurnPCView __instance) {
    //         __instance.m_Label.text = "Bonus: " + __instance.m_Label.text;
    //     }

    // }


    // [HarmonyPatch(typeof(AbilityKineticist), "IsAbilityRestrictionPassed", new Type[] { typeof(AbilityData) })]
    // public static class IsAbilityRestrictionPassedPatch {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("IsAbilityRestrictionPassedPatch");

    //     [HarmonyPrefix]
    //     public static bool Prefix(AbilityKineticist __instance, AbilityData ability, ref bool __result) {

    //         BlueprintArchetype lifeSensate = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);
    //         UnitDescriptor owner = ability.Caster;

    //         UnitPartKineticist unitPartKineticist = owner.Get<UnitPartKineticist>();
    //         int num = ((unitPartKineticist != null) ? __instance.CalculateCost(ability) : 0);

    //         if (owner.Progression.IsArchetype(lifeSensate)) {

    //             UnitAbilityResource resource = owner.Resources.GetResource(LingeringEnergiesResource.ResourcePool);
    //             __result = (resource.Amount >= num);

    //         } else {

    //             __result = unitPartKineticist && unitPartKineticist.MaxBurnPerRound >= num && unitPartKineticist.LeftBurnThisRound >= num;

    //         }

    //         return false;

    //     }

    // }

    // [HarmonyPatch(typeof(AbilityKineticist), "Spend", new Type[] { typeof(AbilityData) })]
    // public static class SpendPatch {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("SpendPatch");

    //     [HarmonyPrefix]
    //     public static bool Prefix(AbilityKineticist __instance, AbilityData ability) {

    //         SpendPathImpl.Handle(__instance, ability);
    //         return false;

    //     }

    // }


    // public static class SpendPathImpl {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("SpendPathImpl");

    //     public static void Handle(AbilityKineticist __instance, AbilityData ability) {

    //         Logger.Info("Handle Started");

    //         try {

    //             BlueprintArchetype lifeSensate = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);
    //             UnitDescriptor owner = ability.Caster;
    //             UnitPartKineticist caster = owner.Get<UnitPartKineticist>();

    //             if (!caster) {
    //                 Logger.Error(string.Format("Only kineticist can use this ability: ({0})", ability));
    //             } else {

    //                 Logger.Info("Calculating costs");
    //                 int total = __instance.CalculateBurnCost(ability).Total;
    //                 int num = (__instance.AllowOnlyBurnCost ? 0 : Math.Min(__instance.GetQuiverResource(caster), total));
    //                 int acceptedBurn = caster.AcceptBurn(total - num, ability);

    //                 Logger.Info("Accepted Burn " + acceptedBurn);

    //                 Logger.Info("Checking archetype ");

    //                 if (owner.Progression.IsArchetype(lifeSensate)) {

    //                     Logger.Info("Archetype matched");

    //                     Logger.Info("Starting resources " + owner.Resources.GetResource(LingeringEnergiesResource.ResourcePool).Amount);

    //                     owner.Resources.Spend(LingeringEnergiesResource.ResourcePool, acceptedBurn);
    //                     acceptedBurn = 0;

    //                     Logger.Info("Resource Spent");
    //                     Logger.Info("Remaining resources " + owner.Resources.GetResource(LingeringEnergiesResource.ResourcePool).Amount);

    //                 } else {
    //                     if (num > 0) {
    //                         __instance.SpendQuiver(caster, num);
    //                     }
    //                 }

    //                 EventBus.RaiseEvent<IKineticistAcceptBurnHandler>(caster.Owner.Unit, delegate (IKineticistAcceptBurnHandler h) {
    //                     h.HandleKineticistAcceptBurn(caster, acceptedBurn, ability);
    //                 });

    //                 //EventBus.RaiseEvent<IKineticistBurnValueHandler>(caster.Owner.Unit, delegate (IKineticistBurnValueHandler h) {
    //                 //    h.HandleKineticistBurnValueChanged(caster, acceptedBurn, ability);
    //                 //});

    //                 EventBus.RaiseEvent<IKineticistGlobalHandler>(delegate (IKineticistGlobalHandler h) {
    //                     h.HandleKineticistBurnValueChanged(caster, acceptedBurn, ability);
    //                 }, true);

    //             }

    //         } catch (Exception ex) {
    //             Logger.Error(ex.ToString());
    //         }

    //     }

    // }



    // [HarmonyPatch(typeof(AbilityAcceptBurnOnCast), "IsAbilityRestrictionPassed", new Type[] { typeof(AbilityData) })]
    // public static class IsAbilityRestrictionPassedPatch2 {

    //     [HarmonyPrefix]
    //     public static bool Prefix(AbilityAcceptBurnOnCast __instance, AbilityData ability, ref bool __result) {

    //         BlueprintArchetype lifeSensate = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);

    //         if (ability.Caster.Progression.IsArchetype(lifeSensate)) {
    //             __result = true; //__instance.BurnValue <= ability.Caster.Resources.GetResource(LingeringEnergiesResource.ResourcePool).Amount;
    //         } else {
    //             UnitPartKineticist unitPartKineticist = ability.Caster.Get<UnitPartKineticist>();
    //             __result = unitPartKineticist != null && __instance.BurnValue <= unitPartKineticist.LeftBurnThisRound;
    //         }

    //         return false;

    //     }

    // }


    // [HarmonyPatch(typeof(AbilityKineticist), "CalculateBurnCost", new Type[] { typeof(UnitDescriptor), typeof(BlueprintAbility) })]
    // public static class CalculateBurnCostBlueprintAbilityPatch {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("CalculateBurnCostAbilityDataPatch");

    //     [HarmonyPostfix]
    //     public static void Postfix(UnitDescriptor caster, BlueprintAbility abilityBlueprint, ref KineticistAbilityBurnCost __result) {

    //         // Ensure caster and ability are not null
    //         if (caster == null || abilityBlueprint == null) {
    //             Logger.Info("Caster or abilityBlueprint is null.");
    //             return;
    //         }

    //         if (CalculateBurnCostPatchTools.ApplyLingeringEnergies(caster, ref __result)) {
    //             Logger.Info("Sufficient LifeSensate resource pool.");
    //         } else {
    //             Logger.Info("Insufficient LifeSensate LifeSensate resource pool.");
    //         }

    //     }
    // }


    // [HarmonyPatch(typeof(AbilityKineticist), "CalculateBurnCost", new Type[] { typeof(AbilityData) })]
    // public static class CalculateBurnCostAbilityDataPatch {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("CalculateBurnCostAbilityDataPatch");

    //     [HarmonyPostfix]
    //     public static void Postfix(AbilityData ability, ref KineticistAbilityBurnCost __result) {

    //         // Ensure caster and ability are not null
    //         if (ability == null) {
    //             Logger.Info("Ability is null.");
    //             return;
    //         }

    //         if (CalculateBurnCostPatchTools.ApplyLingeringEnergies(ability.Caster, ref __result)) {
    //             Logger.Info("Sufficient LifeSensate resource pool.");
    //         } else {
    //             Logger.Info("Insufficient LifeSensate resource pool.");
    //         }

    //     }

    // }

    // static class CalculateBurnCostPatchTools {

    //     private static readonly LogWrapper Logger = LogWrapper.Get("CalculateBurnCostPatchTools");

    //     public static bool ApplyLingeringEnergies(UnitDescriptor caster, ref KineticistAbilityBurnCost __result) {

    //         bool result = false;

    //         try {

    //             if (caster == null) {

    //                 Logger.Info("Caster not found.");
    //                 result = false;

    //             } else {

    //                 BlueprintArchetype archetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);

    //                 if (archetype == null) {
    //                     Logger.Info("archetype not found.");
    //                 } else {

    //                     if (caster.Progression.IsArchetype(archetype)) {

    //                         Logger.Info("Caster has the specific archetype.");

    //                         if (caster.Resources == null) {
    //                             Logger.Info("caster.Resources is null");
    //                         } else {

    //                             UnitAbilityResource resource = caster.Resources.GetResource(LingeringEnergiesResource.ResourcePool);

    //                             if (resource == null) { 
    //                                 Logger.Info("Resource not found.");
    //                             } else {

    //                                 if (resource.Amount >= __result.GetTotal(true)) {
    //                                     __result = new KineticistAbilityBurnCost(0, 0, 0, 1);
    //                                     result = true;
    //                                 } else {
    //                                     Logger.Info($"Not enough resources to pay burn cost. Available: {resource.Amount}, Required: {__result.GetTotal(true)}.");
    //                                     result = false;
    //                                 }

    //                             } 

    //                         }

    //                     }

    //                 }

    //             }

    //         } catch (Exception ex) {
    //             Logger.Error(ex.ToString());
    //         }

    //         return result;

    //     }

    // }

    // Probably only adjusts cost estimtes of ability for display
    // [HarmonyPatch(typeof(AbilityKineticist), "CalculateCost", new Type[] { typeof(AbilityData) })]
    // public static class CalculateCostPatch {

    //     [HarmonyPrefix]
    //     public static bool Prefix(AbilityKineticist __instance, AbilityData ability, ref int __result) {

    //         BlueprintArchetype archetype = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);
    //         UnitDescriptor owner = ability.Caster;
    //         int lingeringEnergy = 0;

    //         if (owner.Progression.IsArchetype(archetype)) {
    //             UnitAbilityResource resource =  owner.Resources.GetResource(LingeringEnergiesResource.ResourcePool);
    //             lingeringEnergy = resource.Amount;

    //             __result = 0;

    //             return false;
    //         } else {
    //             return true;
    //         }

    //         // UnitPartKineticist unitPartKineticist = ability.Caster.Get<UnitPartKineticist>();
    //         // int num = (__instance.AllowOnlyBurnCost ? 0 : (__instance.GetQuiverResource(unitPartKineticist) + lingeringEnergy));
    //         // __result = Math.Max(0, __instance.CalculateBurnCost(ability).Total - num);

    //         // return false;

    //     }

    // }




    //   [HarmonyPatch(typeof(UnitPartKineticist), "AcceptBurn")]
    // public static class AcceptBurnPatch {

    //     [HarmonyPostfix]
    //     public static void Postfix(UnitPartKineticist __instance, int burn, AbilityData ability, ref int __result) {

    //         BlueprintArchetype lifeSensate = ResourcesLibrary.TryGetBlueprint<BlueprintArchetype>(LifeSensate.featGuid);

    //         if (ability.Caster.Progression.IsArchetype(lifeSensate)) {

    //             int burnApplied = burn;

    //             UnitAbilityResource resource = ability.Caster.Resources.GetResource(LingeringEnergiesResource.ResourcePool);

    //             if (resource!= null && resource.Amount > 0) {

    //                 if (burnApplied > resource.Amount) {
    //                     ability.Caster.Resources.Spend(LingeringEnergiesResource.ResourcePool, resource.Amount);
    //                     burnApplied = burnApplied - resource.Amount;
    //                 } else {
    //                     ability.Caster.Resources.Spend(LingeringEnergiesResource.ResourcePool, burnApplied);
    //                     burnApplied = 0;
    //                 }

    //                 EventBus.RaiseEvent<IKineticistGlobalHandler>(delegate (IKineticistGlobalHandler h) {
    //                      h.HandleKineticistBurnValueChanged(__instance, burnApplied, ability);
    //                 }, true);

    //             }

    //             int prev = __instance.AcceptedBurn;
    //             __instance.AcceptedBurn = Math.Min(__instance.MaxBurn, __instance.AcceptedBurn + burnApplied);

    //             if (ability.Caster.Unit.IsInCombat) {
    //                 __instance.AcceptedBurnThisRound += burnApplied;
    //             }

    //             Buff buff = ability.Caster.Buffs.GetBuff(__instance.m_Settings.GatherPowerBuff1);

    //             if (buff != null) {
    //                 buff.RemoveAfterDelay(default(TimeSpan));
    //             }

    //             Buff buff2 = ability.Caster.Buffs.GetBuff(__instance.m_Settings.GatherPowerBuff2);
    //             if (buff2 != null) {
    //                 buff2.RemoveAfterDelay(default(TimeSpan));
    //             }

    //             Buff buff3 = ability.Caster.Buffs.GetBuff(__instance.m_Settings.GatherPowerBuff3);
    //             if (buff3 != null) {
    //                 buff3.RemoveAfterDelay(default(TimeSpan));
    //             }

    //             __instance.GatherPowerRank = 0;

    //             if (prev != __instance.AcceptedBurn) {
    //                 EventBus.RaiseEvent<IKineticistBurnValueHandler>(ability.Caster.Unit, delegate (IKineticistBurnValueHandler h) {
    //                     h.HandleKineticistBurnValueChanged(__instance, prev, ability);
    //                 });
    //                 EventBus.RaiseEvent<IKineticistGlobalHandler>(delegate (IKineticistGlobalHandler h) {
    //                     h.HandleKineticistBurnValueChanged(__instance, prev, ability);
    //                 }, true);
    //             }

    //             __result = burnApplied;

    //         } 

    //     }

    // }