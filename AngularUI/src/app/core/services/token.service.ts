import { UserModel } from 'src/app/core/models/user/userModel';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private jwtHelperService: JwtHelperService = new JwtHelperService();

  constructor(private localStorageService: LocalStorageService) {}

  decodeToken(token: string) {
    return this.jwtHelperService.decodeToken(token);
  }

  getToken(): string {
    return this.localStorageService.getItem('token');
  }

  setToken(token: string): void {
    this.localStorageService.setItem('token', token);
  }

  removeToken(): void {
    this.localStorageService.removeItem('token');
  }

  isTokenExpired(): boolean {
    let isExpired = this.jwtHelperService.isTokenExpired(this.getToken());

    return isExpired != null ? isExpired : true;
  }

  getTokenExpirationDate(): Date {
    return this.jwtHelperService.getTokenExpirationDate(this.getToken());
  }

  getUserRolesWithJWT(): string[] {
    let token = this.decodeToken(this.getToken());

    if (token != null) {
      let roles = token[Object.keys(token).filter((r) => r.endsWith('/role'))[0]];

      if (!Array.isArray(roles)) {
        let array = new Array();
        array.push(roles);

        return array;
      }

      return roles;
    }

    return [];
  }

  getUserWithJWT(): UserModel {
    let token = this.jwtHelperService.decodeToken(this.getToken());

    if (token != null) {
      let userModel = {
        id: +token[Object.keys(token).filter((t) => t.endsWith('nameidentifier'))[0]],
        email: token.email,
        firstName: token[Object.keys(token).filter((t) => t.endsWith('name'))[0]],
        lastName: token[Object.keys(token).filter((t) => t.endsWith('surname'))[0]],
        status: Boolean(token.status),
      };

      return userModel;
    }

    return null;
  }
}
