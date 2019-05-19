import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRouteGuard } from '@shared/auth/auth-route-guard';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { TenantsComponent } from './tenants/tenants.component';
import { RolesComponent } from 'app/roles/roles.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { OptionListItemsComponent } from './optionlistitems/optionlistitems.component';
import { OptionListsComponent } from './optionlists/optionlists.component';
import { HostOptionListsComponent } from './host/optionlists/hostoptionlists.component';
import { HostOptionListItemsComponent } from './host/optionlistitems/optionlistitems.component';
import { ContactsComponent } from './contacts/contacts.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                component: AppComponent,
                children: [
                    { path: 'home', component: HomeComponent,  canActivate: [AppRouteGuard] },
                    { path: 'users', component: UsersComponent, data: { permission: 'Pages.Users' }, canActivate: [AppRouteGuard] },
                    { path: 'roles', component: RolesComponent, data: { permission: 'Pages.Roles' }, canActivate: [AppRouteGuard] },
                    { path: 'tenants', component: TenantsComponent, data: { permission: 'Pages.Tenants' }, canActivate: [AppRouteGuard] },
                    { path: 'update-password', component: ChangePasswordComponent },
                    { path: 'optionlists', component:OptionListsComponent, data: { permission: 'Pages.OptionLists'}, canActivate: [AppRouteGuard] },
                    { path: 'optionlistitems', component:OptionListItemsComponent, data: { permission: 'Pages.OptionListItems'}, canActivate: [AppRouteGuard] },
                    { path: 'host/optionlists', component: HostOptionListsComponent, data: { permission: 'Pages.Tenants.OptionLists'}, canActivate: [AppRouteGuard] },
                    { path: 'host/optionlistitems', component: HostOptionListItemsComponent, data: { permission: 'Pages.Tenants.OptionListItems'}, canActivate: [AppRouteGuard] },
                    { path: 'contacts', component: ContactsComponent, data: { permission: 'Pages.Contacts' }, canActivate: [AppRouteGuard] }
                ]
            }
        ])
    ],
    exports: [RouterModule]
})
export class AppRoutingModule { }
