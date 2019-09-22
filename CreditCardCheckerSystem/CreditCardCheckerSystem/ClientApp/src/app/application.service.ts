import { SuggestedProduct, CustomerApplication } from "./models";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable()
export class ApplicationService {

    constructor(private http: HttpClient) {}

    public submitApplication(data: CustomerApplication): Observable<SuggestedProduct> {
        return this.http.post<SuggestedProduct>(`api/ApplicationCheck/SubmitApplication`, data);
    }
}