import { Injectable } from '@angular/core';
import { confirm } from 'devextreme/ui/dialog';
import notify from 'devextreme/ui/notify';
import { of } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    constructor() { }

    showSucces(message: string) {
        notify(
            {
                message: message,
                width: 230,
                position: {
                    at: "bottom",
                    my: "bottom"
                }
            },
            'success',
            500
        );
    }

    showError(message: string) {
        notify(
            {
                message: message,
                width: 230,
                position: {
                    at: "bottom",
                    my: "bottom"
                }
            },
            'error',
            1500
        );
    }

    showConfirmation(message: string, title: string) {
        return confirm(message,  title);
    }
}
