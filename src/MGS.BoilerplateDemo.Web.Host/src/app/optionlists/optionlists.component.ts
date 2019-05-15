import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { OptionListViewDto, OptionListServiceProxy, PagedResultDtoOfOptionListViewDto } from "@shared/service-proxies/service-proxies";
import { MatDialog } from "@angular/material";
import { finalize } from "rxjs/operators";
import { CreateOptionListDialogComponent } from "./create/create-optionlist-dialog.component";
import { EditOptionListDialogComponent } from "./edit/edit-optionlist-dialog.component";

class PagedOptionListRequestDto extends PagedRequestDto {
    key: string;
    filter: string;
    sorting: string;
}

@Component({
    templateUrl: './optionlists.component.html',
    animations: [appModuleAnimation()],
    styleUrls: ['./optionlists.component.less']
})
export class OptionListsComponent extends PagedListingComponentBase<OptionListViewDto> {
    optionLists: OptionListViewDto[] = [];

    constructor(
        injector: Injector,
        private _optionListService: OptionListServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    list(request: PagedOptionListRequestDto, pageNumber: number, finishedCallback: Function): void {
        this._optionListService
            .getAll(request.filter, request.key, request.sorting, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfOptionListViewDto) => {
                this.optionLists = result.items;
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
            createOrEditOptionListDialog = this._dialog.open(CreateOptionListDialogComponent);
        } else {
            createOrEditOptionListDialog = this._dialog.open(EditOptionListDialogComponent, {
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