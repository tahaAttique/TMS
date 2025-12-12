using TMS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TMS.Permissions;

public class TMSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TMSPermissions.GroupName);

        var ticketCategoriesPermission = myGroup.AddPermission(TMSPermissions.TicketCategories.Default, L("Permission:TicketCategories"));
        ticketCategoriesPermission.AddChild(TMSPermissions.TicketCategories.Create, L("Permission:TicketCategories.Create"));
        ticketCategoriesPermission.AddChild(TMSPermissions.TicketCategories.Edit, L("Permission:TicketCategories.Edit"));
        ticketCategoriesPermission.AddChild(TMSPermissions.TicketCategories.Delete, L("Permission:TicketCategories.Delete"));

        var ticketsPermission = myGroup.AddPermission(TMSPermissions.Tickets.Default, L("Permission:Tickets"));
        ticketsPermission.AddChild(TMSPermissions.Tickets.Create, L("Permission:Tickets.Create"));
        ticketsPermission.AddChild(TMSPermissions.Tickets.Edit, L("Permission:Tickets.Edit"));
        ticketsPermission.AddChild(TMSPermissions.Tickets.Delete, L("Permission:Tickets.Delete"));
        ticketsPermission.AddChild(TMSPermissions.Tickets.Assign, L("Permission:Tickets.Assign"));

        var commentsPermission = myGroup.AddPermission(TMSPermissions.Comments.Default, L("Permission:Comments"));
        commentsPermission.AddChild(TMSPermissions.Comments.Create, L("Permission:Comments.Create"));
        commentsPermission.AddChild(TMSPermissions.Comments.Edit, L("Permission:Comments.Edit"));
        commentsPermission.AddChild(TMSPermissions.Comments.Delete, L("Permission:Comments.Delete"));
        commentsPermission.AddChild(TMSPermissions.Comments.Status, L("Permission:Comments.Status"));

        var notificationPermission = myGroup.AddPermission(TMSPermissions.Notifications.Default, L("Permission:Notifications"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TMSResource>(name);
    }
}
