import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IpAddressService {
  private apiUrl = 'https://httpbin.org/ip'; // Replace with your chosen IP address API

  constructor(private http: HttpClient) {}

  getIpAddress(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}