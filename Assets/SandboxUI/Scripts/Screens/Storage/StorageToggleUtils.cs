using Playgama.Modules.Storage;
using UnityEngine.UI;

namespace SandboxUI.Scripts.Screens.Storage
{
    public static class StorageToggleUtils
    {
        public static StorageType ConvertToggleToStorageType(Toggle toggle)
        {
            switch (toggle.name)
            {
                case "LocalStorage":
                    return StorageType.LocalStorage;
                case "PlatformInternal":
                    return StorageType.PlatformInternal;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }
    }
}