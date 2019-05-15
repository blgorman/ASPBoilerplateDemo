import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { OptionListItemViewDto, OptionListItemsServiceProxy } from "@shared/service-proxies/service-proxies";
import { MatDialog } from "@angular/material";

class PagedOptionListItemRequestDto extends PagedRequestDto {
    filter: string;
    key: string;
    sorting: string;
}

@Component({
    templateUrl: './optionlistitems.component.html',
    styleUrls: ['./optionlistitems.component.less'],
    animations: [appModuleAnimation()]
})
export class OptionListItemsComponent extends PagedListingComponentBase<OptionListItemViewDto> {

    optionListItems: OptionListItemViewDto[] = [];

    constructor(injector: Injector,
                private _optionListItemService: OptionListItemsServiceProxy,
                private _dialog: MatDialog) 
    {
        super(injector);
    }

    protected list(request: PagedOptionListItemRequestDto, pageNumber: number, finishedCallback: Function): void {
        throw new Error("Method not implemented.");
    }

    protected delete(entity: OptionListItemViewDto): void {
        throw new Error("Method not implemented.");
    }
}