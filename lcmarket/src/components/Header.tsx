'use client';
import { useEffect, useState } from 'react';
import Link from 'next/link';
import { useAuthStore } from '@/lib/store/authStore'; 
import SearchInput from '@/components/SearchInput';

export default function Header() {
  const { user, setToken, logout } = useAuthStore();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token) {
      setToken(token);
    }
  }, []);
  console.log("Header user:", user);
  const initials = user
  ? `${user.given_name?.charAt(0) ?? ''}${user.surname?.charAt(0) ?? ''}`.toUpperCase()
  : '';

  return (
    <header className="bg-white shadow-sm border-b border-gray-200">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <div className="flex items-center space-x-4">
            <Link href="/" className="text-2xl font-black text-blue-600 hover:text-blue-700 transition">
              LCW MARKET
            </Link>

          </div>

          {/* Search */}
          <div className="flex-1 max-w-2xl mx-8">
            <SearchInput />
          </div>

          {/* Sağ Menü */}
          <div className="flex items-center space-x-6 relative">
            {!user ? (
              <Link
              href="/auth/login"
              className="flex items-center space-x-2 px-4 py-2 bg-blue-600 text-white rounded-md hover:bg-blue-700 transition-colors duration-200"
            >
              <svg className="h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  strokeWidth={2}
                  d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"
                />
              </svg>
              <span className="text-sm font-medium">Giriş Yap</span>
            </Link>
            ) : (
              <div
                className="relative"

              >
               <button
                onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                className="flex items-center justify-center w-10 h-10 rounded-full bg-blue-700 text-white font-semibold uppercase 
                transition-all duration-200 hover:bg-black-300">
                  {initials}
                </button>
                {isDropdownOpen && (
  <div className="absolute right-0 mt-2 w-56 bg-white border border-gray-200 rounded-lg shadow-lg z-50 py-2">
    {user?.role === 'Customer' ? (
      <>
        <Link
          href="/orders"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors"
        >
          Siparişlerim
        </Link>
        <Link
          href="/addresses"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors"
        >
          Adreslerim
        </Link>
        <Link
          href="/payments"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors"
        >
          Ödeme Yöntemlerim
        </Link>
        <Link
          href="/account"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors"
        >
          Ayarlarım
        </Link>
        <button
          onClick={logout}
          className="block w-full text-left px-4 py-2 text-red-600 hover:bg-red-50 transition-colors"
        >
          Çıkış Yap
        </button>
      </>
    ) : user?.role === 'Supplier' ? (
      <>
        <Link
          href="/supplier/panel"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors"
        >
          Tedarikçi Paneli
        </Link>
        <Link
          href="/account"
          className="block px-4 py-2 text-gray-700 hover:bg-gray-100 transition-colors"
        >
          Ayarlarım
        </Link>
        <button
          onClick={logout}
          className="block w-full text-left px-4 py-2 text-red-600 hover:bg-red-50 transition-colors"
        >
          Çıkış Yap
        </button>
      </>
    ) : null}
  </div>
)}

              </div>
            )}

              <Link 
                href="/cart" 
                className="flex items-center space-x-2 text-gray-700 hover:text-blue-600 transition-colors duration-200 relative"
              >
                <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 3h2l.4 2M7 13h10l4-8H5.4m0 0L7 13m0 0l-1.1 5.5A2 2 0 007.9 21h8.2a2 2 0 002-2.1L17 13H7m0 0h10m-5 4a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z" />
                </svg>
                <span className="hidden sm:block text-sm font-medium">Sepetim</span>
              </Link>
          </div>
        </div>
      </div>
    </header>
  );
}
