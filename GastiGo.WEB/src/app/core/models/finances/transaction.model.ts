import { User } from "../users/user.model";
import { Account } from "./account.model";
import { Category } from "./category.model";
import { TransactionType } from "./transactionType.model";

export interface Transaction{
    user: User;
    transactionType: TransactionType;
    category: Category;
    account: Account;
    amount: number;
    description: string;
    transactionDate: Date;
    reference: string;
    transferGroupId?: string;
}

export interface TransactionRequestDTO{
    transactionId: string;
    userId: string;
    transactionTypeId: string;
    categoryId: string;
    accountId: string;
    amount: number;
    description: string;
    transactionDate: Date;
    reference: string;
    transferGroupId?: string;
}