// Footer Component - E-ticaret sitesi için ana footer
// Kategoriler, linkler, ödeme logoları ve yasal bilgiler

'use client';

import Link from 'next/link';
import { useState, useEffect } from 'react';
import { categoryAPI } from '@/lib/api';

// Kategori tipi
interface Category {
  id: number;
  name: string;
  description?: string;
}

export default function Footer() {
  // State'ler
  const [categories, setCategories] = useState<Category[]>([]);

  // Kategorileri API'den çek
  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const data = await categoryAPI.getAll();
      // Sadece ilk 6 kategoriyi göster
      setCategories((data || []).slice(0, 6));
    } catch (error) {
     
    }
  };

  return (
    <footer className="bg-gray-50 border-t border-gray-200">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        
        {/* Ana Footer İçeriği */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8">
          
          {/* LCW Market Bilgileri */}
          <div className="space-y-4">
            <h1 className="text-lg font-bold text-blue-600">LCW MARKET</h1>
            <p className="text-sm text-gray-600">
             İyi Alışveriş Herkesin Hakkı.
            </p>
            <div className="flex space-x-4">
              {/* Sosyal Medya İkonları */}
              <Link href="#" className="text-gray-400 hover:text-blue-600 transition-colors duration-200">
                <svg className="h-5 w-5" fill="currentColor" viewBox="0 0 24 24">
                  <path d="M24 4.557c-.883.392-1.832.656-2.828.775 1.017-.609 1.798-1.574 2.165-2.724-.951.564-2.005.974-3.127 1.195-.897-.957-2.178-1.555-3.594-1.555-3.179 0-5.515 2.966-4.797 6.045-4.091-.205-7.719-2.165-10.148-5.144-1.29 2.213-.669 5.108 1.523 6.574-.806-.026-1.566-.247-2.229-.616-.054 2.281 1.581 4.415 3.949 4.89-.693.188-1.452.232-2.224.084.626 1.956 2.444 3.379 4.6 3.419-2.07 1.623-4.678 2.348-7.29 2.04 2.179 1.397 4.768 2.212 7.548 2.212 9.142 0 14.307-7.721 13.995-14.646.962-.695 1.797-1.562 2.457-2.549z"/>
                </svg>
              </Link>
              
              <Link href="#" className="text-gray-400 hover:text-blue-600 transition-colors duration-200">
                <svg className="h-5 w-5" fill="currentColor" viewBox="0 0 24 24">
                  <path d="M12.017 0C5.396 0 .029 5.367.029 11.987c0 5.079 3.158 9.417 7.618 11.174-.105-.949-.199-2.403.041-3.439.219-.937 1.406-5.957 1.406-5.957s-.359-.72-.359-1.781c0-1.663.967-2.911 2.168-2.911 1.024 0 1.518.769 1.518 1.688 0 1.029-.653 2.567-.992 3.992-.285 1.193.6 2.165 1.775 2.165 2.128 0 3.768-2.245 3.768-5.487 0-2.861-2.063-4.869-5.008-4.869-3.41 0-5.409 2.562-5.409 5.199 0 1.033.394 2.143.889 2.741.099.12.112.225.085.345-.09.375-.293 1.199-.334 1.363-.053.225-.172.271-.402.165-1.495-.69-2.433-2.878-2.433-4.646 0-3.776 2.748-7.252 7.92-7.252 4.158 0 7.392 2.967 7.392 6.923 0 4.135-2.607 7.462-6.233 7.462-1.214 0-2.357-.629-2.746-1.378l-.748 2.853c-.271 1.043-1.002 2.35-1.492 3.146C9.57 23.812 10.763 24.009 12.017 24.009c6.624 0 11.99-5.367 11.99-11.988C24.007 5.367 18.641.001.012.001z"/>
                </svg>
              </Link>
            </div>
          </div>

          {/* Kategoriler */}
          <div className="space-y-4">
            <h3 className="text-lg font-semibold text-gray-900">Kategoriler</h3>
            <ul className="space-y-2">
              {categories.map((category) => (
                <li key={category.id}>
                  <Link 
                    href={`/kategori/${category.id}`}
                    className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200" >
                    {category.name}
                  </Link>
                </li>
              ))}
              <li>
                <Link 
                  href="/kategoriler"
                  className="text-sm text-blue-600 hover:text-blue-700 font-medium transition-colors duration-200">
                  Tüm Kategoriler →
                </Link>
              </li>
            </ul>
          </div>

          {/* Kurumsal */}
          <div className="space-y-4">
            <h3 className="text-lg font-semibold text-gray-900">Kurumsal</h3>
            <ul className="space-y-2">
              <li>
                <Link href="/hakkimizda" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  Hakkımızda
                </Link>
              </li>
              <li>
                <Link href="/iletisim" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  İletişim
                </Link>
              </li>
              <li>
                <Link href="/kampanyalar" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  Kampanyalar
                </Link>
              </li>
            </ul>
          </div>

          {/* Müşteri Hizmetleri */}
          <div className="space-y-4">
            <h3 className="text-lg font-semibold text-gray-900">Müşteri Hizmetleri</h3>
            <ul className="space-y-2">
              <li>
                <Link href="/yardim" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  Yardım Merkezi
                </Link>
              </li>
              <li>
                <Link href="/siparis-takip" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  Sipariş Takibi
                </Link>
              </li>
              <li>
                <Link href="/iade-degisim" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  İade & Değişim
                </Link>
              </li>
              <li>
                <Link href="/teslimat" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  Teslimat Bilgileri
                </Link>
              </li>
              <li>
                <Link href="/sss" className="text-sm text-gray-600 hover:text-blue-600 transition-colors duration-200">
                  SSS
                </Link>
              </li>
            </ul>
          </div>
        </div>

        {/* Ödeme Yöntemleri ve Güvenlik */}
        <div className="mt-12 pt-8 border-t border-gray-200">
          <div className="flex flex-col md:flex-row justify-between items-center space-y-4 md:space-y-0">
            
            {/* Ödeme Logoları */}
            <div className="flex items-center space-x-4">
              <span className="text-sm text-gray-600 font-medium">Güvenli Ödeme:</span>
              
              {/* Visa Logo */}
              <div className="bg-white border border-gray-200 rounded px-2 py-1 flex items-center space-x-1">
                 <img src="/icons/visa.png" alt="Visa Logo" className="w-10 h-10" />
                </div>
              
              {/* Mastercard Logo */}
              <div className="bg-white border border-gray-200 rounded px-2 py-1 flex items-center space-x-1">
                 <img src="/icons/mastercard.png" alt="Mastercard Logo" className="w-15 h-8" />
                </div>
              
              {/* Troy Logo */}
              <div className="bg-white border border-gray-200 rounded px-2 py-1 flex items-center space-x-1">
                 <img src="/icons/troy.png" alt="TROY Logo" className="w-10 h-10" />
                </div>
              
              {/* SSL Güvenlik */}
              <div className="flex items-center space-x-1 bg-green-50 border border-green-200 rounded px-2 py-1">
                <svg className="h-4 w-4 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z" />
                </svg>
                <span className="text-green-600 font-medium text-xs">SSL</span>
              </div>
            </div>
          </div>
        </div>

        {/* Alt Bilgiler ve Yasal Linkler */}
        <div className="mt-8 pt-8 border-t border-gray-200">
          <div className="flex flex-col md:flex-row justify-between items-center space-y-4 md:space-y-0">
            
            {/* Copyright */}
            <div className="text-sm text-gray-600">
              © 2025 LCW Market. Tüm hakları saklıdır.
            </div>

            {/* Yasal Linkler */}
            <div className="flex flex-wrap justify-center md:justify-end items-center space-x-6 text-sm">
              <Link href="/gizlilik-politikasi" className="text-gray-600 hover:text-blue-600 transition-colors duration-200">
                Gizlilik Politikası
              </Link>
              <Link href="/kvkk" className="text-gray-600 hover:text-blue-600 transition-colors duration-200">
                KVKK Açık Rıza Metni
              </Link>
              <Link href="/kullanici-sozlesmesi" className="text-gray-600 hover:text-blue-600 transition-colors duration-200">
                Kullanıcı Sözleşmesi
              </Link>
              <Link href="/cerez-politikasi" className="text-gray-600 hover:text-blue-600 transition-colors duration-200">
                Çerez Politikası
              </Link>
            </div>
          </div>
        </div>
      </div>
    </footer>
  );
} 