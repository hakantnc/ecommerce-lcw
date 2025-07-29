"use client";
import { useState } from "react";
import { useParams } from "next/navigation";
import { categoryAPI, subcategoryAPI, productAPI } from "@/lib/api";
import { useQuery } from "@tanstack/react-query";

interface Category {
  id: number;
  name: string;
  description?: string;
}

interface Subcategory {
  id: number;
  name: string;
  categoryId: number;
}

interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  categoryId: number;
  sub_id?: number;
}

export const useCategory = (id: number) =>
  useQuery({
    queryKey: ["category", id],
    queryFn: () => categoryAPI.getById(id),
    enabled: !!id,
  });

export const useSubcategories = (categoryId: number) =>
  useQuery({
    queryKey: ["subcategories", categoryId],
    queryFn: () => subcategoryAPI.getByCategory(categoryId),
    enabled: !!categoryId,
  });

export const useProducts = () =>
  useQuery({
    queryKey: ["products"],
    queryFn: () => productAPI.getAll(),
  });

export default function CategoryDetailPage() {
  const params = useParams();
  const categoryId = Number(params.id);
  const [selectedSub, setSelectedSub] = useState<number | null>(null);

  const { data: category, isLoading: categoryLoading } = useCategory(categoryId);
  const { data: subcategories = [], isLoading: subcategoriesLoading } = useSubcategories(categoryId);
  const { data: allProducts = [], isLoading: productsLoading } = useProducts();

  const loading = categoryLoading || subcategoriesLoading || productsLoading;
  const products = allProducts.filter((p: Product) => p.categoryId === categoryId);
  const filteredProducts = selectedSub
    ? products.filter((p) => p.subcategoryId === selectedSub)
    : products;

  const selectedSubName = selectedSub
    ? subcategories.find((s: Subcategory) => s.id === selectedSub)?.name
    : null;

  return (
    <div className="min-h-screen bg-gradient-to-b from-gray-50 to-white">
      {/* Kategori Başlığı */}
      <div className="bg-white border-b border-gray-100 shadow-sm mb-8">
        <div className="max-w-7xl mx-auto px-4 py-8">
          <div className="flex flex-col md:flex-row justify-between items-start md:items-center gap-4">
            <div className="flex-1">
              <h1 className="text-3xl font-bold text-gray-900 flex items-center gap-3">
                <span className="w-10 h-10 rounded-xl bg-blue-50 flex items-center justify-center">
                  <svg className="w-6 h-6 text-blue-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                  </svg>
                </span>
                {category?.name}
              </h1>
              <p className="mt-2 text-gray-600 max-w-2xl">
                {category?.description || "Bu kategoriye ait açıklama bulunmamaktadır."}
              </p>
            </div>
            <div className="flex gap-3">
              <div className="px-4 py-2 bg-blue-50 rounded-xl">
                <span className="block text-sm text-gray-500">Toplam Ürün</span>
                <span className="text-xl font-bold text-blue-600">{filteredProducts.length}</span>
              </div>
             
            </div>
          </div>
        </div>
      </div>

      <div className="max-w-7xl mx-auto px-4 pb-12">
        <div className="flex flex-col lg:flex-row gap-8">
          {/* Sol Menü: Alt Kategoriler */}
          <aside className="lg:w-1/4 w-full lg:sticky lg:top-24 h-fit">
            <div className="bg-white rounded-2xl shadow-sm p-6 border border-gray-100">
              <div className="flex items-center gap-2 mb-6">
                <div className="w-8 h-8 rounded-lg bg-blue-50 flex items-center justify-center">
                  <svg className="w-5 h-5 text-blue-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 6h16M4 12h16M4 18h16" />
                  </svg>
                </div>
                <h2 className="text-lg font-bold text-gray-900">Alt Kategoriler</h2>
              </div>
              <div className="space-y-2">
                <button
                  onClick={() => setSelectedSub(null)}
                  className={`w-full px-4 py-3 rounded-xl text-left transition-all duration-200 group ${
                    selectedSub === null
                      ? "bg-blue-600 text-white shadow-md hover:shadow-lg"
                      : "hover:bg-gray-50 text-gray-700"
                  }`}
                >
                  <div className="flex items-center gap-3">
                    <span className={`block w-2 h-2 rounded-full ${selectedSub === null ? "bg-white" : "bg-blue-500 group-hover:bg-blue-600"}`} />
                    <span className="font-medium">Tüm Ürünler</span>
                  </div>
                </button>
                {subcategories.map((sub: Subcategory) => (
                  <button
                    key={sub.id}
                    onClick={() => setSelectedSub(sub.id)}
                    className={`w-full px-4 py-3 rounded-xl text-left transition-all duration-200 group ${
                      selectedSub === sub.id
                        ? "bg-blue-600 text-white shadow-md hover:shadow-lg"
                        : "hover:bg-gray-50 text-gray-700"
                    }`}
                  >
                    <div className="flex items-center gap-3">
                      <span className={`block w-2 h-2 rounded-full ${selectedSub === sub.id ? "bg-white" : "bg-blue-500 group-hover:bg-blue-600"}`} />
                      <span className="font-medium">{sub.name}</span>
                    </div>
                  </button>
                ))}
              </div>
            </div>
          </aside>

          {/* Sağ: Ürünler */}
          <main className="flex-1">
            {selectedSubName && (
              <div className="mb-6 px-4 py-2 bg-blue-50 inline-block rounded-lg">
                <span className="text-blue-700 font-medium">{selectedSubName}</span>
                <button 
                  onClick={() => setSelectedSub(null)}
                  className="ml-2 text-blue-400 hover:text-blue-600"
                >
                  ×
                </button>
              </div>
            )}

            {loading ? (
              <div className="flex flex-col items-center justify-center py-32">
                <div className="w-16 h-16 border-4 border-blue-200 border-t-blue-500 rounded-full animate-spin mb-4"></div>
                <p className="text-gray-500 font-medium">Ürünler yükleniyor...</p>
              </div>
            ) : filteredProducts.length === 0 ? (
              <div className="flex flex-col items-center justify-center py-32 text-center">
                <div className="w-16 h-16 bg-gray-100 rounded-full flex items-center justify-center mb-4">
                  <svg className="w-8 h-8 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 13V6a2 2 0 00-2-2H6a2 2 0 00-2 2v7m16 0v5a2 2 0 01-2 2H6a2 2 0 01-2-2v-5m16 0h-2.586a1 1 0 00-.707.293l-2.414 2.414a1 1 0 01-.707.293h-3.172a1 1 0 01-.707-.293l-2.414-2.414A1 1 0 006.586 13H4" />
                  </svg>
                </div>
                <h3 className="text-lg font-medium text-gray-900 mb-1">Ürün Bulunamadı</h3>
                <p className="text-gray-500">Bu kategoride henüz ürün bulunmuyor.</p>
              </div>
            ) : (
              <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
                {filteredProducts.map((product: Product) => (
                  <div
                    key={product.id}
                    className="group bg-white rounded-2xl shadow-sm hover:shadow-md transition-all duration-200 overflow-hidden border border-gray-100"
                  >
                    <div className="aspect-square bg-gray-50 relative overflow-hidden">
                      {product.imageUrl ? (
                        <img
                          src={product.imageUrl}
                          alt={product.name}
                          className="w-full h-full object-contain p-4 group-hover:scale-105 transition-transform duration-300"
                        />
                      ) : (
                        <div className="w-full h-full flex items-center justify-center">
                          <svg className="w-12 h-12 text-gray-300" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M4 16l4.586-4.586a2 2 0 012.828 0L16 16m-2-2l1.586-1.586a2 2 0 012.828 0L20 14m-6-6h.01M6 20h12a2 2 0 002-2V6a2 2 0 00-2-2H6a2 2 0 00-2 2v12a2 2 0 002 2z" />
                          </svg>
                        </div>
                      )}
                    </div>
                    <div className="p-4">
                      <h3 className="font-medium text-gray-900 group-hover:text-blue-600 transition-colors duration-200 mb-1 line-clamp-2">
                        {product.name}
                      </h3>
                      <p className="text-sm text-gray-500 mb-4 line-clamp-2">
                        {product.description}
                      </p>
                      <div className="flex items-center justify-between">
                        <span className="text-lg font-bold text-blue-600">
                          {product.price.toLocaleString("tr-TR", {
                            style: "currency",
                            currency: "TRY",
                          })}
                        </span>
                        <button className="px-4 py-2 bg-blue-50 text-blue-600 rounded-lg text-sm font-medium hover:bg-blue-100 transition-colors duration-200">
                          İncele
                        </button>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            )}
          </main>
        </div>
      </div>
    </div>
  );
} 