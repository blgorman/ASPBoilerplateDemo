import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import { OptionListItemCreateOrEditDto, OptionListItemsServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './create-optionlistitems-dialog.component.html',
  styleUrls: ['./create-optionlistitems-dialog.component.less']
})
export class CreateOptionListItemsDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionListItem: OptionListItemCreateOrEditDto = new OptionListItemCreateOrEditDto();

    constructor(injector: Injector,
                private _optionListItemService: OptionListItemsServiceProxy,
                private _dialogRef: MatDialogRef<CreateOptionListItemsDialogComponent>) 
    {
        super(injector);
    }

    ngOnInit(): void {
        this.optionListItem.isActive = true;
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
