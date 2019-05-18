import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import { OptionListItemCreateOrEditDto, OptionListItemsServiceProxy, OptionListServiceProxy, PagedResultDtoOfOptionListViewDto, OptionListViewDto } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './create-optionlistitems-dialog.component.html',
  styleUrls: ['./create-optionlistitems-dialog.component.less']
})
export class CreateOptionListItemsDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionListItem: OptionListItemCreateOrEditDto = new OptionListItemCreateOrEditDto();
    optionLists: OptionListViewDto[] = [];

    constructor(injector: Injector,
                private _optionListItemService: OptionListItemsServiceProxy,
                private _optionListService: OptionListServiceProxy,
                private _dialogRef: MatDialogRef<CreateOptionListItemsDialogComponent>) 
    {
        super(injector);
    }

    ngOnInit(): void {
        this.loadLists();
        this.optionListItem.isActive = true;
        this.optionListItem.optionListId = -1;   
    }

    loadLists() {
        this._optionListService
            .getAll('', '', '', 0, 1000)
            .pipe(
                finalize(() => {
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
