"use client"
import { create } from 'zustand';
import { parseJwt, DecodedToken } from '@/lib/utils/jwt';

interface AuthState {
  token: string | null;
  user: DecodedToken | null;
  setToken: (token: string) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  token: null,
  user: null,
  setToken: (token: string) => {
    const user = parseJwt(token);
    set({ token, user });
    localStorage.setItem('token', token);
  },
  logout: () => {
    set({ token: null, user: null });
    localStorage.removeItem('token');
    window.location.href = '/'; // logout sonrası yönlendirme
  },
}));
