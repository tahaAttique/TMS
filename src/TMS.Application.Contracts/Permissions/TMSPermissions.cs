namespace TMS.Permissions;

public static class TMSPermissions
{
    public const string GroupName = "TMS";
    
    public static class TicketCategories
    {
        public const string Default = GroupName + ".TicketCategories";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    } 
    
    public static class Tickets
    {
        public const string Default = GroupName + ".Tickets";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Assign = Default + ".Assign";
    }

    public static class Comments
    {
        public const string Default = GroupName + ".Comments";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string Status = Default + ".Status";
    }

    public static class  Notifications
    {
        public const string Default = GroupName + ".Notifications";
    }
}
