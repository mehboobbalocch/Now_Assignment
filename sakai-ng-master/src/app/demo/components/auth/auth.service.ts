import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map, of } from 'rxjs';
import { IpAddressService } from '../../service/ip-address.service';
import { BrowserDetectService } from '../../service/browser-detect.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrlReg = 'https://localhost:7169/api/users/signup';
  private baseUrlbalance = 'https://localhost:7169/api/users/balance';
  
  private readonly baseUrl = 'https://localhost:7169/api/users/';
  isLogin = false;
  roleAs: string;
  ipAddress : string = "119.73.124.136"
  private _token: BehaviorSubject<any>;

  constructor(private http: HttpClient, private ipService: IpAddressService, private browserDetectService: BrowserDetectService) {
    this._token = <BehaviorSubject<any>>new BehaviorSubject(sessionStorage.getItem('auth_token'));
   }

   fetchIpAddress() {
    this.ipService.getIpAddress().subscribe(
      (data) => {
        this.ipAddress = data.origin;
      },
      (error) => {
        console.error('Error fetching IP address:', error);
      }
    );
  }

  userlogin(_username: string, _password: string): Observable<boolean> {
    this.fetchIpAddress()
    const credentials = {
      userName: _username,
      password: _password,
      ipAddress: this.ipAddress,
      browser: this.browserDetectService.getBrowserName()
    };
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(`${this.baseUrl}authenticate`, credentials, { headers }).pipe(map((response: any) => {
      if (response && response.token) {
        const token = response.token;
        if(token){  
            this.setToken(token);
            this.login('SuperAdmin')
            return true;
          } else {
            return false;
          }
        }
        return false;
      }));
    }

    setToken(token: string) {
      this._token.next(token);
      sessionStorage.setItem('auth_token', token);
    }
  
  login(value: string) {
    this.isLogin = true;
    this.roleAs = value;
    localStorage.setItem('STATE', 'true');
    localStorage.setItem('ROLE', this.roleAs);
    return of({ success: this.isLogin, role: this.roleAs });
  }

  logout() {
    this.isLogin = false;
    this.roleAs = '';
    localStorage.setItem('STATE', 'false');
    localStorage.setItem('ROLE', '');
    return of({ success: this.isLogin, role: '' });
  }

  isLoggedIn() {
    const loggedIn = localStorage.getItem('STATE');
    if (loggedIn == 'true')
      this.isLogin = true;
    else
      this.isLogin = false;
    return this.isLogin;
  }

  balance(): Observable<any> {

    const token = sessionStorage.getItem('auth_token');
    const headers = token ? new HttpHeaders({
      'Authorization': `Bearer ${token}`
    }) : new HttpHeaders();

    return this.http.get<any>(`${this.baseUrl}balance/`, { headers });
  }
  
  reglogin(data: any): Observable<boolean> {
    this.fetchIpAddress();

    const credentials = {
      Email: data.email,
      FirstName: data.firstname,
      LastName: data.lastname,
      Password: data.password,
      Roles: [data.role],
      IpAddress: this.ipAddress,
      Browser: this.browserDetectService.getBrowserName()
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });


    return this.http.post<boolean>(`${this.baseUrl}signup`, credentials, { headers });
  }


  getRole() {
    this.roleAs = localStorage.getItem('ROLE');
    return this.roleAs;
  }

}
