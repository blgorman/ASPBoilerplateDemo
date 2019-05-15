import { Component, Injector } from "@angular/core";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import { PagedListingComponentBase, PagedRequestDto } from "@shared/paged-listing-component-base";
import { OptionListItemViewDto, OptionListItemsServiceProxy, PagedResultDtoOfOptionListItemViewDto } from "@shared/service-proxies/service-proxies";
import { MatDialog } from "@angular/material";
import { finalize } from "rxjs/operators";
import { CreateOptionListItemsDialogComponent } from "./create/create-optionlistitems-dialog.component";
import { EditOptionListItemsDialogComponent } from "./edit/edit-optionlistitems-dialog.component";

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

    constructor(
        injector: Injector,
        private _optionListItemService: OptionListItemsServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    list(
        request: PagedOptionListItemRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        this._optionListItemService
            .getAllOptionListItems(request.filter, request.key, request.sorting, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfOptionListItemViewDto) => {
                this.optionListItems = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(optionListItem: OptionListItemViewDto): void {
        abp.message.confirm(
            this.l('AreYouSureWantToDelete', optionListItem.displayText),
            (result: boolean) => {
                if (result) {
                    this._optionListItemService.deleteListItem(optionListItem.id).subscribe(() => {
                        abp.notify.success(this.l('SuccessfullyDeleted'));
                        this.refresh();
                    });
                }
            }
        );
    }

    createOptionListItem(): void {
        this.showCreateOrEditOptionListItemDialog();
    }

    editOptionListItem(optionListItem: OptionListItemViewDto): void {
        this.showCreateOrEditOptionListItemDialog(optionListItem.id);
    }

    showCreateOrEditOptionListItemDialog(id?: number): void {
        let createOrEditOptionListItemDialog;
        if (id === undefined || id <= 0) {
            createOrEditOptionListItemDialog = this._dialog.open(CreateOptionListItemsDialogComponent);
        } else {
            createOrEditOptionListItemDialog = this._dialog.open(EditOptionListItemsDialogComponent, {
                data: id
            });
        }

        createOrEditOptionListItemDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}