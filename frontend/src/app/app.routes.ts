import { Routes } from '@angular/router';
import { CadastroComponent } from './features/cadastro/cadastro.component';
import { LoginComponent } from './features/login/login.component';
import { AreaRestritaComponent } from './features/area-restrita/area-restrita.component';
import { AdminComponent } from './features/admin/admin.component';
import { adminGuard, authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'cadastro',
    pathMatch: 'full'
  },
  {
    path: 'cadastro',
    component: CadastroComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'area-restrita',
    component: AreaRestritaComponent,
    canActivate: [authGuard]
  },
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [adminGuard]
  },
  {
    path: '**',
    redirectTo: 'cadastro'
  }
];