import { Component } from '@angular/core';
import { ApplicationService } from '../application.service';
import { SuggestedProduct, CustomerApplication } from '../models';

@Component({
  selector: 'apply',
  templateUrl: './apply.component.html',
  styleUrls: ['./apply.component.css']
})
export class ApplyComponent {

    public suggestedProduct: SuggestedProduct;

    constructor(private applicationService: ApplicationService) {}

    public onClickSubmit(data: CustomerApplication) {
        this.applicationService.submitApplication(data).subscribe(result => {
            this.suggestedProduct = result;
            console.log(this.suggestedProduct);
          }, error => console.error(error));
    }
}



