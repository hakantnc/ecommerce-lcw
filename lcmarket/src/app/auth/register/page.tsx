'use client';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import Link from 'next/link';

const registerSchema = z
  .object({
    email: z
      .string()
      .min(1, 'Email zorunludur')
      .email('Geçerli bir email giriniz')
      .refine((val) => /^[^\s@]+@[^\s@]+\.[a-z]{2,}$/.test(val), {
        message: 'Geçerli bir alan adı giriniz (örn: .com, .org)',
      }),
    password: z
      .string()
      .min(8, 'Şifre en az 8 karakter olmalı')
      .max(150, 'Şifre 150 karakteri geçemez')
      .refine(
        (val) =>
          /[A-Z]/.test(val) &&
          /[a-z]/.test(val) &&
          /\d/.test(val) &&
          /[^A-Za-z0-9]/.test(val),
        'Şifre büyük/küçük harf, sayı ve özel karakter içermeli'
      ),
    firstName: z.string().optional(),
    lastName: z.string().optional(),
    supplierName: z.string().optional(),
    userType: z.enum(['customer', 'supplier']),
  })
  .superRefine((data, ctx) => {
    if (data.userType === 'customer') {
      if (!data.firstName) {
        ctx.addIssue({
          path: ['firstName'],
          message: 'Ad zorunludur',
          code: z.ZodIssueCode.custom,
        });
      }
      if (!data.lastName) {
        ctx.addIssue({
          path: ['lastName'],
          message: 'Soyad zorunludur',
          code: z.ZodIssueCode.custom,
        });
      }
    } else if (data.userType === 'supplier') {
      if (!data.supplierName) {
        ctx.addIssue({
          path: ['supplierName'],
          message: 'Tedarikçi adı zorunludur',
          code: z.ZodIssueCode.custom,
        });
      }
    }
  });

type FormValues = z.infer<typeof registerSchema>;

export default function RegisterPage() {
  const [userType, setUserType] = useState<'customer' | 'supplier'>('customer');
  const [error, setError] = useState('');
  const router = useRouter();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>({
    resolver: zodResolver(registerSchema),
    defaultValues: { userType },
  });

  const onSubmit = async (data: FormValues) => {
    setError('');

    const endpoint =
      data.userType === 'customer'
        ? 'http://localhost:5267/api/auth/customer/register'
        : 'http://localhost:5267/api/auth/supplier/register';

    const payload =
      data.userType === 'customer'
        ? {
            email: data.email,
            password: data.password,
            firstName: data.firstName,
            lastName: data.lastName,
          }
        : {
            email: data.email,
            password: data.password,
            supplierName: data.supplierName,
          };

    try {
      const res = await fetch(endpoint, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });

      if (!res.ok) throw new Error('Kayıt işlemi başarısız oldu');

      router.push('/auth/login');
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Bir hata oluştu');
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center relative">
      <div
        className="absolute inset-0 bg-cover bg-center bg-no-repeat"
        style={{
          backgroundImage: 'url(/auth-bg/auth-bg.jpg)',
          filter: 'brightness(0.3)',
        }}
      />

      <div className="max-w-md w-full space-y-8 bg-white/90 backdrop-blur-sm p-10 rounded-xl shadow-lg relative z-10 mx-4">
        <div className="text-center">
          <Link href="/" className="block">
            <h1 className="text-4xl font-black text-indigo-600 tracking-tight hover:text-blue-700 transition-colors duration-200">
              LCW MARKET
            </h1>
          </Link>
          <h2 className="mt-2 text-3xl font-extrabold text-gray-900">Yeni Hesap Oluşturun</h2>
          <p className="mt-2 text-sm text-gray-600">
            Veya{' '}
            <Link href="/auth/login" className="font-medium text-indigo-600 hover:text-indigo-500">
              mevcut hesabınıza giriş yapın
            </Link>
          </p>
        </div>

        <div className="flex justify-center space-x-4 mt-6">
          {(['customer', 'supplier'] as const).map((type) => (
            <button
              key={type}
              type="button"
              className={`px-6 py-3 rounded-lg font-medium transition-all duration-200 ${
                userType === type
                  ? 'bg-indigo-600 text-white shadow-md'
                  : 'bg-gray-100 text-gray-600 hover:bg-gray-200'
              }`}
              onClick={() => setUserType(type)}
            >
              {type === 'customer' ? 'Müşteri' : 'Tedarikçi'}
            </button>
          ))}
        </div>

        <form className="mt-8 space-y-6" onSubmit={handleSubmit(onSubmit)}>
          {error && <p className="text-red-500 text-sm font-medium">{error}</p>}

          <input type="hidden" value={userType} {...register('userType')} />

          <div className="space-y-4">
            <div>
              <label htmlFor="email" className="block text-sm font-medium text-gray-700">
                Email adresi
              </label>
              <input
                id="email"
                type="email"
                className="mt-1 block w-full px-3 py-2 border rounded-md shadow-sm"
                {...register('email')}
              />
              {errors.email && <p className="text-red-500 text-sm mt-1">{errors.email.message}</p>}
            </div>

            <div>
              <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                Şifre
              </label>
              <input
                id="password"
                type="password"
                className="mt-1 block w-full px-3 py-2 border rounded-md shadow-sm"
                {...register('password')}
              />
              {errors.password && <p className="text-red-500 text-sm mt-1">{errors.password.message}</p>}
            </div>

            {userType === 'customer' && (
              <>
                <div>
                  <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">
                    Ad
                  </label>
                  <input
                    id="firstName"
                    type="text"
                    className="mt-1 block w-full px-3 py-2 border rounded-md shadow-sm"
                    {...register('firstName')}
                  />
                  {errors.firstName && <p className="text-red-500 text-sm mt-1">{errors.firstName.message}</p>}
                </div>
                <div>
                  <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">
                    Soyad
                  </label>
                  <input
                    id="lastName"
                    type="text"
                    className="mt-1 block w-full px-3 py-2 border rounded-md shadow-sm"
                    {...register('lastName')}
                  />
                  {errors.lastName && <p className="text-red-500 text-sm mt-1">{errors.lastName.message}</p>}
                </div>
              </>
            )}

            {userType === 'supplier' && (
              <div>
                <label htmlFor="supplierName" className="block text-sm font-medium text-gray-700">
                  Tedarikçi Adı
                </label>
                <input
                  id="supplierName"
                  type="text"
                  className="mt-1 block w-full px-3 py-2 border rounded-md shadow-sm"
                  {...register('supplierName')}
                />
                {errors.supplierName && (
                  <p className="text-red-500 text-sm mt-1">{errors.supplierName.message}</p>
                )}
              </div>
            )}
          </div>

          <div>
            <button
              type="submit"
              className="w-full flex justify-center py-3 px-4 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
            >
              Kayıt Ol
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
