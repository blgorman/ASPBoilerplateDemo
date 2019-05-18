import { Component, Injector, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { OptionListItemsServiceProxy, OptionListItemViewDto, OptionListViewDto, OptionListServiceProxy, PagedResultDtoOfOptionListViewDto } from '@shared/service-proxies/service-proxies';
import { Title } from '@angular/platform-browser';

@Component({
  templateUrl: './edit-optionlistitems-dialog.component.html',
  styleUrls: ['./edit-optionlistitems-dialog.component.less']
})
export class EditOptionListItemsDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionListItem: OptionListItemViewDto = new OptionListItemViewDto();
    optionLists: OptionListViewDto[] = [];

    constructor (injector: Injector,
            private _optionListItemService: OptionListItemsServiceProxy,
            private _dialogRef: MatDialogRef<EditOptionListItemsDialogComponent>,
            private _optionListService: OptionListServiceProxy,
            @Optional() @Inject(MAT_DIALOG_DATA) private _id: number,
            private titleService: Title) 
    {
        super(injector);
    }

    ngOnInit(): void {
        this._optionListItemService.getListItemById(this._id)
            .pipe(
                finalize(() => {
                    this.loadLists();
                })
            )
            .subscribe((result: OptionListItemViewDto) => {
                this.optionListItem = result;
                this.titleService.setTitle(this.optionListItem.displayText);
        });
    }

    loadLists() {
        this._optionListService
            .getAll('', '', '', 0, 1000)
            .pipe(
                finalize(() => {
                    // this.optionLists.forEach((option) => {
                    //     if (option.id == this.optionListItem.optionListId)
                    //     {
                    //         this.selectedOptionList = option;
                    //     }
                    // });
                })
            )
            .subscribe((result: PagedResultDtoOfOptionListViewDto) => {
                this.optionLists = result.items;
            });
    }

    save(): void {
        this.saving = true;

        this._optionListItemService.createOrUpdateListItem(this.optionListItem)
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
