import { Account } from "./account.model";
import { Transaction } from "./transaction.model";

export interface TransactionDetail{
  TransactionDetailId: string;
  Transaction: Transaction;
  Account: Account;
  Amount: number;
  EntryType: string;
}
