// import { ComponentFixture, TestBed } from '@angular/core/testing';

// import { ApplyComponent } from './apply.component';
// import { ApplicationService } from '../application.service';
// import { of } from 'rxjs/internal/observable/of';
// import { FormsModule } from '@angular/forms';
// import { CustomerApplication } from '../models';

// describe('ApplyComponent', () => {
//     let component: ApplyComponent;
//     let fixture: ComponentFixture<ApplyComponent>;

//     const appService = {
//         submitApplication: () => {}
//     };

//     const customer: CustomerApplication = {
//         forename: "bob",
//         surname: "smith",
//         dateOfBirth: new Date(),
//         annualIncome: 35000
//     };

//     const recommendation = {
//         cardName: 'Some card',
//         aprRate: 0,
//         promoMessage: 'some message'
//     };

//     beforeEach(() => {
//         spyOn(appService, 'submitApplication').and.returnValue(of(recommendation))
//         TestBed.configureTestingModule({
//             declarations: [ ApplyComponent ],
//             imports: [ FormsModule ],
//             providers: [
//                 { provide: ApplicationService, useValue: appService }
//             ]
//         })
//         .compileComponents();

//         fixture = TestBed.createComponent(ApplyComponent);
//         component = fixture.componentInstance;
//         fixture.detectChanges();
//     });

// //   it('should display a title', async(() => {
// //     const titleText = fixture.nativeElement.querySelector('h1').textContent;
// //     expect(titleText).toEqual('Counter');
// //   }));

//     it('shows data when response received', () => {
//         expect(component).toBeTruthy();
//     });
// });
