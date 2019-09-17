export interface CustomerApplication {
    forename: string;
    surname: string;
    dateOfBirth: Date;
    annualIncome: number;
}

export interface SuggestedProduct {
    cardName: string;
    aprRate: number;
    promoMessage: string;
}