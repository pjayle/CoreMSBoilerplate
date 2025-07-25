namespace gumfa.Web.Utility
{
    public class CONST
    {
        public enum ApiType
        {
            GET, POST, PUT, DELETE
        }
        public enum ContentType
        {
            Json, MultipartFormData
        }

        public static string ProductAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public const string RoleSuperAdmin = "SUPERADMIN";
        public const string RoleAdmin = "ADMIN";
        public const string RoleSupervisor = "SUPERVISOR";
        public const string RoleOperator = "OPERATOR";

        public const string TokenCookie = "JWTToken";

        public const string Status_Pending = "Pending";
        public const string Status_Approved = "Approved";
        public const string Status_ReadyForPickup = "ReadyForPickup";
        public const string Status_Completed = "Completed";
        public const string Status_Refunded = "Refunded";
        public const string Status_Cancelled = "Cancelled";
    }
}
