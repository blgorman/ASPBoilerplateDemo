import { AppComponentBase } from "@shared/app-component-base";
import { OnInit, Component, Injector, Inject, Optional } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Title } from "@angular/platform-browser";
import { ContactServiceProxy, ContactListViewDto, OptionListItemsServiceProxy, OptionListItemViewDto, PagedResultDtoOfOptionListItemViewDto } from "@shared/service-proxies/service-proxies";
import { finalize } from "rxjs/operators";

@Component({
    templateUrl: './edit-contact-dialog.component.html',
    styleUrls: ['./edit-contact-dialog.component.less']
})
export class EditContactDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    contact: ContactListViewDto = new ContactListViewDto();
    titles: OptionListItemViewDto[] = [];
    states: OptionListItemViewDto[] = [];
   
    constructor(injector: Injector,
                    private _dialogRef: MatDialogRef<EditContactDialogComponent>,
                    private _contactService: ContactServiceProxy,
                    private _optionListItemService: OptionListItemsServiceProxy,
                    @Optional() @Inject(MAT_DIALOG_DATA) private _id: number,
                    private titleService: Title) 
    {
      super(injector);
    }
  
    ngOnInit(): void {
        this.loadStates();
    }

    loadStates(): void {
        this._optionListItemService.getListItemsByListKey("", "States", "", 0, 1000)
            .pipe( finalize(() =>{ this.loadTitles(); }))
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                console.log("States");
                console.log(result.items);
                this.states = result.items;
            });
    }

    loadTitles(): void {
        this._optionListItemService.getListItemsByListKeyByTenant("", "Titles", "", 0, 1000)
            .pipe( finalize(() =>{this.loadContact();}))
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                console.log("Titles");
                console.log(result.items);
                this.titles = result.items;
            });
    }

    loadContact(): void {
        this._contactService.get(this._id).subscribe((result: ContactListViewDto) => {
            this.contact = result;
            this.titleService.setTitle(this.contact.displayName);
        });
    }

    displayTitles(): boolean {
        return this.titles && this.titles.length > 0;
    }
  
    save(): void {
        this.saving = true;
  
        this._contactService.update(this.contact)
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