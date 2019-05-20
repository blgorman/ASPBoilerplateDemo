import { Component, OnInit, Injector } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { MatDialogRef } from "@angular/material";
import { ContactListViewDto, ContactServiceProxy, OptionListViewDto, OptionListItemsServiceProxy, PagedResultDtoOfOptionListItemViewDto, OptionListItemViewDto } from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";

@Component({
templateUrl: './create-contact-dialog.component.html',
styleUrls: ['./create-contact-dialog.component.less']
})
export class CreateContactDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    contact: ContactListViewDto = new ContactListViewDto();
    titles: OptionListItemViewDto[] = [];
    states: OptionListItemViewDto[] = [];
    
    constructor(injector: Injector,
                private _optionListItemService: OptionListItemsServiceProxy,
                private _contactService: ContactServiceProxy,
                private _dialogRef: MatDialogRef<CreateContactDialogComponent>) 
    {
        super(injector);
    }

    ngOnInit(): void {
        this.loadStates();
        this.loadTitles();
        this.contact.isActive = true;
    }

    loadStates(): void {
        this._optionListItemService.getListItemsByListKey("", "States", "", 0, 1000)
            .pipe( finalize(() =>{}))
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                console.log("States");
                console.log(result.items);
                this.states = result.items;
            });
    }

    loadTitles(): void {
        this._optionListItemService.getListItemsByListKeyByTenant("", "Titles", "", 0, 1000)
            .pipe( finalize(() =>{}))
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                console.log("Titles");
                console.log(result.items);
                this.titles = result.items;
            });
    }

    displayTitles(): boolean {
        return this.titles && this.titles.length > 0;
    }

    save(): void {
        this.saving = true;

        this._contactService.create(this.contact)
            .pipe(
                finalize(() => {
                this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close(true);
        });
    }

    close(result: any): void {
        this._dialogRef.close(result);
    }
}
  