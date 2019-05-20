import { Component, Injector, OnInit } from "@angular/core";
import { ContactServiceProxy, ContactListViewDto, PagedResultDtoOfContactListViewDto, OptionListItemsServiceProxy, PagedResultDtoOfOptionListItemViewDto, OptionListItemViewDto } from "@shared/service-proxies/service-proxies";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { MatDialog } from "@angular/material";
import { finalize } from "rxjs/operators";
import { CreateContactDialogComponent } from "./create/create-contact-dialog.component";
import { EditContactDialogComponent } from "./edit/edit-contact-dialog.component";

class PagedContactRequestDto extends PagedRequestDto {
    key: string;
    filter: string;
    sorting: string;
}

@Component({
    templateUrl: './contacts.component.html',
    animations: [appModuleAnimation()],
    styleUrls: ['./contacts.component.less']
})
export class ContactsComponent extends PagedListingComponentBase<ContactListViewDto> implements OnInit {
    contacts: ContactListViewDto[] = [];
    states: OptionListItemViewDto[] = [];
    titles: OptionListItemViewDto[] = [];

    constructor(
        injector: Injector,
        private _contactService: ContactServiceProxy,
        private _optionListItemService: OptionListItemsServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.loadStates();
        this.loadTitles();
        this.refresh();
    }

    loadStates(): void {
        this._optionListItemService.getListItemsByListKey("", "States", "", 0, 1000)
            .pipe( finalize(() =>{}))
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                this.states = result.items;
            });
    }

    loadTitles(): void {
        this._optionListItemService.getListItemsByListKeyByTenant("", "Titles", "", 0, 1000)
            .pipe( finalize(() =>{}))
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                this.titles = result.items;
            });
    }

    displayTitles(): boolean {
        return this.titles && this.titles.length > 0;
    }

    list(request: PagedContactRequestDto, pageNumber: number, finishedCallback: Function): void {
        console.log("listing");
        this._contactService
            .getAll(request.filter, request.key, request.sorting, 0, 1000)
            .pipe(
                finalize(() => {
                    this.contacts.forEach((contact) => {
                        if (contact.titleId && contact.titleId > 0) {
                            this.titles.forEach((title) => {
                                if (title.id == contact.titleId) {
                                    contact.titleDisplay = title.displayText;
                                }
                            });
                        }

                        this.states.forEach((state) => {
                            if (state.id == contact.stateId)
                            {
                                contact.stateDisplay = state.displayText;
                                
                            }
                        });
                        console.log(contact);
                    });
                    
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfContactListViewDto) => {
                this.contacts = result.items;
                console.log(this.contacts);
                this.showPaging(result, pageNumber);
            });
    }

    protected delete(contact: ContactListViewDto): void {
        abp.message.confirm(
            this.l('AreYouSureWantToDelete', contact.firstName + ' ' + contact.lastName),
            (result: boolean) => {
                if (result) {
                    this._contactService.delete(contact.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    createContact(): void {
        this.showCreateOrEditContactListDialog();
    }

    editContact(contact: ContactListViewDto): void {
        this.showCreateOrEditContactListDialog(contact.id);
    }

    showCreateOrEditContactListDialog(id?: number): void {
        let createOrEditContactDialog;
        if (id === undefined || id <= 0) {
            createOrEditContactDialog = this._dialog.open(CreateContactDialogComponent);
        } else {
            createOrEditContactDialog = this._dialog.open(EditContactDialogComponent, {
                data: id
            });
        }

        createOrEditContactDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}