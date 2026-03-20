import { User } from "../users/user.model";
import { AccountType } from "./accountType.model";
import { Bank } from "./bank.model";
import { Currency } from "./currency.model";


export interface Account {
    accountId: string;
    accountType: AccountType;
    user: User;
    currency: Currency;
    bank: Bank;
    name: string;
    description: string;
    balance: number;
}


export interface AccountRequestDTO {
    accountId: string;
    accountTypeId: string;
    userId: string;
    currencyId: string;
    bankId: string;
    name: string;
    description: string;
    balance: number;
}

