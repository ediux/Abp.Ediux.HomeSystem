namespace Ediux.HomeSystem
{
    public static class HomeSystemDomainErrorCodes
    {
        /* You can add your business exception error codes here, as constants */
        public const string GeneralError = "HomeSystem:99999";
        public const string NoPermission = "HomeSystem:99998";
        public const string NoPermissionByName = "HomeSystem:99997";
        public const string CollectibleAssemblyLoadContext = "HomeSystem:99996";
        public const string LoadingAssemblyFailed = "HomeSystem:99995";
        public const string CouldNotGetModuleTypesFromAssembly = "HomeSystem:99994";
        public const string DataAlreadyExistsError = "HomeSystem:99501";
        public const string SpecifyDataItemAlreadyExistsError = "HomeSystem:99502";
        public const string CannotBeNullOrEmpty = "HomeSystem:99503";
        public const string DataMissing = "HomeSystem:99504";
        public const string DataMissingWithIdentity = "HomeSystem:99505";
        public const string DataNotFound = "HomeSystem:94040";
        public const string SpecifyDataItemNotFound = "HomeSystem:94041";
        public const string FileNotFoundInContainer = "HomeSystem:94042";
    }
}
