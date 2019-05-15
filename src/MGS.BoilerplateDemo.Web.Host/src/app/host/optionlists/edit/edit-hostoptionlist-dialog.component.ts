import { Component, Injector, OnInit, Inject, Optional } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { OptionListServiceProxy, OptionListViewDto } from '@shared/service-proxies/service-proxies';
import { Title } from '@angular/platform-browser';

@Component({
  templateUrl: './edit-hostoptionlist-dialog.component.html',
  styleUrls: ['./edit-hostoptionlist-dialog.component.less']
})
export class HostEditOptionListDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionList: OptionListViewDto = new OptionListViewDto();

    constructor(injector: Injector,
                private _optionListService: OptionListServiceProxy,
                private _dialogRef: MatDialogRef<HostEditOptionListDialogComponent>,
                @Optional() @Inject(MAT_DIALOG_DATA) private _id: number,
                private titleService: Title) 
    {
        super(injector);
    }

    ngOnInit(): void {
        this._optionListService.getListById(this._id).subscribe((result: OptionListViewDto) => {
                    this.optionList = result;
                    this.titleService.setTitle(this.optionList.displayName);
                });
    }

    save(): void {
        this.saving = true;

        this._optionListService
                .createOrUpdateList(this.optionList)
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
