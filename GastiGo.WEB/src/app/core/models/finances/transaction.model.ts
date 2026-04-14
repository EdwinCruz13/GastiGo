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
    previousBalance: number;
    amount: number;
    balance: number;
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


export interface BalanceDTO{
   // transactionId: string;
    transactionDate: string;
    description: string;
    previousBalance: number;
    amount: number;
    balance: number;
    entryType: 'IN' | 'OUT' | 'TRANSFER';
}
