import { Component, OnInit, Injector, Optional, Inject } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { OptionListViewDto, OptionListServiceProxy } from "@shared/service-proxies/service-proxies";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Title } from "@angular/platform-browser";
import { finalize } from "rxjs/operators";

@Component({
    templateUrl: './edit-optionlist-dialog.component.html',
    styleUrls: ['./edit-optionlist-dialog.component.less']
})
export class EditOptionListDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionList: OptionListViewDto = new OptionListViewDto();
  
    constructor(injector: Injector,
                    private _optionListService: OptionListServiceProxy,
                    private _dialogRef: MatDialogRef<EditOptionListDialogComponent>,
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
  