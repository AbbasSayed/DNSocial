namespace DNSocial.Api
{
    public static class ApiRouts
    {
        public const string BaseRoute = "api/v{version:apiVersion}/[controller]";

        public static class UserProfiles
        {
            public const string IdRoute = "{id}";
        }

        public static class Identity
        {
            public const string Login = "login";

            public const string Registration = "registration";
        }
    }
}
