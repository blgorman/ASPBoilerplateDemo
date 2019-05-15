import { OptionListItemViewDto, OptionListItemsServiceProxy } from "@shared/service-proxies/service-proxies";
import { Component, OnInit, Injector, Optional, Inject } from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Title } from "@angular/platform-browser";

@Component({
    templateUrl: './edit-optionlistitems-dialog.component.html',
    styleUrls: ['./edit-optionlistitems-dialog.component.less']
})
export class HostEditOptionListItemsDialogComponent extends AppComponentBase implements OnInit {
    saving = false;
    optionListItem: OptionListItemViewDto = new OptionListItemViewDto();
  
    constructor (injector: Injector,
              private _optionListItemService: OptionListItemsServiceProxy,
              private _dialogRef: MatDialogRef<HostEditOptionListItemsDialogComponent>,
              @Optional() @Inject(MAT_DIALOG_DATA) private _id: number,
              private titleService: Title) 
    {
        super(injector);
    }
  
    ngOnInit(): void {
        this._optionListItemService.getListItemById(this._id).subscribe((result: OptionListItemViewDto) => {
                this.optionListItem = result;
                this.titleService.setTitle(this.optionListItem.displayText);
        });
    }
}