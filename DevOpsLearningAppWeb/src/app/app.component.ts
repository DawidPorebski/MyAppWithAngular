import { Component, OnInit } from '@angular/core';
import { MyEntityViewModel } from "./models/my-entity-view-model";
import { AppApiService } from "./app-api.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: [ './app.component.scss' ]
})
export class AppComponent implements OnInit {
  public readonly displayedColumns: string[] = [ 'id', 'text' ];

  public dataSource: MyEntityViewModel[] = []
  public isLoading: boolean = true;

  public constructor(private readonly _api: AppApiService) {
  }

  public async ngOnInit(): Promise<void> {
    await this._setRows();
  }

  public async addRow(): Promise<void> {
    this.isLoading = true;
    await this._api.addRow();
    await this._setRows();
  }

  public async removeAllRows(): Promise<void> {
    this.isLoading = true;
    await this._api.removeAllRows();
    await this._setRows();
  }

  private async _setRows(): Promise<void> {
    this.dataSource = await this._api.getRows();
    this.isLoading = false;
  }
}
