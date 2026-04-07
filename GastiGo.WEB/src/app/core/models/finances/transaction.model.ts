import { TransactionDetail } from './transactionDetail.model';
import { User } from "../users/user.model";
import { Account } from "./account.model";
import { Category } from "./category.model";
import { TransactionType } from "./transactionType.model";

export interface Transaction{
    transactionId: string;
    user: User;
    transactionType: TransactionType;
    category?: Category;
    description: string;
    transactionDate: Date;
    reference?: string;
    transferGroupId?: string;
    transactionDetail: TransactionDetail[];
    amount: number;
    entryType: 'IN' | 'OUT' | 'TRANSFER';
}

export interface TransactionRequestDTO{
    userId: string;
    transactionTypeId: string;
    categoryId: string;
    description: string;

    fromAccountId?: string;
    toAccountId?: string;
    amount: number;
    entryType: 'IN' | 'OUT' | 'TRANSFER';
}


export interface TransactionDetailDTO{
    transactionId: string;
    userName: string;
    transactionName: string;
    categoryName: string;
    description: string;
    transactionDate: Date;
    reference?: string;
    transferGroupId?: string;
    accountName: string;
    currencyName: string;
    inicialBalance: number;
    amount: number;
    balance: number;
    entryType: 'IN' | 'OUT' | 'TRANSFER';
}
