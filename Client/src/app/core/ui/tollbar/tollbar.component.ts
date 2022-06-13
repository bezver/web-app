import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { map, Observable, shareReplay, Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../auth/auth.service';
import { AppConfigService } from '../../config/app-config.service';

@Component({
    selector: 'app-tollbar',
    templateUrl: './tollbar.component.html',
    styleUrls: ['./tollbar.component.scss']
})
export class TollbarComponent implements OnInit {
    @Input() drawer: any;

    isLoggingin = false;

    destroyed$ = new Subject<boolean>();
    isHandset$: Observable<boolean>;

    constructor(
        private breakpointObserver: BreakpointObserver,
        private router: Router,
        public authService: AuthService,
        public config: AppConfigService,
        private cdr: ChangeDetectorRef) {
        this.isHandset$ = this.breakpointObserver.observe(Breakpoints.Handset)
            .pipe(
                map(result => result.matches),
                shareReplay()
            );
        this.authService.loggingin$.pipe(takeUntil(this.destroyed$))
            .subscribe(loggingin => {
                this.isLoggingin = loggingin;
            });
    }

    ngOnInit(): void {
    }

    get isAuthenticated() {
        return !!this.authService.user;
    }

    login() {
        this.authService.returnUrl = this.router.url;
        this.router.navigate(['login']);
    }

    logout() {
        this.authService.logout();
    }

    goHome() {
        this.router.navigate(['/']);
    }
}
