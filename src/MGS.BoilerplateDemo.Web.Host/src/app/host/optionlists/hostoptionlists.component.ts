import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { OptionListViewDto } from "@shared/service-proxies/service-proxies";

class PagedOptionListRequestDto extends PagedRequestDto {
    keyword: string;
    filter: string;
    sorting: string;
}

@Component({
    templateUrl: './hostoptionlists.component.html',
    animations: [appModuleAnimation()],
    styleUrls: ['./hostoptionlists.component.less']
})
export class HostOptionListsComponent extends PagedListingComponentBase<OptionListViewDto> {
    optionLists: OptionListViewDto[] = [];

    constructor(
        injector: Injector,
    ) {
        super(injector);
    }

    protected list(
            request: PagedOptionListRequestDto, 
            pageNumber: number, 
            finishedCallback: Function): void {
        throw new Error("Method not implemented.");
    }

    protected delete(entity: OptionListViewDto): void {
        throw new Error("Method not implemented.");
    }
}
