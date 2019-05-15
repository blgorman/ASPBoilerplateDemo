import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { OptionListViewDto, OptionListServiceProxy, PagedResultDtoOfOptionListViewDto } from "@shared/service-proxies/service-proxies";
import { MatDialog } from "@angular/material";
import { finalize } from "rxjs/operators";
import { HostCreateOptionListDialogComponent } from "./create/create-hostoptionlist-dialog.component";
import { HostEditOptionListDialogComponent } from "./edit/edit-hostoptionlist-dialog.component";

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
        private _optionListService: OptionListServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    protected list(request: PagedOptionListRequestDto, pageNumber: number, finishedCallback: Function): void {
        this._optionListService
            .getAll(request.filter, request.keyword, request.sorting, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfOptionListViewDto) => {
                this.optionLists = result.items;
                console.log(this.optionLists);
                this.showPaging(result, pageNumber);
        });
    }

    protected delete(optionList: OptionListViewDto): void {
        abp.message.confirm(
            this.l('AreYouSureWantToDelete', optionList.displayName),
            (result: boolean) => {
                if (result) {
                    this._optionListService.deleteList(optionList.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    createOptionList(): void {
        this.showCreateOrEditOptionListDialog();
    }

    editOptionList(optionList: OptionListViewDto): void {
        this.showCreateOrEditOptionListDialog(optionList.id);
    }

    showCreateOrEditOptionListDialog(id?: number): void {
        let createOrEditOptionListDialog;
        if (id === undefined || id <= 0) {
            createOrEditOptionListDialog = this._dialog.open(HostCreateOptionListDialogComponent);
        } else {
            createOrEditOptionListDialog = this._dialog.open(HostEditOptionListDialogComponent, {
                data: id
            });
        }

        createOrEditOptionListDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
