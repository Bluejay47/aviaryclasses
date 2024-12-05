
using BlueprintCore.Utils;
using BlueprintCore.Blueprints.Configurators.Items;


namespace AviaryClasses.Items {

    /// <summary>
    /// Mods the TriceratopsStatuetteItem to have a permanent ability grant
    /// </summary>
    public class TriceratopsStatuetteItemAlt {

        private static readonly LogWrapper logger = LogWrapper.Get("TriceratopsStatuetteItemAlt");

        public static readonly string FeatName = "TriceratopsStatuetteItem";
        public static readonly string FeatGuid = "d8d32918de5e11246a048269e7e0bbb9";

        public static void Configure() {


            ItemConfigurator statue = ItemConfigurator.For(FeatGuid);
            statue.SetDescriptionText("TriceratopsStatuetteItem.Description");

            //statue.RemoveComponents(c => c is RestrictionHasFact);

            //statue.AddActivateTrigger(ActionsBuilder actions = null, bool? alsoOnAreaLoad = null, ConditionsBuilder conditions = null, bool? once = null)

            statue.Configure();

        }
    }
}