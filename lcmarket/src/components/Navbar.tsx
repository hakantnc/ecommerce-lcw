'use client'

import Link from 'next/link'
import { useState } from 'react'
import { useQuery } from '@tanstack/react-query'
import { categoryAPI } from '@/lib/api'

// Kategori tipi
interface Category {
  id: number
  name: string
  description?: string
}

export default function Navbar() {
  const [mobileMenuOpen, setMobileMenuOpen] = useState(false)

  // ✅ TanStack Query ile kategorileri çekiyoruz
  const {
    data: categories = [],
    isLoading,
    isError
  } = useQuery({
    queryKey: ['categories'],
    queryFn: categoryAPI.getAll
  })

  const toggleMobileMenu = () => setMobileMenuOpen(!mobileMenuOpen)

  return (
    <nav className="bg-white border-b border-gray-200 shadow-sm">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">

        {/* Desktop navbar */}
        <div className="hidden md:flex items-center justify-between h-12">
          <div className="flex items-center space-x-1 overflow-hidden">
            {categories.length > 0 && (
              <div className="flex items-center space-x-1 overflow-hidden">
                {categories.map((category) => (
                  <Link
                    key={category.id}
                    href={`/category/${category.id}`}
                    className="px-3 py-2 text-sm font-medium text-gray-700 hover:text-blue-600 hover:bg-blue-50 rounded-md transition-colors duration-200"
                  >
                    {category.name}
                  </Link>
                ))}
                <Link
                  href="/categories"
                  className="px-3 py-2 text-sm font-medium text-blue-600 hover:text-blue-700 hover:bg-blue-50 rounded-md transition-colors duration-200"
                >
                  Tüm Kategoriler
                </Link>
              </div>
            )}
          </div>
        </div>

        {/* Mobile versiyon */}
        <div className="md:hidden">
          <div className="flex items-center justify-between h-12">
            <span className="text-sm font-medium text-gray-700">Kategoriler</span>

            <button
              onClick={toggleMobileMenu}
              className="p-2 rounded-md text-gray-700 hover:text-blue-600 hover:bg-gray-100 transition-colors duration-200"
            >
              <svg className="h-5 w-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                {mobileMenuOpen ? (
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                ) : (
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                )}
              </svg>
            </button>
          </div>

          {mobileMenuOpen && (
            <div className="border-t border-gray-200 bg-white">
              <div className="py-2 space-y-1">
                {isLoading ? (
                  <div className="px-3 py-2">Yükleniyor...</div>
                ) : isError ? (
                  <div className="px-3 py-2 text-sm text-red-500">Hata oluştu</div>
                ) : categories.map((category) => (
                  <Link
                    key={category.id}
                    href={`/kategori/${category.id}`}
                    onClick={() => setMobileMenuOpen(false)}
                    className="block px-3 py-2 text-sm font-medium text-gray-700 hover:text-blue-600 hover:bg-blue-50"
                  >
                    {category.name}
                  </Link>
                ))}
              </div>
            </div>
          )}
        </div>
      </div>
    </nav>
  )
}
