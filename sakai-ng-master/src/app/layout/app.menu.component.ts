import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';

@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];

    constructor(public layoutService: LayoutService) { }

    ngOnInit() {
        // this.model = [
        //     {
        //         label: 'Home',
        //         items: [
        //             { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/superadmin/dashboard'] },
        //             { label: 'Laboratory', icon: 'pi pi-fw pi-id-card', routerLink: ['/superadmin/laboratory'] },
        //             { label: 'User', icon: 'pi pi-fw pi-check-square', routerLink: ['/superadmin/user'] }
        //         ]
        //     }
        // ];

        this.model = [
            {
                label: 'Home',
                items: [
                    { label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/lab/dashboard'] },
                    { label: 'Login', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/auth/login'] },
                    { label: 'Registeration', icon: 'pi pi-fw pi-check-square', routerLink: ['/auth/registration'] },
                ]
            }
        ];
    }
}
