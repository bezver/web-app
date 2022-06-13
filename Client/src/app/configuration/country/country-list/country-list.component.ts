import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { firstValueFrom } from 'rxjs';
import { AppConfigService } from 'src/app/core/config/app-config.service';
import { NotificationService } from 'src/app/core/notification/notification.service';
import { CountryClient } from 'src/app/core/user-api';

@Component({
    selector: 'app-country-list',
    templateUrl: './country-list.component.html',
    styleUrls: ['./country-list.component.scss']
})
export class CountryListComponent implements OnInit {
    dataSource: CustomStore;

    constructor(
        private notification: NotificationService,
        private countryClient: CountryClient,
        public config: AppConfigService
        ) {
        this.dataSource = new CustomStore({
            key: 'id',
            load: () => {
                return firstValueFrom(this.countryClient.list())
                    .then(countries => {
                        return {
                            data: countries
                        };
                    });
            },
            insert: (country) => {
                return firstValueFrom(this.countryClient.create(country))
                .then(() => {
                    this.notification.showSucces('Success');
                });
            },
            update: (key, values) => {
                return firstValueFrom(this.countryClient.update({ id: key, name: values.name }))
                .then(() => {
                    this.notification.showSucces('Success');
                });
            },
            remove: (key) => {
                return firstValueFrom(this.countryClient.delete(key))
                    .then(() => {
                        this.notification.showSucces('Success');
                    });
            }
        });
    }

    ngOnInit(): void {
    }

}
