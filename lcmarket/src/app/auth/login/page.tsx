'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import { useAuthStore } from '@/lib/store/authStore';

export default function LoginPage() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const router = useRouter();
  const { setToken } = useAuthStore();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    try {
      const res = await fetch('http://localhost:5267/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
      });

      if (!res.ok) {
        const msg = await res.text();
        throw new Error(msg || 'Giriş başarısız');
      }

      const data = await res.json();

      if (!data.token) throw new Error('Token alınamadı');

      // ✅ Token'ı kaydet
      localStorage.setItem('token', data.token);
      setToken(data.token);

      // Anasayfaya yönlendir
      router.push('/');
      router.refresh();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Bir hata oluştu');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center relative">
           <div
          className="absolute inset-0 bg-cover bg-center bg-no-repeat z-0"
          style={{
            backgroundImage: "url(/auth-bg/auth-bg.jpg)",
            filter: "brightness(0.3)",
          }}
        />
      <div className="w-full max-w-md bg-white p-8 rounded-xl shadow-md relative z-10 mx-4">
        <h2 className="text-center text-3xl font-extrabold text-gray-900">
          Giriş Yap
        </h2>
        <p className="mt-2 text-center text-sm text-gray-600">
          Henüz hesabınız yok mu?{' '}
          <Link href="/auth/register" className="text-indigo-600 hover:text-indigo-500">
            Kayıt Ol
          </Link>
        </p>

        {error && (
          <div className="mt-4 text-sm text-red-600 bg-red-50 border border-red-300 px-4 py-2 rounded">
            {error}
          </div>
        )}

        <form className="mt-6 space-y-4" onSubmit={handleSubmit}>
          <div>
            <label htmlFor="email" className="block text-sm font-medium text-gray-700">
              E-posta
            </label>
            <input
              id="email"
              type="email"
              autoComplete="email"
              required
              className="mt-1 w-full px-4 py-2 border rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              placeholder="ornek@mail.com"
            />
          </div>

          <div>
            <label htmlFor="password" className="block text-sm font-medium text-gray-700">
              Şifre
            </label>
            <input
              id="password"
              type="password"
              autoComplete="current-password"
              required
              className="mt-1 w-full px-4 py-2 border rounded-md shadow-sm focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              placeholder="••••••••"
            />
          </div>

          <button
            type="submit"
            className="w-full py-2 px-4 bg-indigo-600 text-white font-semibold rounded-md hover:bg-indigo-700 transition"
          >
            Giriş Yap
          </button>
        </form>
      </div>
    </div>
  );
}
