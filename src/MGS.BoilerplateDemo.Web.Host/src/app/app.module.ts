import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { JsonpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { AbpModule } from '@abp/abp.module';

import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { SharedModule } from '@shared/shared.module';

import { HomeComponent } from '@app/home/home.component';
import { AboutComponent } from '@app/about/about.component';
import { TopBarComponent } from '@app/layout/topbar.component';
import { TopBarLanguageSwitchComponent } from '@app/layout/topbar-languageswitch.component';
import { SideBarUserAreaComponent } from '@app/layout/sidebar-user-area.component';
import { SideBarNavComponent } from '@app/layout/sidebar-nav.component';
import { SideBarFooterComponent } from '@app/layout/sidebar-footer.component';
import { RightSideBarComponent } from '@app/layout/right-sidebar.component';
// tenants
import { TenantsComponent } from '@app/tenants/tenants.component';
import { CreateTenantDialogComponent } from './tenants/create-tenant/create-tenant-dialog.component';
import { EditTenantDialogComponent } from './tenants/edit-tenant/edit-tenant-dialog.component';
// roles
import { RolesComponent } from '@app/roles/roles.component';
import { CreateRoleDialogComponent } from './roles/create-role/create-role-dialog.component';
import { EditRoleDialogComponent } from './roles/edit-role/edit-role-dialog.component';
// users
import { UsersComponent } from '@app/users/users.component';
import { CreateUserDialogComponent } from '@app/users/create-user/create-user-dialog.component';
import { EditUserDialogComponent } from '@app/users/edit-user/edit-user-dialog.component';
import { ChangePasswordComponent } from './users/change-password/change-password.component';
import { ResetPasswordDialogComponent } from './users/reset-password/reset-password.component';
//option lists tenant level
import { OptionListsComponent } from './optionlists/optionlists.component';
import { CreateOptionListDialogComponent } from './optionlists/create/create-optionlist-dialog.component';
import { EditOptionListDialogComponent } from './optionlists/edit/edit-optionlist-dialog.component';
// option list items
import { OptionListItemsComponent } from './optionlistitems/optionlistitems.component';
import { CreateOptionListItemsDialogComponent } from './optionlistitems/create/create-optionlistitems-dialog.component';
import { EditOptionListItemsDialogComponent } from './optionlistitems/edit/edit-optionlistitems-dialog.component';
//host lists
import { HostOptionListsComponent } from './host/optionlists/hostoptionlists.component';
import { HostCreateOptionListDialogComponent } from './host/optionlists/create/create-hostoptionlist-dialog.component';
import { HostEditOptionListDialogComponent } from './host/optionlists/edit/edit-hostoptionlist-dialog.component';
//host list items
import { HostOptionListItemsComponent } from './host/optionlistitems/optionlistitems.component';
import { HostCreateOptionListItemsDialogComponent } from './host/optionlistitems/create/create-optionlistitems-dialog.component';
import { HostEditOptionListItemsDialogComponent } from './host/optionlistitems/edit/edit-optionlistitems-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AboutComponent,
    TopBarComponent,
    TopBarLanguageSwitchComponent,
    SideBarUserAreaComponent,
    SideBarNavComponent,
    SideBarFooterComponent,
    RightSideBarComponent,
    // tenants
    TenantsComponent,
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    RolesComponent,
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    UsersComponent,
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ChangePasswordComponent,
    ResetPasswordDialogComponent,
    //option lists tenant level
    OptionListsComponent,
    CreateOptionListDialogComponent,
    EditOptionListDialogComponent,
    // option list items
    OptionListItemsComponent,
    CreateOptionListItemsDialogComponent,
    EditOptionListItemsDialogComponent,
    //host option lists
    HostOptionListsComponent,
    HostCreateOptionListDialogComponent,
    HostEditOptionListDialogComponent,
    //host option list items
    HostOptionListItemsComponent,
    HostCreateOptionListItemsDialogComponent,
    HostEditOptionListItemsDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    JsonpModule,
    ModalModule.forRoot(),
    AbpModule,
    AppRoutingModule,
    ServiceProxyModule,
    SharedModule,
    NgxPaginationModule
  ],
  providers: [],
  entryComponents: [
    // tenants
    CreateTenantDialogComponent,
    EditTenantDialogComponent,
    // roles
    CreateRoleDialogComponent,
    EditRoleDialogComponent,
    // users
    CreateUserDialogComponent,
    EditUserDialogComponent,
    ResetPasswordDialogComponent,
    //option lists tenant level
    OptionListsComponent,
    CreateOptionListDialogComponent,
    EditOptionListDialogComponent,
    // option list items
    OptionListItemsComponent,
    CreateOptionListItemsDialogComponent,
    EditOptionListItemsDialogComponent,
    //host option lists
    HostOptionListsComponent,
    HostCreateOptionListDialogComponent,
    HostEditOptionListDialogComponent,
    //host option list items
    HostOptionListItemsComponent,
    HostCreateOptionListItemsDialogComponent,
    HostEditOptionListItemsDialogComponent
  ]
})
export class AppModule {}
