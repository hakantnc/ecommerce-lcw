# LC Market - E-ticaret Yönetim Sistemi

LC Market, mevcut .NET Core backend sisteminizle entegre çalışacak şekilde tasarlanmış modern bir Next.js frontend uygulamasıdır. Bu proje, e-ticaret operasyonlarınızı yönetmek için basit ve kullanıcı dostu bir admin panel sağlar.

## 🚀 Özellikler

- **Modern Admin Panel**: Tailwind CSS ile tasarlanmış responsive ve kullanıcı dostu arayüz
- **.NET Core API Entegrasyonu**: Mevcut localhost:5267 API'nize tam entegrasyon
- **CRUD İşlemleri**: Kategoriler, ürünler, müşteriler ve siparişler için tam CRUD desteği
- **TypeScript**: Type-safe development için tam TypeScript desteği
- **Responsive Design**: Mobil ve masaüstü cihazlarda mükemmel görünüm

## 📋 Gereksinimler

### Frontend (Bu Proje)
- Node.js 18+ 
- Next.js 14
- TypeScript
- Tailwind CSS

### Backend (Mevcut Sisteminiz)
- .NET Core API
- localhost:5267 portunda çalışan API
- Entity Framework
- SQL Server Database

## 🛠️ Kurulum

### 1. Projeyi Klonlayın veya İndirin
```bash
cd lcmarket
```

### 2. Bağımlılıkları Yükleyin
```bash
npm install
```

### 3. Backend API'nizi Çalıştırın
Öncelikle mevcut .NET Core backend sisteminizin localhost:5267 portunda çalıştığından emin olun.

### 4. Frontend'i Başlatın
```bash
npm run dev
```

Uygulama http://localhost:3000 adresinde çalışmaya başlayacaktır.

## 📁 Proje Yapısı

```
lcmarket/
├── src/
│   ├── app/                    # Next.js App Router
│   │   ├── admin/             # Admin panel sayfaları
│   │   │   ├── layout.tsx     # Admin layout
│   │   │   ├── page.tsx       # Dashboard
│   │   │   └── categories/    # Kategori yönetimi
│   │   ├── globals.css        # Global stiller
│   │   ├── layout.tsx         # Ana layout
│   │   └── page.tsx           # Ana sayfa
│   └── lib/
│       └── api.ts             # API servisleri
├── package.json
├── tailwind.config.ts
└── tsconfig.json
```

## 🔧 API Yapılandırması

### API Base URL
API base URL `src/lib/api.ts` dosyasında tanımlanmıştır:

```typescript
const API_BASE_URL = 'http://localhost:5267/api';
```

### Desteklenen Endpoint'ler

Sistem aşağıdaki API endpoint'lerini beklemektedir:

#### Kategoriler
- `GET /api/category` - Tüm kategorileri listele
- `GET /api/category/{id}` - Tek kategori getir
- `POST /api/category` - Yeni kategori oluştur
- `PUT /api/category/{id}` - Kategori güncelle
- `DELETE /api/category/{id}` - Kategori sil

#### Ürünler
- `GET /api/product` - Tüm ürünleri listele
- `GET /api/product/{id}` - Tek ürün getir
- `POST /api/product` - Yeni ürün oluştur
- `PUT /api/product/{id}` - Ürün güncelle
- `DELETE /api/product/{id}` - Ürün sil

#### Müşteriler
- `GET /api/customer` - Tüm müşterileri listele
- `GET /api/customer/{id}` - Tek müşteri getir
- `POST /api/customer` - Yeni müşteri oluştur
- `PUT /api/customer/{id}` - Müşteri güncelle
- `DELETE /api/customer/{id}` - Müşteri sil

#### Siparişler
- `GET /api/order` - Tüm siparişleri listele
- `GET /api/order/{id}` - Tek sipariş getir
- `PUT /api/order/{id}` - Sipariş güncelle
- `DELETE /api/order/{id}` - Sipariş sil

#### Tedarikçiler
- `GET /api/supplier` - Tüm tedarikçileri listele
- `GET /api/supplier/{id}` - Tek tedarikçi getir
- `POST /api/supplier` - Yeni tedarikçi oluştur
- `PUT /api/supplier/{id}` - Tedarikçi güncelle
- `DELETE /api/supplier/{id}` - Tedarikçi sil

## 🎯 Kullanım

### Admin Paneline Erişim
1. Uygulamayı başlattıktan sonra http://localhost:3000 adresine gidin
2. "Admin Paneline Git" butonuna tıklayın
3. Admin panel http://localhost:3000/admin adresinde açılacaktır

### Kategori Yönetimi
1. Sol menüden "Kategoriler" seçin
2. "Yeni Kategori Ekle" butonuyla kategori ekleyebilirsiniz
3. Listeden kategorileri düzenleyebilir veya silebilirsiniz

### Diğer Modüller
- Ürünler, müşteriler, siparişler ve tedarikçiler için benzer işlemler yapılabilir
- Her modül için aynı CRUD (Create, Read, Update, Delete) işlemleri mevcuttur

## 🔧 Özelleştirme

### API URL Değiştirme
Eğer backend API'niz farklı bir portta çalışıyorsa, `src/lib/api.ts` dosyasındaki `API_BASE_URL` değişkenini güncelleyin:

```typescript
const API_BASE_URL = 'http://localhost:SIZIN_PORT/api';
```

### Stil Özelleştirme
- Renkler ve temalar `tailwind.config.ts` dosyasından özelleştirilebilir
- Component stilleri her dosyada Tailwind CSS sınıfları ile düzenlenebilir

## 📝 Komutlar

```bash
# Geliştirme sunucusunu başlat
npm run dev

# Production build oluştur
npm run build

# Production sunucusunu başlat
npm start

# Linting
npm run lint
```

## ⚠️ Önemli Notlar

1. **API Bağlantısı**: Bu frontend uygulaması localhost:5267 adresindeki .NET Core API'nizi bekler. Admin panelini kullanmadan önce backend API'nizin çalıştığından emin olun.

2. **CORS Ayarları**: Backend API'nizde CORS ayarlarının frontend için uygun şekilde yapılandırıldığından emin olun. .NET Core API'nizin `Program.cs` veya `Startup.cs` dosyasında şu ayarların olması gerekiyor:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// ...

app.UseCors();
```

3. **Database**: Herhangi bir mock data kullanılmamıştır. Tüm veriler mevcut database'inizden çekilir.

4. **Hata Yönetimi**: API bağlantı hataları kullanıcı dostu mesajlarla gösterilir.

## 🤝 Katkıda Bulunma

1. Projeyi fork edin
2. Feature branch oluşturun (`git checkout -b feature/yeni-ozellik`)
3. Değişikliklerinizi commit edin (`git commit -am 'Yeni özellik eklendi'`)
4. Branch'inizi push edin (`git push origin feature/yeni-ozellik`)
5. Pull Request oluşturun

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.

## 📞 Destek

Herhangi bir sorun yaşarsanız:
1. GitHub Issues bölümünde sorun bildirin
2. Proje dokümantasyonunu kontrol edin
3. API bağlantınızı test edin

---

**Not**: Bu proje basit ve anlaşılır olması için tasarlanmıştır. Karmaşık authentication, authorization veya advanced özellikler içermez. İhtiyaçlarınıza göre özelleştirilebilir.
