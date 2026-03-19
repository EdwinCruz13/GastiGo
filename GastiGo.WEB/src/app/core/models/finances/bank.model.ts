export interface Bank {
  bankId: string;
  name: string;
  abbre: string;
  transferFee: number;
  imgURL?: string;
}

// Interfaz para representar los datos necesarios para crear o actualizar un banco.
export interface BankRequestDTO {
  name: string;
  abbre: string;
  transferFee: number;
  imgURL?: string;
}

