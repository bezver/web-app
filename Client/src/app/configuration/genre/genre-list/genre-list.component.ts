import { Component, OnInit } from '@angular/core';
import CustomStore from 'devextreme/data/custom_store';
import { firstValueFrom } from 'rxjs';
import { AppConfigService } from 'src/app/core/config/app-config.service';
import { GenreClient } from 'src/app/core/user-api';

@Component({
    selector: 'app-genre-list',
    templateUrl: './genre-list.component.html',
    styleUrls: ['./genre-list.component.scss']
})
export class GenreListComponent implements OnInit {
    dataSource: CustomStore;

    constructor(
        private genreClient: GenreClient,
        public config: AppConfigService
        ) {
        this.dataSource = new CustomStore({
            key: 'id',
            load: () => {
                return firstValueFrom(this.genreClient.list())
                    .then(countries => {
                        return {
                            data: countries
                        };
                    });
            },
            insert: (genre) => {
                return firstValueFrom(this.genreClient.create(genre));
            },
            update: (key, values) => {
                return firstValueFrom(this.genreClient.update({ id: key, name: values.name }));
            },
            remove: (key) => {
                return firstValueFrom(this.genreClient.delete(key));
            }
        });
    }

    ngOnInit(): void {
    }

}
