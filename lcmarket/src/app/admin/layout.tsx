// Admin Panel Layout - Yönetim paneli için ana layout
// Bu dosya admin sayfalarının ortak tasarımını belirler

import Link from 'next/link';

export default function AdminLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <div className="min-h-screen bg-gray-100">
      <div className="flex">
        {/* Sol Sidebar - Navigasyon Menüsü */}
        <aside className="w-64 bg-white shadow-sm min-h-screen flex flex-col">
         
          {/* Navigasyon Menüsü */}
          <nav className="flex-1 px-4 py-6">
            <ul className="space-y-2">
              {/* Dashboard */}
              <li>
                <Link
                  href="/admin"
                  className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-gray-600 hover:bg-gray-50 hover:text-gray-900 transition-colors duration-200"
                >
                  <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 7v10a2 2 0 002 2h14a2 2 0 002-2V9a2 2 0 00-2-2H5a2 2 0 00-2-2z" />
                  </svg>
                  Dashboard
                </Link>
              </li>

              {/* Kategoriler */}
              <li>
                <Link
                  href="/admin/categories"
                  className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-gray-600 hover:bg-gray-50 hover:text-gray-900 transition-colors duration-200"
                >
                  <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.99 1.99 0 013 12V7a4 4 0 014-4z" />
                  </svg>
                  Kategoriler
                </Link>
              </li>

              {/* Ürünler */}
              <li>
                <Link
                  href="/admin/products"
                  className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-gray-600 hover:bg-gray-50 hover:text-gray-900 transition-colors duration-200"
                >
                  <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
                  </svg>
                  Ürünler
                </Link>
              </li>

              {/* Müşteriler */}
              <li>
                <Link
                  href="/admin/customers"
                  className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-gray-600 hover:bg-gray-50 hover:text-gray-900 transition-colors duration-200"
                >
                  <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197m13.5-9a2.5 2.5 0 11-5 0 2.5 2.5 0 015 0z" />
                  </svg>
                  Müşteriler
                </Link>
              </li>

              {/* Siparişler */}
              <li>
                <Link
                  href="/admin/orders"
                  className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-gray-600 hover:bg-gray-50 hover:text-gray-900 transition-colors duration-200"
                >
                  <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5H7a2 2 0 00-2 2v10a2 2 0 002 2h8a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
                  </svg>
                  Siparişler
                </Link>
              </li>

              {/* Tedarikçiler */}
              <li>
                <Link
                  href="/admin/suppliers"
                  className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-gray-600 hover:bg-gray-50 hover:text-gray-900 transition-colors duration-200"
                >
                  <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
                  </svg>
                  Tedarikçiler
                </Link>
              </li>
            </ul>
          </nav>

          {/* Alt Navigasyon - Siteye Dön */}
          <div className="p-4 border-t border-gray-200">
            <Link
              href="/"
              className="group flex items-center px-3 py-2 text-base font-medium rounded-md text-blue-600 hover:bg-blue-50 hover:text-blue-700 transition-colors duration-200"
            >
              <svg className="mr-3 h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M10 19l-7-7m0 0l7-7m-7 7h18" />
              </svg>
              Ana Siteye Dön
            </Link>
          </div>
        </aside>

        {/* Ana İçerik Alanı */}
        <main className="flex-1 p-8">
          {children}
        </main>
      </div>
    </div>
  );
} 