import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { MyEntityViewModel } from "./models/my-entity-view-model";
import { environment } from "../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class AppApiService {
  private readonly _headerDict = {
    "Access-Control-Allow-Origin": "http://3.72.251.204:5006"
  }

  public constructor(
    private readonly _http: HttpClient) {
  }

  public async getRows(): Promise<MyEntityViewModel[]> {
    const observable = await this._http.get<MyEntityViewModel[]>(
      this._createUrl('rows'), {
        headers: this._headerDict
      });
    const response = await observable.toPromise();
    return (response ?? []).map(vm => new MyEntityViewModel(vm));
  }

  public async addRow(): Promise<void> {
    const observable = await this._http.post(this._createUrl("add-row"), null, {
      headers: this._headerDict
    });
    await observable.toPromise();
  }

  public async removeAllRows(): Promise<void> {
    const observable = await this._http.post(this._createUrl("remove-all-rows"), null, {
      headers: this._headerDict
    });
    await observable.toPromise();
  }

  private _createUrl(path: string): string {
    return ${environment.backendBaseUrl}/${path}
  }
}