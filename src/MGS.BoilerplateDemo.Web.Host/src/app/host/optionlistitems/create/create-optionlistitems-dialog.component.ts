import { Component, OnInit, Injector } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { OptionListItemCreateOrEditDto, OptionListItemsServiceProxy } from "@shared/service-proxies/service-proxies";
import { MatDialogRef } from "@angular/material";

@Component({
    templateUrl: './create-optionlistitems-dialog.component.html',
    styleUrls: ['./create-optionlistitems-dialog.component.less']
})
export class HostCreateOptionListItemsDialogComponent extends AppComponentBase implements OnInit {
    
    saving = false;
    optionListItem: OptionListItemCreateOrEditDto = new OptionListItemCreateOrEditDto();
  
    constructor(injector: Injector,
                  private _optionListItemService: OptionListItemsServiceProxy,
                  private _dialogRef: MatDialogRef<HostCreateOptionListItemsDialogComponent>) 
    {
        super(injector);
    }

    ngOnInit(): void {
        throw new Error("Method not implemented.");
    }
}  