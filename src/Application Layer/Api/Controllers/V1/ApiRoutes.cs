namespace NederlandseLoterij.KrasLoterij.Api.Controllers.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

        public const string Get = Base + "/Get";
        public const string IsScratchedByUser = Base + "/IsScratchedByUser";
        public const string Scratch = Base + "/Scratch";
    }
}