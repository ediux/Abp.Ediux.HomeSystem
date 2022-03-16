namespace Ediux.HomeSystem
{
    public static class HomeSystemDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
        public const string GeneralError = "HomeSystem:99999";
        public const string DataAlreadyExistsError = "HomeSystem:99501";
        public const string SpecifyDataItemAlreadyExistsError = "HomeSystem:99502";
        public const string CannotBeNullOrEmpty = "HomeSystem:99503";
        public const string DataMissing = "HomeSystem:99504";
        public const string DataMissingWithIdentity = "HomeSystem:99505";
        public const string DataNotFound = "HomeSystem:94040";
        public const string SpecifyDataItemNotFound = "HomeSystem:94041";
    }
}
