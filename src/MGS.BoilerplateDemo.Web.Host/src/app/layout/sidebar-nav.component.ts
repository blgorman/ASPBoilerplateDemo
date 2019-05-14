import { Component, Injector, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MenuItem } from '@shared/layout/menu-item';

@Component({
    templateUrl: './sidebar-nav.component.html',
    selector: 'sidebar-nav',
    encapsulation: ViewEncapsulation.None
})
export class SideBarNavComponent extends AppComponentBase {

    menuItems: MenuItem[] = [
        new MenuItem(this.l('HomePage'), '', 'home', '/app/home'),

        new MenuItem(this.l('Tenants'), 'Pages.Tenants', 'business', '/app/tenants'),
        new MenuItem(this.l('Users'), 'Pages.Users', 'people', '/app/users'),
        new MenuItem(this.l('Roles'), 'Pages.Roles', 'local_offer', '/app/roles'),
        
        // Administration for the Host Level
        new MenuItem(this.l("AdministrationMenuGlobal"), "Administration", "menu", "", [
            new MenuItem(this.l("OptionListsMenuGlobal"), "Pages.Tenants.OptionLists", "", "/app/host/optionlists"),
            new MenuItem(this.l("OptionListItemsMenuGlobal"), "Pages.Tenants.OptionListItems", "", "/app/host/optionlistitems")
        ]),

        // Option list administration at the tenant level
        new MenuItem(this.l("AdministrationMenu"), "Pages.OptionLists", "menu", "", [
            new MenuItem(this.l("OptionListsMenu"), "Pages.OptionLists", "", "optionlists"),
            new MenuItem(this.l("OptionListItemsMenu"), "Pages.OptionListItems", "", "optionlistitems")
        ]),

        //Further menus can be added here as desired..
    ];

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    showMenuItem(menuItem): boolean {
        if (menuItem.permissionName) {
            return this.permission.isGranted(menuItem.permissionName);
        }

        return true;
    }
}
