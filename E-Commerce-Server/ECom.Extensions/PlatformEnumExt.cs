using ECom.Constants;

namespace ECom.Extensions
{
    public static class PlatformEnumExt
    {
        public static string GetPlatformString(this DataEnums.Platform platform)
        {
            return platform switch
            {
                DataEnums.Platform.Console => "Console",
                DataEnums.Platform.PC => "PC",
                DataEnums.Platform.Mobile => "Mobile",
                DataEnums.Platform.VR => "VR",
                DataEnums.Platform.Web => "Web",
                _ => throw new NotImplementedException()
            };
        }
    }
}
