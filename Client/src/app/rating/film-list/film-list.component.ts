import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ControlContainer } from '@angular/forms';
import CustomStore from 'devextreme/data/custom_store';
import { firstValueFrom } from 'rxjs';
import { Film } from 'src/app/core/api-models';
import { AuthService } from 'src/app/core/auth/auth.service';
import { AppConfigService } from 'src/app/core/config/app-config.service';
import { FilmClient } from 'src/app/core/user-api';

@Component({
    selector: 'app-film-list',
    templateUrl: './film-list.component.html',
    styleUrls: ['./film-list.component.scss']
})
export class FilmListComponent implements OnInit {
    dataSource: CustomStore;
    films : Film[];
    loading = true;

    constructor(
        public authService: AuthService,
        private filmClient: FilmClient,
        public config: AppConfigService) {
            const isNotEmpty = (value: any) => value !== undefined && value !== null && value !== '';
 
        this.dataSource = new CustomStore({
            key: 'id',
            load: loadOptions => {
                console.log(loadOptions);
                const filter = {
                    skip: loadOptions.skip,
                    take: loadOptions.take
                };
                return firstValueFrom(this.filmClient.listCount(filter))
                    .then(container => {
                        return {
                            data: container?.films,
                            totalCount: container?.totalCount,
                        };
                    })
                    .catch(() => { throw 'Data loading error' });

                // let params: HttpParams = new HttpParams();
                // [
                //     'filter',
                //     'group', 
                //     'groupSummary',
                //     'parentIds',
                //     'requireGroupCount',
                //     'requireTotalCount',
                //     'searchExpr',
                //     'searchOperation',
                //     'searchValue',
                //     'select',
                //     'sort',
                //     'skip',     
                //     'take',
                //     'totalSummary', 
                //     'userData'
                // ].forEach(i => {
                //     if(i in loadOptions && isNotEmpty(loadOptions[i])) {
                //         params[i] = JSON.stringify(loadOptions[i]);
                //     }
                // });
 
                // return this.http.get('https://mydomain.com/MyDataService', { params: params })
                //     .toPromise()
                //     .then(response => {
                //         return {
                //             data: response.data,
                //             totalCount: response.totalCount,
                //             summary: response.summary,
                //             groupCount: response.groupCount
                //         };
                //     })
                //     .catch(() => { throw 'Data loading error' });
            },
            update: (key, values) => {
                console.log(key, values);
                return Promise.resolve();
                // return firstValueFrom(this.genreClient.update({ id: key, name: values.name }));
            }
        });
    }




    ngOnInit(): void {
    }

    calculateCountries(film: Film) {
        if (film.countries?.length > 0) {
            return film.countries
                .map(country => country.name)
                .reduce((prev, curr) => {
                    return `${prev}, ${curr}`
                });
        }
        return '-';
    }

    calculateGenres(film: Film) {
        if (film.genres?.length > 0) {
            return film.genres
                .map(genre => genre.name)
                .reduce((prev, curr) => {
                    return `${prev}, ${curr}`
                });
        }
        return '-';
    }

}
