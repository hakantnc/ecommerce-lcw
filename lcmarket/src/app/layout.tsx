import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import Header from "@/components/Header";
import Navbar from "@/components/Navbar";
import Footer from "@/components/Footer";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "LCW Market - Online Alışveriş",
  description: "LCW Market ile her şey ayağınıza gelsin! Kaliteli ürünler, uygun fiyatlar.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="tr">
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        {/* Ana Header - Tüm sayfalarda görünecek */}
        <Header />
        
        {/* Kategoriler Navbar - Header'ın altında */}
        <Navbar />
        
        {/* Sayfa İçeriği */}
        <main className="min-h-screen">
          {children}
        </main>
        
        {/* Footer - Sayfanın altında */}
        <Footer />
      </body>
    </html>
  );
}
