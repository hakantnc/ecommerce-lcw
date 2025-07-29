'use client';
import { useQuery } from '@tanstack/react-query';
import { useEffect, useState } from 'react';

export default function SearchInput() {
  const [inputValue, setInputValue] = useState('');
  const [debouncedQuery, setDebouncedQuery] = useState('');
  const [showDropdown, setShowDropdown] = useState(false);

  // 500ms debounce
  useEffect(() => {
    const timer = setTimeout(() => {
      setDebouncedQuery(inputValue.trim());
    }, 500);
    return () => clearTimeout(timer);
  }, [inputValue]);

  // useQuery: Arama sorgusu çalıştır
  const { data: results = [], isSuccess } = useQuery({
    queryKey: ['search-products', debouncedQuery],
    queryFn: async () => {
      if (!debouncedQuery) return [];
      const res = await fetch(`/api/product/search?query=${debouncedQuery}`);
      const text = await res.text();
      if (!text.trim()) return [];
      return JSON.parse(text);
    },
    enabled: !!debouncedQuery, // boşsa çağrılmasın
    staleTime: 1000 * 60, // önbellek süresi (opsiyonel)
  });

  // dropdown kontrolü
  useEffect(() => {
    if (debouncedQuery && results.length > 0) {
      setShowDropdown(true);
    } else {
      setShowDropdown(false);
    }
  }, [results, debouncedQuery]);

  return (
    <div className="relative">
      {/* Arama input'u */}
      <input
        type="text"
        placeholder="Binlerce ürün arasında ara..."
        value={inputValue}
        onChange={(e) => setInputValue(e.target.value)}
        className="block w-full pl-10 pr-12 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-blue-500 outline-none transition-colors duration-200"
      />

      {/* Dropdown sonuçlar */}
      {showDropdown && isSuccess && results.length > 0 && (
        <ul className="absolute left-0 right-0 top-full bg-white border border-gray-200 rounded-md shadow-md mt-2 max-h-80 overflow-y-auto z-50 divide-y">
          {results.map((product: any, index: number) => (
            <li
              key={index}
              onClick={() => {
                setShowDropdown(false);
              }}
              className="flex items-center px-4 py-2 hover:bg-gray-100 cursor-pointer gap-3"
            >
              {/* Görsel */}
              <img
                src={product.imageUrl || '/placeholder.png'}
                alt={product.name}
                className="w-12 h-12 object-cover rounded"
              />

              {/* Bilgiler */}
              <div className="flex-1">
                <div className="font-semibold text-sm text-gray-900">{product.name}</div>
                <div className="text-xs text-gray-500">
                  {product?.supplier_name || 'Tedarikçi yok'} •{' '}
                  {product?.subcategoryName || 'Alt kategori yok'}
                </div>
              </div>

              {/* Fiyat */}
              <div className="text-sm font-bold text-blue-600 whitespace-nowrap">
                ₺{product.price?.toFixed(2)}
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
