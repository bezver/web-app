import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { firstValueFrom } from 'rxjs';
import { Film } from 'src/app/core/api-models';
import { AppConfigService } from 'src/app/core/config/app-config.service';
import { NotificationService } from 'src/app/core/notification/notification.service';
import { CountryClient, FilmClient } from 'src/app/core/user-api';

@Component({
    selector: 'app-film-list',
    templateUrl: './film-list.component.html',
    styleUrls: ['./film-list.component.scss']
})
export class FilmListComponent implements OnInit {
    dataSource: CustomStore;
    currentID: string;
    isPopupVisible = false;

    constructor(
        private filmClient: FilmClient,
        private notification: NotificationService,
        private countryClient: CountryClient,
        public config: AppConfigService
        ) {
        this.dataSource = new CustomStore({
            key: 'id',
            load: loadOptions => {
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
                    });
            },
            insert: (country) => {
                return firstValueFrom(this.countryClient.create(country));
            },
            update: (key, values) => {
                return firstValueFrom(this.countryClient.update({ id: key, name: values.name }));
            },
            remove: (key) => {
                return firstValueFrom(this.countryClient.delete(key));
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

    onClick(): void {
        this.currentID = '';
        this.isPopupVisible = true;
        this.notification.showSucces('success');
      }

      onEditClick(id: string): void {
        this.currentID = id;
        this.isPopupVisible = true;
      }

      async onDeleteClick(id: string) {
        const result = await this.notification.showConfirmation('Are you sure you want to delete this record?',  'Confirm your action');
        if (result) {
            this.filmClient.delete(id).subscribe();
        }
      }

}
