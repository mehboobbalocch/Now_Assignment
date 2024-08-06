import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AppLayoutComponent } from "./layout/app.layout.component";
import { AuthGuard } from './demo/components/auth/auth.guard';

@NgModule({
    imports: [
        RouterModule.forRoot([
            { path: '', redirectTo: 'auth/registration', pathMatch: 'full' },
            {
                
                path: '', component: AppLayoutComponent,
                children: [
                    {
                        path: 'superadmin',
                        canActivate: [AuthGuard],
                        data: { role: 'SuperAdmin' },
                        loadChildren: () => import('./superadmin/super-admin.module').then(m => m.SuperAdminModule)
                    }
                ]
            },
            { path: 'auth', loadChildren: () => import('./demo/components/auth/auth.module').then(m => m.AuthModule) },
            { path: '**', redirectTo: '/notfound' },
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
