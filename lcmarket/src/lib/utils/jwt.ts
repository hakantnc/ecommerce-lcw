import { jwtDecode } from 'jwt-decode';

export interface DecodedToken {
  email: string;
  role: 'Customer' | 'Supplier';
  given_name: string;
  surname: string;
  exp: number;
}

export function parseJwt(token: string): DecodedToken | null {
  try {
    const decoded: any = jwtDecode(token);

    return {
      email: decoded.email,
      role:
        decoded.role ||
        decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
      given_name:
        decoded.given_name ||
        decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname'],
      surname:
        decoded.surname ||
        decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname'],
      exp: decoded.exp,
    };
  } catch (err) {
    console.error("parseJwt error", err);
    return null;
  }
}
