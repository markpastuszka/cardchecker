import { getBaseUrl } from "src/main";
import { SuggestedProduct, CustomerApplication } from "./models";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";

export class ApplicationService {

    constructor(private http: HttpClient) {}

    public submitApplication(data: CustomerApplication): Observable<SuggestedProduct> {
        return this.http.post<SuggestedProduct>(getBaseUrl() + `api/ApplicationCheck/SubmitApplication`, data);
    }
}