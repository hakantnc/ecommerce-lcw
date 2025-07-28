# LC Market E-Ticaret Projesi

Bu proje, modern bir e-ticaret platformu oluşturmak için .NET 8 ve Next.js teknolojilerini kullanan full-stack bir web uygulamasıdır.

## Proje Yapısı

Proje iki ana bölümden oluşmaktadır:

### Backend (.NET 8)
- **EcommerceAPI**: Ana API projesi
- **Entities**: Veritabanı modelleri ve DTO'lar
- **Repositories**: Veritabanı işlemleri
- **Services**: İş mantığı katmanı
- **Presentation**: Controller'lar ve API endpoints

### Frontend (Next.js)
- **lcmarket**: Next.js tabanlı modern web arayüzü

## Teknolojiler

### Backend
- .NET 8
- Entity Framework Core
- AutoMapper
- JWT Authentication
- SQL Server

### Frontend
- Next.js
- TypeScript
- Tailwind CSS

## Kurulum

### Backend için Gereksinimler
1. .NET 8 SDK
2. SQL Server
3. Visual Studio 2022 veya Visual Studio Code

```bash
# Backend projesini başlatmak için
cd EcommerceAPI
dotnet restore
dotnet run
```

### Frontend için Gereksinimler
1. Node.js (18.x veya üzeri)
2. npm veya yarn

```bash
# Frontend projesini başlatmak için
cd lcmarket
npm install
npm run dev
```

## Özellikler

- 🛍️ Ürün listeleme ve detay görüntüleme
- 🛒 Alışveriş sepeti yönetimi
- 👤 Kullanıcı hesap yönetimi
- 💳 Ödeme sistemi entegrasyonu
- 📱 Responsive tasarım
- 🔐 Güvenli kimlik doğrulama
- 📦 Kategori yönetimi
- 🏪 Tedarikçi paneli

## Katkıda Bulunma

1. Bu repository'yi fork edin
2. Feature branch'i oluşturun (`git checkout -b feature/AmazingFeature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add some AmazingFeature'`)
4. Branch'inizi push edin (`git push origin feature/AmazingFeature`)
5. Pull Request oluşturun

## Lisans

Bu proje MIT lisansı altında lisanslanmıştır. 