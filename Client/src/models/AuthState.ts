export interface AuthState {
  token: string | null;
  userId: string | null;
  userName: string | null;
  role: string | null;
  error: string | null;
  isLoading: boolean;
  isAuthenticated: boolean;
}
