// API Servis Katmanı - Mevcut .NET Core backend'imize bağlanmak için
// Bu dosya HTTP isteklerini yönetir ve API endpoint'lerimizi tanımlar

const API_BASE_URL = '/api'; // Next.js proxy üzerinden .NET Core API'mize erişim

// HTTP isteklerini yapan temel fonksiyon
async function apiRequest(endpoint: string, options: RequestInit = {}) {
  const url = `${API_BASE_URL}${endpoint}`;
  
  const config: RequestInit = {
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
    ...options,
  };

  try {
    const response = await fetch(url, config);
    
    if (!response.ok) {
      throw new Error(`API Error: ${response.status} ${response.statusText}`);
    }
    
    // Boş response kontrolü (DELETE işlemleri için)
    const text = await response.text();
    if (!text.trim()) {
      return null;
    }
    
    return JSON.parse(text);
  } catch (error) {
    console.error('API Request failed:', error);
    throw error;
  }
}

// API servis fonksiyonları - Her entity için CRUD operasyonları

// Kategori veri dönüştürme fonksiyonları
const mapCategoryFromAPI = (apiCategory: any) => ({
  id: apiCategory.category_id,
  name: apiCategory.category_name,
  description: apiCategory.category_description
});

const mapCategoryToAPI = (category: any) => ({
  category_name: category.name,
  category_description: category.description
});

// Kategori İşlemleri
export const categoryAPI = {
  // Tüm kategorileri getir
  getAll: async () => {
    const data = await apiRequest('/category');
    return Array.isArray(data) ? data.map(mapCategoryFromAPI) : [];
  },
  
  // Tek kategori getir
  getById: async (id: number) => {
    const data = await apiRequest(`/category/${id}`);
    return mapCategoryFromAPI(data);
  },
  
  // Yeni kategori oluştur
  create: (category: any) => apiRequest('/category', {
    method: 'POST',
    body: JSON.stringify(mapCategoryToAPI(category))
  }),
  
  // Kategori güncelle
  update: (id: number, category: any) => apiRequest(`/category/${id}`, {
    method: 'PUT',
    body: JSON.stringify(mapCategoryToAPI(category))
  }),
  
  // Kategori sil
  delete: (id: number) => apiRequest(`/category/${id}`, {
    method: 'DELETE'
  })
};

// Ürün veri dönüştürme fonksiyonları
const mapProductFromAPI = (apiProduct: any) => ({
  id: apiProduct.Id || apiProduct.id,
  name: apiProduct.Name || apiProduct.name,
  description: apiProduct.Description || apiProduct.description,
  price: apiProduct.Price || apiProduct.price,
  stock: apiProduct.Stock || apiProduct.stock,
  imageUrl: apiProduct.ImageUrl || apiProduct.imageUrl,
  categoryId: apiProduct.category_id || apiProduct.categoryId,
  supplierId: apiProduct.supplier_id || apiProduct.supplierId,
  isActive: apiProduct.IsActive !== undefined ? apiProduct.IsActive : apiProduct.isActive,
  subcategoryId: apiProduct.SubcategoryId,
  subcategoryName: apiProduct.SubcategoryName
});

const mapProductToAPI = (product: any) => {
  console.log(' mapProductToAPI input:', product);
  
  const mapped: any = {
    Name: product.name || '',
    Description: product.description || '',
    Price: parseFloat(product.price) || 0,
    Stock: parseInt(product.stock) || 0,
    ImageUrl: product.imageUrl || '',
    IsActive: product.isActive !== false,
    category_id: product.categoryId ? parseInt(product.categoryId) : null,
    supplier_id: product.supplierId ? parseInt(product.supplierId) : null,
    SubcategoryId: product.subcategoryId ? parseInt(product.subcategoryId) : null,
    SubcategoryName: product.subcategoryName || null
  };
  
  // UPDATE işlemi için ID eklenir
  if (product.id) {
    mapped.Id = product.id;
  }
  
  console.log('mapProductToAPI output:', mapped);
  return mapped;
};
// Ürün İşlemleri
export const productAPI = {
  getAll: async () => {
    const data = await apiRequest('/product');
    return Array.isArray(data) ? data.map(mapProductFromAPI) : [];
  },
  getById: async (id: number) => {
    const data = await apiRequest(`/product/${id}`);
    return mapProductFromAPI(data);
  },
  create: async (product: any) => {
    const data = await apiRequest('/product', {
      method: 'POST',
      body: JSON.stringify(mapProductToAPI(product))
    });
    return data ? mapProductFromAPI(data) : null;
  },
  update: async (id: number, product: any) => {
    const productWithId = { ...product, id };
    const data = await apiRequest(`/product/${id}`, {
      method: 'PUT',
      body: JSON.stringify(mapProductToAPI(productWithId))
    });
    return data ? mapProductFromAPI(data) : null;
  },
  delete: async (id: number) => {
    return await apiRequest(`/product/${id}`, {
      method: 'DELETE'
    });
  },
  search: async (query: string) => {
    const data = await apiRequest(`/product/search?query=${encodeURIComponent(query)}`);
    return Array.isArray(data) ? data.map(mapProductFromAPI) : [];
  }
};

// Müşteri İşlemleri
export const customerAPI = {
  getAll: () => apiRequest('/customer'),
  getById: (id: number) => apiRequest(`/customer/${id}`),
  create: (customer: any) => apiRequest('/customer', {
    method: 'POST',
    body: JSON.stringify(customer)
  }),
  update: (id: number, customer: any) => apiRequest(`/customer/${id}`, {
    method: 'PUT',
    body: JSON.stringify(customer)
  }),
  delete: (id: number) => apiRequest(`/customer/${id}`, {
    method: 'DELETE'
  })
};

// Sipariş İşlemleri
export const orderAPI = {
  getAll: () => apiRequest('/order'),
  getById: (id: number) => apiRequest(`/order/${id}`),
  update: (id: number, order: any) => apiRequest(`/order/${id}`, {
    method: 'PUT',
    body: JSON.stringify(order)
  }),
  delete: (id: number) => apiRequest(`/order/${id}`, {
    method: 'DELETE'
  })
};

// Tedarikçi İşlemleri
export const supplierAPI = {
  getAll: () => apiRequest('/supplier'),
  getById: (id: number) => apiRequest(`/supplier/${id}`),
  create: (supplier: any) => apiRequest('/supplier', {
    method: 'POST',
    body: JSON.stringify(supplier)
  }),
  update: (id: number, supplier: any) => apiRequest(`/supplier/${id}`, {
    method: 'PUT',
    body: JSON.stringify(supplier)
  }),
  delete: (id: number) => apiRequest(`/supplier/${id}`, {
    method: 'DELETE'
  })
}; 

// Subcategory işlemleri
export const subcategoryAPI = {
  // Tüm alt kategorileri getir
  getAll: async () => {
    const data = await apiRequest('/subcategory');
    return Array.isArray(data) ? data.map((s: any) => ({
      id: s.subcategoryId,
      name: s.subcategoryName,
      categoryId: s.category_id
    })) : [];
  },

  // ID'ye göre alt kategori getir
  getById: async (id: number) => {
    const data = await apiRequest(`/subcategory/${id}`);
    return data ? {
      id: data.subcategoryId,
      name: data.subcategoryName,
      categoryId: data.category_id
    } : null;
  },

  // Kategoriye göre alt kategorileri getir
  getByCategory: async (categoryId: number) => {
    const data = await apiRequest(`/subcategory/by-category/${categoryId}`);
    return Array.isArray(data) ? data.map((s: any) => ({
      id: s.subcategoryId,
      name: s.subcategoryName,
      categoryId: s.category_id
    })) : [];
  },
  
  // Yeni alt kategori oluştur
  create: async (subcategory: { name: string, categoryId: number }) => {
    const data = await apiRequest('/subcategory', {
      method: 'POST',
      body: JSON.stringify({
        subcategoryName: subcategory.name,
        category_id: subcategory.categoryId
      })
    });
    return data ? {
      id: data.subcategoryId,
      name: data.subcategoryName,
      categoryId: data.category_id
    } : null;
  },

  // Alt kategori güncelle
  update: async (id: number, subcategory: { name: string, categoryId: number }) => {
    const data = await apiRequest(`/subcategory/${id}`, {
      method: 'PUT',
      body: JSON.stringify({
        subcategoryName: subcategory.name,
        category_id: subcategory.categoryId
      })
    });
    return data;
  },

  // Alt kategori sil
  delete: (id: number) => apiRequest(`/subcategory/${id}`, {
    method: 'DELETE'
  })
}; 

const API_URL = 'http://localhost:5267/api';

export const api = {
  auth: {
    login: async (email: string, password: string) => {
      const res = await fetch(`${API_URL}/auth/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email, password }),
      });
      
      if (!res.ok) {
        throw new Error('Invalid email or password');
      }
      
      return res.ok;
    },

    registerCustomer: async (data: {
      email: string;
      password: string;
      firstName: string;
      lastName: string;
    }) => {
      const res = await fetch(`${API_URL}/auth/customer/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (!res.ok) {
        throw new Error('Registration failed');
      }

      return res.json();
    },

    registerSupplier: async (data: {
      email: string;
      password: string;
      supplierName: string;
    }) => {
      const res = await fetch(`${API_URL}/auth/supplier/register`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (!res.ok) {
        throw new Error('Registration failed');
      }

      return res.json();
    },
  },
}; 