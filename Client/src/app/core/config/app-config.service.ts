import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  title: string = environment.appTitle;
  authUrl = environment.authUrl;
  apiUrl = environment.apiUrl;
  pageSize = environment.pageSize;
}
