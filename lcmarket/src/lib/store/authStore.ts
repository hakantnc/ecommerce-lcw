"use client";
import { create } from 'zustand';
import { DecodedToken } from '@/lib/utils/jwt';

interface AuthState {
  user: DecodedToken | null;
  setUser: (user: DecodedToken | null) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  user: null,
  setUser: (user) => set({ user }),
  logout: async () => {
    await fetch('http://localhost:5267/api/auth/logout', {
      method: 'POST',
      credentials: 'include',
    });
    set({ user: null });
    window.location.href = '/';
  },
}));
