import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnDestroy } from '@angular/core';
import { map, Observable, shareReplay, Subject, delay } from 'rxjs';
import { AuthService } from './core/auth/auth.service';
import { AppConfigService } from './core/config/app-config.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy {
    isHandset$: Observable<boolean>;
    destroyed$ = new Subject<boolean>();

    constructor(
        private breakpointObserver: BreakpointObserver,
        public authService: AuthService,
        public config: AppConfigService) {
        this.isHandset$ = this.breakpointObserver.observe(Breakpoints.Handset)
            .pipe(
                map(result => result.matches),
                shareReplay()
            );
    }

    ngOnDestroy(): void {
        this.destroyed$.next(true);
        this.destroyed$.complete();
    }

    get isAuthenticated() {
        return !!this.authService.user;
    }

    get isAdmin() {
        return this.authService.user$.pipe(map(user => {
            window.dispatchEvent(new Event('resize'));
            return user?.roles?.includes('System Admin');
        }));
    }

    get isLoggingin() {
        return this.authService.loggingin$.pipe(delay(0));
    }
}
