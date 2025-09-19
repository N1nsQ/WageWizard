export interface AuthState {
  isAuthenticated: boolean;
  user: string | null;
  error: string | null;
  isLoading: boolean;
}
