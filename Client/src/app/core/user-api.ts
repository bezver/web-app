import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, of, throwError } from 'rxjs';
import { Country, Genre, FilmFilter } from './api-models';
import { AppConfigService } from './config/app-config.service';

@Injectable({
    providedIn: 'root'
})
export class CountryClient {
    baseUrl: string;

    constructor(
        private http: HttpClient,
        private config: AppConfigService) {
            this.baseUrl = `${this.config.apiUrl}/country`;
        }

    list() {
        let url = this.baseUrl;
        const options: any = {
            observe: 'response'
        };

        return this.http.request('get', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body
                }
                return undefined;
            }), catchError(errorResponse => {
                return throwError(() => 'Cannot load countries');
            }));
    }

    create(country: Country) {
        let url = this.baseUrl;
        const options: any = {
            observe: 'response',
            body: country
        };

        return this.http.request('post', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body
                }
                return undefined;
            }), catchError(errorResponse => {
                return throwError(() => 'Cannot create country');
            }));
    }

    update(country: Country) {
        let url = this.baseUrl;
        const options: any = {
            observe: 'response',
            body: country
        };

        return this.http.request('put', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body
                }
                return undefined;
            }), catchError(errorResponse => {
                return throwError(() => 'Cannot update country');
            }));
    }

    delete(id: string) {
        let url = `${this.baseUrl}/${id}`;
        const options: any = {
            observe: 'response'
        };

        return this.http.request('delete', url, options)
            .pipe(
                map(() => undefined),
                catchError(() => {
                    return throwError(() => 'Cannot delete country');
                })
            );
    }
}

@Injectable({
    providedIn: 'root'
})
export class GenreClient {
    baseUrl: string;

    constructor(
        private http: HttpClient,
        private config: AppConfigService) {
            this.baseUrl = `${this.config.apiUrl}/genre`;
        }

    list() {
        let url = this.baseUrl;
        const options: any = {
            observe: 'response'
        };

        return this.http.request('get', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body
                }
                return undefined;
            }), catchError(errorResponse => {
                return throwError(() => 'Cannot load countries');
            }));
    }

    create(genre: Genre) {
        let url = this.baseUrl;
        const options: any = {
            observe: 'response',
            body: genre
        };

        return this.http.request('post', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body
                }
                return undefined;
            }), catchError(errorResponse => {
                return throwError(() => 'Cannot create genre');
            }));
    }

    update(genre: Genre) {
        let url = this.baseUrl;
        const options: any = {
            observe: 'response',
            body: genre
        };

        return this.http.request('put', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body
                }
                return undefined;
            }), catchError(errorResponse => {
                return throwError(() => 'Cannot update genre');
            }));
    }

    delete(id: string) {
        let url = `${this.baseUrl}/${id}`;
        const options: any = {
            observe: 'response'
        };

        return this.http.request('delete', url, options)
            .pipe(
                map(() => undefined),
                catchError(() => {
                    return throwError(() => 'Cannot delete genre');
                })
            );
    }
}


@Injectable({
    providedIn: 'root'
})
export class FilmClient {
    baseUrl: string;

    constructor(
        private http: HttpClient,
        private config: AppConfigService) {
            this.baseUrl = `${this.config.apiUrl}/film`;
        }

    listCount(filter: FilmFilter) {
        let url = `${this.baseUrl}/list-count`;
        if (filter.skip !== undefined && filter.take !== undefined) {
            url += `?filter.skip=${filter.skip}&filter.take=${filter.take}`;
        }
        const options: any = {
            observe: 'response'
        };

        return this.http.request('get', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return {
                        totalCount: response.body.totalCount,
                        films:  response.body.data
                    };
                }
                return undefined;
            }), catchError(errorResponse => {
                console.log(errorResponse);
                return throwError(() => 'Cannot load films');
            }));
    }

    list(filter: FilmFilter) {
        let url = this.baseUrl;
        if (filter.skip !== undefined && filter.take !== undefined) {
            url += `?filter.skip=${filter.skip}&filter.take=${filter.take}`;
        }
        const options: any = {
            observe: 'response'
        };

        return this.http.request('get', url, options)
            .pipe(map(response => {
                if (response instanceof HttpResponse && response.ok) {
                    return response.body;
                }
                return undefined;
            }), catchError(errorResponse => {
                console.log(errorResponse);
                return of('er');
            }));
    }

    delete(id: string) {
        let url = `${this.baseUrl}/${id}`;
        const options: any = {
            observe: 'response'
        };

        return this.http.request('delete', url, options)
            .pipe(
                map(() => undefined),
                catchError(() => {
                    return throwError(() => 'Cannot delete film');
                })
            );
    }
}
