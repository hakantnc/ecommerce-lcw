// Admin Dashboard - Ana yönetim paneli sayfası
// Bu sayfa sistemin genel durumunu gösterir ve hızlı erişim sağlar

'use client';

import { useEffect, useState } from 'react';
import Link from 'next/link';
import { categoryAPI, productAPI, customerAPI, orderAPI } from '@/lib/api';

export default function AdminDashboard() {
  // State'ler - Sayfa verilerini tutmak için
  const [stats, setStats] = useState({
    totalCategories: 0,
    totalProducts: 0,
    totalCustomers: 0,
    totalOrders: 0,
    loading: true,
    error: null as string | null
  });

  // Sayfa yüklendiğinde istatistikleri çek
  useEffect(() => {
    fetchDashboardStats();
  }, []);

  // Dashboard istatistiklerini API'den çeken fonksiyon
  const fetchDashboardStats = async () => {
    try {
      setStats(prev => ({ ...prev, loading: true, error: null }));

      // Paralel olarak tüm verileri çek
      const [categories, products, customers, orders] = await Promise.all([
        categoryAPI.getAll().catch(() => []),
        productAPI.getAll().catch(() => []),
        customerAPI.getAll().catch(() => []),
        orderAPI.getAll().catch(() => [])
      ]);

      setStats({
        totalCategories: categories.length || 0,
        totalProducts: products.length || 0,
        totalCustomers: customers.length || 0,
        totalOrders: orders.length || 0,
        loading: false,
        error: null
      });
    } catch (error) {
      console.error('Dashboard stats yüklenirken hata:', error);
      setStats(prev => ({
        ...prev,
        loading: false,
        error: 'İstatistikler yüklenirken bir hata oluştu. Lütfen API\'nin çalıştığından emin olun.'
      }));
    }
  };

  return (
    <div>
      {/* Sayfa Başlığı */}
      <div className="mb-8">
        <h1 className="text-3xl font-bold text-gray-900">Dashboard</h1>
        <p className="mt-2 text-gray-600">
          LC Market yönetim paneline hoş geldiniz. Sistemin genel durumunu burada görebilirsiniz.
        </p>
      </div>

      {/* Hata Mesajı */}
      {stats.error && (
        <div className="mb-6 bg-red-50 border border-red-200 rounded-md p-4">
          <div className="flex">
            <div className="flex-shrink-0">
              <svg className="h-5 w-5 text-red-400" fill="currentColor" viewBox="0 0 20 20">
                <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
              </svg>
            </div>
            <div className="ml-3">
              <h3 className="text-sm font-medium text-red-800">Bağlantı Hatası</h3>
              <p className="text-sm text-red-700 mt-1">{stats.error}</p>
              <button
                onClick={fetchDashboardStats}
                className="mt-2 text-sm text-red-600 underline hover:text-red-500"
              >
                Tekrar Dene
              </button>
            </div>
          </div>
        </div>
      )}

      {/* İstatistik Kartları */}
      <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4 mb-8">
        {/* Kategoriler */}
        <div className="bg-white overflow-hidden shadow rounded-lg">
          <div className="p-5">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <svg className="h-6 w-6 text-blue-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.99 1.99 0 013 12V7a4 4 0 014-4z" />
                </svg>
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Toplam Kategori
                  </dt>
                  <dd className="text-lg font-medium text-gray-900">
                    {stats.loading ? '...' : stats.totalCategories}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-gray-50 px-5 py-3">
            <div className="text-sm">
              <Link href="/admin/categories" className="font-medium text-blue-600 hover:text-blue-500">
                Kategorileri Görüntüle
              </Link>
            </div>
          </div>
        </div>

        {/* Ürünler */}
        <div className="bg-white overflow-hidden shadow rounded-lg">
          <div className="p-5">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <svg className="h-6 w-6 text-green-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
                </svg>
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Toplam Ürün
                  </dt>
                  <dd className="text-lg font-medium text-gray-900">
                    {stats.loading ? '...' : stats.totalProducts}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-gray-50 px-5 py-3">
            <div className="text-sm">
              <Link href="/admin/products" className="font-medium text-green-600 hover:text-green-500">
                Ürünleri Görüntüle
              </Link>
            </div>
          </div>
        </div>

        {/* Müşteriler */}
        <div className="bg-white overflow-hidden shadow rounded-lg">
          <div className="p-5">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <svg className="h-6 w-6 text-purple-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197m13.5-9a2.5 2.5 0 11-5 0 2.5 2.5 0 015 0z" />
                </svg>
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Toplam Müşteri
                  </dt>
                  <dd className="text-lg font-medium text-gray-900">
                    {stats.loading ? '...' : stats.totalCustomers}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-gray-50 px-5 py-3">
            <div className="text-sm">
              <Link href="/admin/customers" className="font-medium text-purple-600 hover:text-purple-500">
                Müşterileri Görüntüle
              </Link>
            </div>
          </div>
        </div>

        {/* Siparişler */}
        <div className="bg-white overflow-hidden shadow rounded-lg">
          <div className="p-5">
            <div className="flex items-center">
              <div className="flex-shrink-0">
                <svg className="h-6 w-6 text-orange-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5H7a2 2 0 00-2 2v10a2 2 0 002 2h8a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
                </svg>
              </div>
              <div className="ml-5 w-0 flex-1">
                <dl>
                  <dt className="text-sm font-medium text-gray-500 truncate">
                    Toplam Sipariş
                  </dt>
                  <dd className="text-lg font-medium text-gray-900">
                    {stats.loading ? '...' : stats.totalOrders}
                  </dd>
                </dl>
              </div>
            </div>
          </div>
          <div className="bg-gray-50 px-5 py-3">
            <div className="text-sm">
              <Link href="/admin/orders" className="font-medium text-orange-600 hover:text-orange-500">
                Siparişleri Görüntüle
              </Link>
            </div>
          </div>
        </div>
      </div>

      {/* Hızlı İşlemler */}
      <div className="bg-white shadow rounded-lg">
        <div className="px-4 py-5 sm:p-6">
          <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
            Hızlı İşlemler
          </h3>
          <div className="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3">
            <Link
              href="/admin/categories"
              className="relative group bg-white p-6 focus-within:ring-2 focus-within:ring-inset focus-within:ring-blue-500 border border-gray-200 rounded-lg hover:bg-gray-50"
            >
              <div>
                <span className="rounded-lg inline-flex p-3 bg-blue-50 text-blue-600 group-hover:bg-blue-100">
                  <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
                  </svg>
                </span>
              </div>
              <div className="mt-8">
                <h3 className="text-lg font-medium">
                  Yeni Kategori Ekle
                </h3>
                <p className="mt-2 text-sm text-gray-500">
                  Sisteme yeni bir ürün kategorisi ekleyebilirsiniz.
                </p>
              </div>
            </Link>

            <Link
              href="/admin/products"
              className="relative group bg-white p-6 focus-within:ring-2 focus-within:ring-inset focus-within:ring-green-500 border border-gray-200 rounded-lg hover:bg-gray-50"
            >
              <div>
                <span className="rounded-lg inline-flex p-3 bg-green-50 text-green-600 group-hover:bg-green-100">
                  <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
                  </svg>
                </span>
              </div>
              <div className="mt-8">
                <h3 className="text-lg font-medium">
                  Yeni Ürün Ekle
                </h3>
                <p className="mt-2 text-sm text-gray-500">
                  Sisteme yeni bir ürün ekleyebilirsiniz.
                </p>
              </div>
            </Link>

            <Link
              href="/admin/orders"
              className="relative group bg-white p-6 focus-within:ring-2 focus-within:ring-inset focus-within:ring-orange-500 border border-gray-200 rounded-lg hover:bg-gray-50"
            >
              <div>
                <span className="rounded-lg inline-flex p-3 bg-orange-50 text-orange-600 group-hover:bg-orange-100">
                  <svg className="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5H7a2 2 0 00-2 2v10a2 2 0 002 2h8a2 2 0 002-2V7a2 2 0 00-2-2h-2M9 5a2 2 0 002 2h2a2 2 0 002-2M9 5a2 2 0 012-2h2a2 2 0 012 2" />
                  </svg>
                </span>
              </div>
              <div className="mt-8">
                <h3 className="text-lg font-medium">
                  Siparişleri Yönet
                </h3>
                <p className="mt-2 text-sm text-gray-500">
                  Gelen siparişleri görüntüleyip yönetebilirsiniz.
                </p>
              </div>
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
} 