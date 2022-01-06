using System;
using System.Diagnostics;
using System.Resources;

namespace Foustiga.WPF.Scheduler.Foundation.Enums
{
    public static class Localization
    {

        public static string GetResourceEntry(Type ResourceType, string ResourceKey)
        {
            if (ResourceKey == null) return null;
            try
            {
                ResourceManager _resourceManager = new ResourceManager(ResourceType);
                string display = _resourceManager.GetString(ResourceKey);
                if (display == null)
                {
                    Debug.WriteLine(string.Format("Static class Localization, Unable to get Resource Key '{0}' in Resource file '{1}'", ResourceKey, ResourceType));
                    return ResourceKey;
                }
                return string.IsNullOrWhiteSpace(display)
                    ? ResourceKey
                    : display;
            }
            catch (MissingManifestResourceException)
            {
                Debug.WriteLine(string.Format("Static class Localization, Unable to get Resource file '{0}' for Resource key '{1}'", ResourceType, ResourceKey));
                //Console.WriteLine("Static class Localization, Unable to get Resource file: {0}", ResourcesFile);
                return ResourceKey;
            }
            catch (Exception)
            {
                Debug.WriteLine(string.Format("Static class Localization, Unable to get Resource Key localized '{0}'", ResourceKey));
                //Console.WriteLine("Static class Localization, Unable to get Resource Key localized: {0}", ResourceValue);
                return ResourceKey;
            }

        }
    }
}
