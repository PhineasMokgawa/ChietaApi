using CHIETAMIS.Debugging;

namespace CHIETAMIS
{
    public class CHIETAMISConsts
    {
        public const string LocalizationSourceName = "CHIETAMIS";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const bool AllowTenantsToChangeEmailSettings = false;

        public const string Currency = "Rands";

        public const string CurrencySign = "R";


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "e804f140beea4413950c314a5adcd846";
    }
}
