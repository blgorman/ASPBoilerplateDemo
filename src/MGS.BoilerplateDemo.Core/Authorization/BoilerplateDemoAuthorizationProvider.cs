using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace MGS.BoilerplateDemo.Authorization
{
    public class BoilerplateDemoAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("Pages"));
            var administration = context.GetPermissionOrNull(PermissionNames.Administration) ?? context.CreatePermission(PermissionNames.Administration, L("Administration"), multiTenancySides: MultiTenancySides.Host);
            var administrationOnTenant = context.GetPermissionOrNull(PermissionNames.AdministrationOnTenant) ?? context.CreatePermission(PermissionNames.AdministrationOnTenant, L("Administration"), multiTenancySides: MultiTenancySides.Tenant);

            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var hostOptionLists = pages.CreateChildPermission(PermissionNames.Pages_Tenants_OptionLists, L("OptionListsAll"), multiTenancySides: MultiTenancySides.Host);
            hostOptionLists.CreateChildPermission(PermissionNames.Pages_Tenants_OptionLists_Create, L("OptionListsCreateAll"), multiTenancySides: MultiTenancySides.Host);
            hostOptionLists.CreateChildPermission(PermissionNames.Pages_Tenants_OptionLists_Update, L("OptionListsUpdateAll"), multiTenancySides: MultiTenancySides.Host);
            hostOptionLists.CreateChildPermission(PermissionNames.Pages_Tenants_OptionLists_Delete, L("OptionListsDeleteAll"), multiTenancySides: MultiTenancySides.Host);

            var hostOptionListItems = pages.CreateChildPermission(PermissionNames.Pages_Tenants_OptionListItems, L("OptionListItemsAll"), multiTenancySides: MultiTenancySides.Host);
            hostOptionListItems.CreateChildPermission(PermissionNames.Pages_Tenants_OptionListItems_Create, L("OptionListItemsCreateAll"), multiTenancySides: MultiTenancySides.Host);
            hostOptionListItems.CreateChildPermission(PermissionNames.Pages_Tenants_OptionListItems_Update, L("OptionListItemsUpdateAll"), multiTenancySides: MultiTenancySides.Host);
            hostOptionListItems.CreateChildPermission(PermissionNames.Pages_Tenants_OptionListItems_Delete, L("OptionListItemsDeleteAll"), multiTenancySides: MultiTenancySides.Host);

            var tenantOptionLists = pages.CreateChildPermission(PermissionNames.Pages_OptionLists, L("OptionLists"), multiTenancySides: MultiTenancySides.Tenant);
            tenantOptionLists.CreateChildPermission(PermissionNames.Pages_OptionLists_Create, L("OptionListsCreate"), multiTenancySides: MultiTenancySides.Tenant);
            tenantOptionLists.CreateChildPermission(PermissionNames.Pages_OptionLists_Update, L("OptionListsUpdate"), multiTenancySides: MultiTenancySides.Tenant);
            tenantOptionLists.CreateChildPermission(PermissionNames.Pages_OptionLists_Delete, L("OptionListsDelete"), multiTenancySides: MultiTenancySides.Tenant);

            var tenantOptionListItems = pages.CreateChildPermission(PermissionNames.Pages_OptionListItems, L("OptionListItems"), multiTenancySides: MultiTenancySides.Tenant);
            tenantOptionListItems.CreateChildPermission(PermissionNames.Pages_OptionListItems_Create, L("OptionListItemsCreate"), multiTenancySides: MultiTenancySides.Tenant);
            tenantOptionListItems.CreateChildPermission(PermissionNames.Pages_OptionListItems_Update, L("OptionListItemsUpdate"), multiTenancySides: MultiTenancySides.Tenant);
            tenantOptionListItems.CreateChildPermission(PermissionNames.Pages_OptionListItems_Delete, L("OptionListItemsDelete"), multiTenancySides: MultiTenancySides.Tenant);

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BoilerplateDemoConsts.LocalizationSourceName);
        }
    }
}
