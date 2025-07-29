'use client';
import SearchInput from '@/components/SearchInput';
import Link from 'next/link';

export default function Header() {
  return (
    <header className="bg-white shadow-sm border-b border-gray-200">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Sol: Logo */}
          <div className="flex-shrink-0">
            <Link href="/" className="block">
              <h1 className="text-2xl font-black text-blue-600 hover:text-blue-700 transition-colors duration-200">
                LCW MARKET
              </h1>
            </Link>
          </div>

          {/* Orta: Arama Çubuğu */}
          <div className="flex-1 max-w-2xl mx-8">
            <form onSubmit={(e) => e.preventDefault()} className="relative">
              <div className="relative">
                {/* Arama ikonu */}
                <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                  <svg className="h-5 w-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" />
                  </svg>
                </div>

                <SearchInput />
              </div>
            </form>
          </div>

           {/* Sağ Taraf - Kullanıcı Menüsü */}
           <div className="flex items-center space-x-6">
            
            {/* Hesabım İkonu */}
            <Link 
              href="/auth/login" 
              className="flex items-center space-x-2 text-gray-700 hover:text-blue-600 transition-colors duration-200"
            >
              <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
              </svg>
              <span className="hidden sm:block text-sm font-medium">Hesabım</span>
            </Link>

            {/* Sepetim İkonu */}
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
