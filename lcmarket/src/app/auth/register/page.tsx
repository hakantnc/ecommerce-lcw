'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';
import Image from 'next/image';

export default function RegisterPage() {
  const [userType, setUserType] = useState<'customer' | 'supplier'>('customer');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [supplierName, setSupplierName] = useState('');
  const [error, setError] = useState('');
  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    const endpoint = userType === 'customer' 
      ? 'http://localhost:5267/api/auth/customer/register'
      : 'http://localhost:5267/api/auth/supplier/register';

    const body = userType === 'customer'
      ? { email, password, firstName, lastName }
      : { email, password, supplierName };

    try {
      const res = await fetch(endpoint, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(body),
      });

      if (!res.ok) {
        throw new Error('Kayıt işlemi başarısız oldu');
      }

      router.push('/auth/login');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Bir hata oluştu');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center relative">
      {/* Background image with overlay */}
      <div 
        className="absolute inset-0 bg-cover bg-center bg-no-repeat"
        style={{
          backgroundImage: 'url(/auth-bg/auth-bg.jpg)',
          filter: 'brightness(0.3)'
        }}
      />

      <div className="max-w-md w-full space-y-8 bg-white/90 backdrop-blur-sm p-10 rounded-xl shadow-lg relative z-10 mx-4">
        <div className="text-center">
          <div className="flex justify-center">
            <Link href="/" className="block">
              <h1 className="text-4xl font-black text-indigo-600 tracking-tight hover:text-blue-700 transition-colors duration-200">
                LCW MARKET
              </h1>
            </Link>
            
          </div>
          <h2 className="mt-2 text-3xl font-extrabold text-gray-900">
            Yeni Hesap Oluşturun
          </h2>
          <p className="mt-2 text-sm text-gray-600">
            Veya{' '}
            <Link href="/auth/login" className="font-medium text-indigo-600 hover:text-indigo-500 transition-colors duration-200">
              mevcut hesabınıza giriş yapın
            </Link>
          </p>
        </div>

        <div className="flex justify-center space-x-4 mt-6">
          <button
            type="button"
            className={`px-6 py-3 rounded-lg font-medium transition-all duration-200 ${
              userType === 'customer'
                ? 'bg-indigo-600 text-white shadow-md'
                : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
            }`}
            onClick={() => setUserType('customer')}
          >
            <div className="flex items-center">
              <svg className="w-5 h-5 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
              </svg>
              Müşteri
            </div>
          </button>
          <button
            type="button"
            className={`px-6 py-3 rounded-lg font-medium transition-all duration-200 ${
              userType === 'supplier'
                ? 'bg-indigo-600 text-white shadow-md'
                : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
            }`}
            onClick={() => setUserType('supplier')}
          >
            <div className="flex items-center">
              <svg className="w-5 h-5 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
              </svg>
              Tedarikçi
            </div>
          </button>
        </div>

        <form className="mt-8 space-y-6" onSubmit={handleSubmit}>
          {error && (
            <div className="bg-red-50 border-l-4 border-red-400 p-4 rounded">
              <div className="flex">
                <div className="flex-shrink-0">
                  <svg className="h-5 w-5 text-red-400" viewBox="0 0 20 20" fill="currentColor">
                    <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
                  </svg>
                </div>
                <div className="ml-3">
                  <p className="text-sm text-red-700">{error}</p>
                </div>
              </div>
            </div>
          )}
          
          <div className="space-y-4">
            <div>
              <label htmlFor="email" className="block text-sm font-medium text-gray-700">
                Email adresi
              </label>
              <input
                id="email"
                name="email"
                type="email"
                autoComplete="email"
                required
                className="mt-1 appearance-none rounded-md relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm transition-colors duration-200"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
            
            <div>
              <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                Şifre
              </label>
              <input
                id="password"
                name="password"
                type="password"
                autoComplete="new-password"
                required
                className="mt-1 appearance-none rounded-md relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm transition-colors duration-200"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>

            {userType === 'customer' ? (
              <>
                <div>
                  <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">
                    Ad
                  </label>
                  <input
                    id="firstName"
                    name="firstName"
                    type="text"
                    autoComplete="given-name"
                    required
                    className="mt-1 appearance-none rounded-md relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm transition-colors duration-200"
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                  />
                </div>
                <div>
                  <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">
                    Soyad
                  </label>
                  <input
                    id="lastName"
                    name="lastName"
                    type="text"
                    autoComplete="family-name"
                    required
                    className="mt-1 appearance-none rounded-md relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm transition-colors duration-200"
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                  />
                </div>
              </>
            ) : (
              <div>
                <label htmlFor="supplierName" className="block text-sm font-medium text-gray-700">
                  Tedarikçi Adı
                </label>
                <input
                  id="supplierName"
                  name="supplierName"
                  type="text"
                  autoComplete="organization"
                  required
                  className="mt-1 appearance-none rounded-md relative block w-full px-3 py-3 border border-gray-300 placeholder-gray-500 text-gray-900 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm transition-colors duration-200"
                  value={supplierName}
                  onChange={(e) => setSupplierName(e.target.value)}
                />
              </div>
            )}
          </div>

          <div>
            <button
              type="submit"
              className="group relative w-full flex justify-center py-3 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500 transition-colors duration-200"
            >
              <span className="absolute left-0 inset-y-0 flex items-center pl-3">
                <svg
                  className="h-5 w-5 text-indigo-500 group-hover:text-indigo-400 transition-colors duration-200"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                  fill="currentColor"
                >
                  <path
                    fillRule="evenodd"
                    d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-8.707l-3-3a1 1 0 00-1.414 0l-3 3a1 1 0 001.414 1.414L9 9.414V13a1 1 0 102 0V9.414l1.293 1.293a1 1 0 001.414-1.414z"
                    clipRule="evenodd"
                  />
                </svg>
              </span>
              Kayıt Ol
            </button>
          </div>
        </form>
      </div>
    </div>
  );
} 