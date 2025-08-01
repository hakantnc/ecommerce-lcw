// Kategori Yönetimi Sayfası - Kategorileri listeleme, ekleme, düzenleme ve silme
// Bu sayfa kategoriler için tam CRUD operasyonlarını sağlar

'use client';

import { useEffect, useState } from 'react';
import { categoryAPI } from '@/lib/api';
import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { subcategoryAPI } from '@/lib/api';
// Kategori tipi tanımı
interface Category {
  id: number;
  name: string;
  description?: string;
}
// Subcategory tipi tanımı
interface Subcategory {
  id: number;
  name: string;
  categoryId: number;
}

export default function CategoriesPage() {
  // State'ler
  
  const [editingCategory, setEditingCategory] = useState<Category | null>(null);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    name: '',
    description: ''
  });

  const queryClient = useQueryClient()

  const { data: categories = [], isLoading, isError } = useQuery({
    queryKey: ['categories'],
    queryFn: categoryAPI.getAll,
  })
  const [error, setError] = useState<string | null>(null);
  // Alt Kategori yönetimi için state'ler
  const [subcategories, setSubcategories] = useState<Subcategory[]>([]);
  const [subForm, setSubForm] = useState({
    name: '',
    categoryId: ''
  });
  const [subError, setSubError] = useState<string | null>(null);
  const [subLoading, setSubLoading] = useState(false);

  // Form gönderme fonksiyonu
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!formData.name.trim()) {
      setError('Kategori adı gereklidir.');
      return;
    }

    try {
      setError(null);
      
      if (editingCategory) {
        // Güncelleme
        await categoryAPI.update(editingCategory.id, formData);
      } else {
        // Yeni ekleme
        await categoryAPI.create(formData);
      }
      
      // Formu temizle ve listeyi yenile
      setFormData({ name: '', description: '' });
      setEditingCategory(null);
      setShowForm(false);
      queryClient.invalidateQueries({ queryKey: ['categories'] });
    } catch (err) {
      console.error('İşlem sırasında hata:', err);
      setError('İşlem gerçekleştirilemedi. Lütfen tekrar deneyin.');
    }
  };

  // Düzenleme başlat
  const startEdit = (category: Category) => {
    setEditingCategory(category);
    setFormData({
      name: category.name || '',
      description: category.description || ''
    });
    setShowForm(true);
    setError(null);
  };

  // Silme fonksiyonu
  const handleDelete = async (id: number) => {
    if (!confirm('Bu kategoriyi silmek istediğinizden emin misiniz?')) {
      return;
    }

    try {
      setError(null);
      await categoryAPI.delete(id);
      queryClient.invalidateQueries({ queryKey: ['categories'] });
    } catch (err) {
      console.error('Silme sırasında hata:', err);
      setError('Kategori silinemedi. Lütfen tekrar deneyin.');
    }
  };

  // Formu iptal et
  const cancelForm = () => {
    setFormData({ name: '', description: '' });
    setEditingCategory(null);
    setShowForm(false);
    setError(null);
  };

  // Alt kategorileri getir
  const fetchSubcategories = async () => {
    setSubLoading(true);
    try {
      const allSubcategories = await subcategoryAPI.getAll();
      setSubcategories(allSubcategories);
    } catch (e) {
      console.error('Alt kategoriler yüklenirken hata:', e);
      setSubcategories([]);
    } finally {
      setSubLoading(false);
    }
  };

  useEffect(() => {
    fetchSubcategories();
  }, [categories]);

  // Alt kategori ekle
  const handleSubSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!subForm.name.trim() || !subForm.categoryId) {
      setSubError('Alt kategori adı ve kategori seçimi zorunludur.');
      return;
    }
    setSubError(null);
    try {
      await subcategoryAPI.create({
        name: subForm.name,
        categoryId: Number(subForm.categoryId)
      });
      setSubForm({ name: '', categoryId: '' });
      fetchSubcategories();
    } catch (e) {
      console.error('Alt kategori eklenirken hata:', e);
      setSubError('Alt kategori eklenemedi.');
    }
  };

  // Alt kategori sil
  const handleSubDelete = async (id: number) => {
    if (!confirm('Bu alt kategoriyi silmek istediğinizden emin misiniz?')) return;
    try {
      await subcategoryAPI.delete(id);
      fetchSubcategories();
    } catch (e) {
      console.error('Alt kategori silinirken hata:', e);
      setSubError('Alt kategori silinemedi.');
    }
  };

  return (
    <div>
      {/* Sayfa Başlığı */}
      <div className="mb-8">
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-3xl font-bold text-gray-900">Kategori Yönetimi</h1>
            <p className="mt-2 text-gray-600">
              Ürün kategorilerini görüntüleyebilir, ekleyebilir, düzenleyebilir ve silebilirsiniz.
            </p>
          </div>
          <button
            onClick={() => setShowForm(true)}
            className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md text-sm font-medium"
          >
            Yeni Kategori Ekle
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

      {/* Kategori Ekleme/Düzenleme Formu */}
      {showForm && (
        <div className="mb-8 bg-white shadow rounded-lg p-6">
          <h3 className="text-lg font-medium text-gray-900 mb-4">
            {editingCategory ? 'Kategori Düzenle' : 'Yeni Kategori Ekle'}
          </h3>
          
          <form onSubmit={handleSubmit} className="space-y-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Kategori Adı *
              </label>
              <input
                type="text"
                value={formData.name}
                onChange={(e) => setFormData(prev => ({ ...prev, name: e.target.value }))}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                placeholder="Kategori adını girin"
                required
              />
            </div>
            
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Açıklama
              </label>
              <textarea
                value={formData.description}
                onChange={(e) => setFormData(prev => ({ ...prev, description: e.target.value }))}
                rows={3}
                className="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
                placeholder="Kategori açıklaması (isteğe bağlı)"
              />
            </div>
            
            <div className="flex space-x-3">
              <button
                type="submit"
                className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md text-sm font-medium"
              >
                {editingCategory ? 'Güncelle' : 'Kaydet'}
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

      {/* Kategoriler Listesi */}
      <div className="bg-white shadow rounded-lg">
        <div className="px-4 py-5 sm:p-6">
          <h3 className="text-lg leading-6 font-medium text-gray-900 mb-4">
            Kategoriler ({categories.length})
          </h3>
          
          {isLoading ? (
            <div className="text-center py-8">
              <div className="inline-block animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
              <p className="mt-2 text-gray-600">Kategoriler yükleniyor...</p>
            </div>
          ) : categories.length === 0 ? (
            <div className="text-center py-8">
              <svg className="mx-auto h-12 w-12 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 7h.01M7 3h5c.512 0 1.024.195 1.414.586l7 7a2 2 0 010 2.828l-7 7a2 2 0 01-2.828 0l-7-7A1.99 1.99 0 013 12V7a4 4 0 014-4z" />
              </svg>
              <h3 className="mt-2 text-sm font-medium text-gray-900">Kategori bulunamadı</h3>
              <p className="mt-1 text-sm text-gray-500">
                Henüz hiç kategori eklenmemiş. İlk kategoriyi eklemek için yukarıdaki butonu kullanın.
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
                      Kategori Adı
                    </th>
                    <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                      Açıklama
                    </th>
                    <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">
                      İşlemler
                    </th>
                  </tr>
                </thead>
                <tbody className="bg-white divide-y divide-gray-200">
                  {categories.map((category) => (
                    <tr key={category.id} className="hover:bg-gray-50">
                      <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                        #{category.id}
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap">
                        <div className="text-sm font-medium text-gray-900">
                          {category.name}
                        </div>
                      </td>
                      <td className="px-6 py-4">
                        <div className="text-sm text-gray-500">
                          {category.description || 'Açıklama yok'}
                        </div>
                      </td>
                      <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                        <button
                          onClick={() => startEdit(category)}
                          className="text-blue-600 hover:text-blue-900 mr-4"
                        >
                          Düzenle
                        </button>
                        <button
                          onClick={() => handleDelete(category.id)}
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
      {/* Alt Kategori Yönetimi */}
      <div className="mt-12">
        <h2 className="text-2xl font-bold text-gray-900 mb-4">Alt Kategori Yönetimi</h2>
        <form onSubmit={handleSubSubmit} className="flex flex-col md:flex-row gap-4 mb-6">
          <input
            type="text"
            value={subForm.name}
            onChange={e => setSubForm(f => ({ ...f, name: e.target.value }))}
            placeholder="Alt kategori adı"
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            required
          />
          <select
            value={subForm.categoryId}
            onChange={e => setSubForm(f => ({ ...f, categoryId: e.target.value }))}
            className="px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500 focus:border-blue-500"
            required
          >
            <option value="">Kategori Seçiniz</option>
            {categories.map(cat => (
              <option key={cat.id} value={cat.id}>{cat.name}</option>
            ))}
          </select>
          <button type="submit" className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded-md font-medium">Ekle</button>
        </form>
        {subError && <div className="mb-4 text-red-600">{subError}</div>}
        <div className="overflow-hidden shadow ring-1 ring-black ring-opacity-5 md:rounded-lg">
          <table className="min-w-full divide-y divide-gray-300">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">ID</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Alt Kategori Adı</th>
                <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Kategori</th>
                <th className="px-6 py-3 text-right text-xs font-medium text-gray-500 uppercase tracking-wider">İşlemler</th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {subLoading ? (
                <tr><td colSpan={4} className="text-center py-4">Yükleniyor...</td></tr>
              ) : subcategories.length === 0 ? (
                <tr><td colSpan={4} className="text-center py-4">Alt kategori bulunamadı.</td></tr>
              ) : subcategories.map(sub => (
                <tr key={sub.id} className="hover:bg-gray-50">
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">#{sub.id}</td>
                  <td className="px-6 py-4 whitespace-nowrap">{sub.name}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{categories.find(c => c.id === sub.categoryId)?.name || '-'}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <button onClick={() => handleSubDelete(sub.id)} className="text-red-600 hover:text-red-900">Sil</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
} 