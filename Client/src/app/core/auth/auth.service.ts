import { HttpClient, HttpHeaders, HttpResponse, JsonpClientBackend } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationEnd, Route, Router, RouterEvent } from '@angular/router';
import { BehaviorSubject, filter } from 'rxjs';
import { User } from '../api-models';
import { AppConfigService } from '../config/app-config.service';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private _user = new BehaviorSubject<User>(undefined);
    returnUrl: string | undefined = undefined;
    user: User | undefined;
    user$ = this._user.asObservable();
    loggingin$ = new BehaviorSubject(false);

    constructor(
        private router: Router,
        private http: HttpClient,
        private config: AppConfigService) {
        this.router.events
            .pipe(filter(e => e instanceof RouterEvent))
            .subscribe(e => {
                if (e instanceof NavigationEnd) {
                    this.loggingin$.next(e.url.includes('login') || e.url.includes('register'));
                }
            });
        this.user = this.getUser();
        this._user.next(this.user);

        this.router.events
            .subscribe(e => {
                // console.log(e);
                if (e instanceof NavigationEnd) {
                    // console.log(e);
                }
            });
    }

    isAuthenticated(): boolean {
        return !!this.user;
    }

    hasRole(role: string | undefined): boolean {
        if (!role) {
            return false;
        }
        return this.user?.roles?.includes(role);
    }

    register(user: User) {
        const options: any = {
            body: user,
            observe: 'response'
        };

        this.http.request('post', `${this.config.authUrl}/register`, options)
            .subscribe({
                next: response => {
                    if (response instanceof HttpResponse && response.ok) {
                        this.user = response.body;
                        this.saveUser(response.body);
                        this.loggingin$.next(false);
                        this.router.navigate([this.returnUrl || '/']);
                    }
                },
                error: e => {
                    console.log(e);
                }
            });
    }

    login(user: User) {
        const options: any = {
            body: user,
            observe: 'response'
        };

        this.http.request('post', this.config.authUrl, options)
            .subscribe({
                next: response => {
                    if (response instanceof HttpResponse && response.ok) {
                        this.user = response.body;
                        this._user.next(this.user);
                        this.saveUser(response.body);
                        this.loggingin$.next(false);
                        this.router.navigate([this.returnUrl || '/']);
                    }
                },
                error: e => {
                    console.log(e);
                }
            });
    }

    logout() {
        const options: any = {
            observe: 'response'
        };

        this.http.request('get', this.config.authUrl, options)
            .subscribe({
                next: response => {
                    if (response instanceof HttpResponse && response.ok) {
                        this.user = undefined;
                        this._user.next(this.user);
                        this.clearStorage();
                        this.router.navigate(['']);
                    }
                },
                error: e => {
                    if (e.status === 401) {
                        this.clearStorage();
                    } 
                }
            });
    }

    private getUser(): User | undefined {
        try {
            const userJson = sessionStorage.getItem('user');
            if (userJson && userJson !== '') {
                return JSON.parse(userJson);
            }
        }
        catch(e) {
            this.clearStorage();
        }
        return undefined;
    }

    private saveUser(user: User) {
        sessionStorage.setItem('user', JSON.stringify(user));
    }

    private clearStorage() {
        sessionStorage.removeItem('user');
    }
}
