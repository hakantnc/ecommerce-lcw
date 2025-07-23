import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  // CORS sorunlarını çözmek için API isteklerini proxy'leme
  async rewrites() {
    return [
      {
        source: '/api/:path*',
        destination: 'http://localhost:5267/api/:path*',
      },
    ];
  },
  
  // CORS headers ekle
  async headers() {
    return [
      {
        source: '/api/:path*',
        headers: [
          { key: 'Access-Control-Allow-Origin', value: '*' },
          { key: 'Access-Control-Allow-Methods', value: 'GET,POST,PUT,DELETE,OPTIONS' },
          { key: 'Access-Control-Allow-Headers', value: 'Content-Type, Authorization' },
        ],
      },
    ];
  },
};

export default nextConfig;
