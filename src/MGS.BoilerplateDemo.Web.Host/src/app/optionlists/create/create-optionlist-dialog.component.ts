import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef, MatCheckboxChange } from '@angular/material';
import { finalize } from 'rxjs/operators';
import * as _ from 'lodash';
import { AppComponentBase } from '@shared/app-component-base';
import { OptionListCreateOrEditDto, OptionListServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './create-optionlist-dialog.component.html',
  styleUrls: ['./create-optionlist-dialog.component.less']
})
export class CreateOptionListDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionList: OptionListCreateOrEditDto = new OptionListCreateOrEditDto();

    constructor(injector: Injector,
                    private _optionListService: OptionListServiceProxy,
                    private _dialogRef: MatDialogRef<CreateOptionListDialogComponent>) 
    {
        super(injector);
    }

    ngOnInit(): void {
        this.optionList.isActive = true;
    }

    save(): void {
        this.saving = true;

        this._optionListService.createOrUpdateList(this.optionList)
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
