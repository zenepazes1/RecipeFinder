export interface LoginUser {
  email: string;
  password: string;
}

export interface RegisterUser {
  email: string;
  password: string;
  firstName: string;
  lastName: string;
}

export interface UserState {
  userId: string;
  token: string;
  email: string;
  firstName: string;
  lastName: string;
  roles: string[];
  error: { error: any; statusText: string } | null;
}

export interface AuthError {
  error: any;
  statusText: string;
}
