import { AppComponentBase } from "@shared/app-component-base";
import { OnInit, Component, Injector } from "@angular/core";
import { OptionListCreateOrEditDto, OptionListServiceProxy } from "@shared/service-proxies/service-proxies";
import { MatDialogRef } from "@angular/material";
import { finalize } from "rxjs/operators";

@Component({
    templateUrl: './create-hostoptionlist-dialog.component.html',
    styleUrls: ['./create-hostoptionlist-dialog.component.less']
})
export class HostCreateOptionListDialogComponent extends AppComponentBase implements OnInit 
{
    saving = false;
    optionList: OptionListCreateOrEditDto = new OptionListCreateOrEditDto();
  
    constructor(injector: Injector,
                private _optionListService: OptionListServiceProxy,
                private _dialogRef: MatDialogRef<HostCreateOptionListDialogComponent>) 
    {
        super(injector);
    }
  
    ngOnInit(): void {
        this.optionList.isActive = true;
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


