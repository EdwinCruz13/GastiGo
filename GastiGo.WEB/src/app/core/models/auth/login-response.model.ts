export interface LoginResponse {
  requireTwoFactor: boolean;
  twoFactorId: string;
  accessToken: string;
  refreshToken: string;
}
