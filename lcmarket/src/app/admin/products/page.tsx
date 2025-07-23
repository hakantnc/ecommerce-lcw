// Ürün Yönetimi Sayfası - Ürünleri listeleme, ekleme, düzenleme ve silme
// Bu sayfa ürünler için tam CRUD operasyonlarını sağlar

'use client';

import { useEffect, useState } from 'react';
import { productAPI, categoryAPI } from '@/lib/api';

// Ürün tipi tanımı
interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  stock: number;
  imageUrl?: string;
  categoryId?: number;
  isActive: boolean;
  supplierId?: number;
}

// Kategori tipi tanımı
interface Category {
  id: number;
  name: string;
  description?: string;
}

export default function ProductsPage() {
  // State'ler
  const [products, setProducts] = useState<Product[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    price: '',
    stock: '',
    imageUrl: '',
    categoryId: '',
    isActive: true,
    supplierId: ''
  });

  // Sayfa yüklendiğinde ürünleri ve kategorileri getir
  useEffect(() => {
    fetchProducts();
    fetchCategories();
  }, []);

  // Ürünleri API'den çeken fonksiyon
  const fetchProducts = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = await productAPI.getAll();
      setProducts(data || []);
    } catch (err) {
      console.error('Ürünler yüklenirken hata:', err);
      setError('Ürünler yüklenirken bir hata oluştu. API bağlantısını kontrol edin.');
    } finally {
      setLoading(false);
    }
  };

  // Kategorileri API'den çeken fonksiyon
  const fetchCategories = async () => {
    try {
      const data = await categoryAPI.getAll();
      setCategories(data || []);
    } catch (err) {
      console.error('Kategoriler yüklenirken hata:', err);
    }
  };

  // Form gönderme fonksiyonu
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!formData.name.trim()) {
      setError('Ürün adı gereklidir.');
      return;
    }

    if (!formData.price || isNaN(parseFloat(formData.price)) || parseFloat(formData.price) < 0) {
      setError('Geçerli bir fiyat giriniz.');
      return;
    }

    if (!formData.stock || isNaN(parseInt(formData.stock)) || parseInt(formData.stock) < 0) {
      setError('Geçerli bir stok miktarı giriniz.');
      return;
    }

    if (!formData.supplierId || isNaN(parseInt(formData.supplierId)) || parseInt(formData.supplierId) <= 0) {
      setError('Geçerli bir tedarikçi ID giriniz.');
      return;
    }

    try {
      setError(null);
      
      const productData = {
        name: formData.name.trim(),
        description: formData.description.trim(),
        price: parseFloat(formData.price),
        stock: parseInt(formData.stock),
        imageUrl: formData.imageUrl.trim(),
        categoryId: formData.categoryId && formData.categoryId !== '' ? parseInt(formData.categoryId) : null,
        isActive: formData.isActive,
        supplierId: formData.supplierId && formData.supplierId !== '' ? parseInt(formData.supplierId) : null
      };
      
      console.log('📝 Form Data:', formData);
      console.log('📦 Product Data:', productData);
      
      if (editingProduct) {
        // Güncelleme
        await productAPI.update(editingProduct.id, productData);
      } else {
        // Yeni ekleme
        await productAPI.create(productData);
      }
      
      // Formu temizle ve listeyi yenile
      setFormData({
        name: '',
        description: '',
        price: '',
        stock: '',
        imageUrl: '',
        categoryId: '',
        isActive: true,
        supplierId: ''
      });
      setEditingProduct(null);
      setShowForm(false);
      await fetchProducts();
    } catch (err) {
      console.error('İşlem sırasında hata:', err);
      setError('İşlem gerçekleştirilemedi. Lütfen tekrar deneyin.');
    }
  };

  // Düzenleme başlat
  const startEdit = (product: Product) => {
    setEditingProduct(product);
    setFormData({
      name: product.name || '',
      description: product.description || '',
      price: product.price?.toString() || '',
      stock: product.stock?.toString() || '',
      imageUrl: product.imageUrl || '',
      categoryId: product.categoryId?.toString() || '',
      isActive: product.isActive !== false,
      supplierId: product.supplierId?.toString() || ''
    });
    setShowForm(true);
    setError(null);
  };

  // Silme fonksiyonu
  const handleDelete = async (id: number) => {
    if (!confirm('Bu ürünü silmek istediğinizden emin misiniz?')) {
      return;
    }

    try {
      setError(null);
      await productAPI.delete(id);
      await fetchProducts();
    } catch (err) {
      console.error('Silme sırasında hata:', err);
      setError('Ürün silinemedi. Lütfen tekrar deneyin.');
    }
  };

  // Formu iptal et
  const cancelForm = () => {
    setFormData({
      name: '',
      description: '',
      price: '',
      stock: '',
      imageUrl: '',
      categoryId: '',
      isActive: true,
      supplierId: ''
    });
    setEditingProduct(null);
    setShowForm(false);
    setError(null);
  };

  // Kategori adını getir
  const getCategoryName = (categoryId: number) => {
    const category = categories.find(cat => cat.id === categoryId);
    return category ? category.name : 'Kategorisiz';
  };

  // Fiyat formatı
  const formatPrice = (price: number) => {
    return new Intl.NumberFormat('tr-TR', {
      style: 'currency',
      currency: 'TRY'
    }).format(price);
  };

  return (
    <div>
      {/* Sayfa Başlığı */}
      <div className="mb-8">
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">Ürün Yönetimi</h1>
            <p className="mt-2 text-gray-600">
              Ürünleri görüntüleyebilir, ekleyebilir, düzenleyebilir ve silebilirsiniz.
            </p>
          </div>
          <button
            onClick={() => setShowForm(true)}
            className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md text-sm font-medium"
          >
            Yeni Ürün Ekle
          </button>
        </div>
      </div>

      {/* Hata Mesajı */}
      {error && (
        <div className="mb-6 bg-red-50 border border-red-200 rounded-md p-4">
          <div className="flex">
            <svg className="h-5 w-5 text-red-400" fill="currentColor" viewBox="0 0 20 20">
              <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
            </svg>
            <div className="ml-3">
              <p className="text-sm text-red-700">{error}</p>
            </div>
          </div>
        </div>
      )}

      {/* Ürün Ekleme/Düzenleme Formu */}
      {showForm && (
        <div className="mb-8 bg-white shadow rounded-lg p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">
            {editingProduct ? 'Ürün Düzenle' : 'Yeni Ürün Ekle'}
          </h3>
          
          <form onSubmit={handleSubmit} className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              {/* Ürün Adı */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Ürün Adı *
                </label>
                <input
                  type="text"
                  value={formData.name}
                  onChange={(e) => setFormData(prev => ({ ...prev, name: e.target.value }))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  placeholder="Ürün adını girin"
                  required
                />
              </div>

              {/* Kategori */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Kategori
                </label>
                <select
                  value={formData.categoryId}
                  onChange={(e) => setFormData(prev => ({ ...prev, categoryId: e.target.value }))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                >
                  <option value="">Kategori Seçiniz</option>
                  {categories.map(category => (
                    <option key={category.id} value={category.id}>
                      {category.name}
                    </option>
                  ))}
                </select>
              </div>

              {/* Fiyat */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Fiyat (TL) *
                </label>
                <input
                  type="number"
                  step="0.01"
                  min="0"
                  value={formData.price}
                  onChange={(e) => setFormData(prev => ({ ...prev, price: e.target.value }))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  placeholder="0.00"
                  required
                />
              </div>
              
              {/* Stok */}
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Stok Miktarı *
                </label>
                <input
                  type="number"
                  min="0"
                  value={formData.stock}
                  onChange={(e) => setFormData(prev => ({ ...prev, stock: e.target.value }))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  placeholder="0"
                  required
                />
              </div>
            
            <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">
                  Tedarikçi ID
                </label>
                <input
                  type="number"
                  min="0"
                  value={formData.supplierId}
                  onChange={(e) => setFormData(prev => ({ ...prev, supplierId: e.target.value }))}
                  className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                  placeholder="0"
                  required
                />
              </div>
            </div>

            {/* Açıklama */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Açıklama
              </label>
              <textarea
                value={formData.description}
                onChange={(e) => setFormData(prev => ({ ...prev, description: e.target.value }))}
                rows={3}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                placeholder="Ürün açıklaması (isteğe bağlı)"
              />
            </div>

            {/* Görsel URL */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Görsel URL
              </label>
              <input
                type="url"
                value={formData.imageUrl}
                onChange={(e) => setFormData(prev => ({ ...prev, imageUrl: e.target.value }))}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                placeholder="https://example.com/image.jpg"
              />
            </div>

            {/* Aktif Durumu */}
            <div>
              <label className="flex items-center">
                <input
                  type="checkbox"
                  checked={formData.isActive}
                  onChange={(e) => setFormData(prev => ({ ...prev, isActive: e.target.checked }))}
                  className="h-4 w-4 text-blue-600 focus:ring-blue-500 border-gray-300 rounded"
                />
                <span className="ml-2 text-sm text-gray-700">Ürün aktif</span>
              </label>
            </div>
            
            <div className="flex space-x-3">
              <button
                type="submit"
                className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md text-sm font-medium"
              >
                {editingProduct ? 'Güncelle' : 'Kaydet'}
              </button>
              <button
                type="button"
                onClick={cancelForm}
                className="bg-gray-300 hover:bg-gray-400 text-gray-700 px-4 py-2 rounded-md text-sm font-medium"
              >
                İptal
              </button>
            </div>
          </form>
        </div>
      )}

      {/* Ürünler Listesi */}
      <div className="bg-white shadow rounded-lg">
        <div className="px-4 py-5 sm:p-6">
          <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
            Ürünler ({products.length})
          </h3>
          
          {loading ? (
            <div className="text-center py-8">
              <div className="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
              <p className="mt-2 text-gray-600">Ürünler yükleniyor...</p>
            </div>
          ) : products.length === 0 ? (
            <div className="text-center py-8">
              <svg className="mx-auto h-12 w-12 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 7l-8-4-8 4m16 0l-8 4m8-4v10l-8 4m0-10L4 7m8 4v10M4 7v10l8 4" />
              </svg>
              <h3 className="mt-2 text-sm font-medium text-gray-900">Ürün bulunamadı</h3>
              <p className="mt-1 text-sm text-gray-500">
                Henüz hiç ürün eklenmemiş. İlk ürünü eklemek için yukarıdaki butonu kullanın.
              </p>
            </div>
          ) : (
            <div className="overflow-hidden shadow ring-1 ring-black ring-opacity-5 md:rounded-lg">
              <table className="min-w-full divide-y divide-gray-300">
                <thead className="bg-gray-50">
                  <tr>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      ID
                    </th>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Ürün Adı
                    </th>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Kategori
                    </th>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Fiyat
                    </th>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Stok
                    </th>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Durum
                    </th>
                    <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                      İşlemler
                    </th>
                  </tr>
                </thead>
                <tbody className="bg-white divide-y divide-gray-200">
                  {products.map((product) => (
                    <tr key={product.id} className="hover:bg-gray-50">
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                        #{product.id}
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="flex items-center">
                          {product.imageUrl && (
                            <img 
                              src={product.imageUrl} 
                              alt={product.name}
                              className="h-10 w-10 rounded-md object-cover mr-3"
                              onError={(e) => {
                                (e.target as HTMLImageElement).style.display = 'none';
                              }}
                            />
                          )}
                          <div>
                            <div className="text-sm font-medium text-gray-900">
                              {product.name}
                            </div>
                            {product.description && (
                              <div className="text-sm text-gray-500 truncate max-w-xs">
                                {product.description}
                              </div>
                            )}
                          </div>
                        </div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">
                        {product.categoryId ? getCategoryName(product.categoryId) : 'Kategorisiz'}
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">
                        {formatPrice(product.price)}
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${
                          product.stock > 10 
                            ? 'bg-green-100 text-green-800' 
                            : product.stock > 0 
                            ? 'bg-yellow-100 text-yellow-800' 
                            : 'bg-red-100 text-red-800'
                        }`}>
                          {product.stock} adet
                        </span>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <span className={`inline-flex px-2 py-1 text-xs font-semibold rounded-full ${
                          product.isActive 
                            ? 'bg-green-100 text-green-800' 
                            : 'bg-red-100 text-red-800'
                        }`}>
                          {product.isActive ? 'Aktif' : 'Pasif'}
                        </span>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                        <button
                          onClick={() => startEdit(product)}
                          className="text-blue-600 hover:text-blue-900 mr-4"
                        >
                          Düzenle
                        </button>
                        <button
                          onClick={() => handleDelete(product.id)}
                          className="text-red-600 hover:text-red-900"
                        >
                          Sil
                        </button>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
    </div>
  );
} 