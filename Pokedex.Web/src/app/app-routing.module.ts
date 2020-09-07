import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PokedexComponent } from './pages/pokedex/pokedex.component';
import { FavoritesComponent } from './pages/favorites/favorites.component';
import { LoginComponent } from './pages/login/login.component';
import { AuthGuard } from 'src/security/AuthGuard';

const routes: Routes = [
  { path : '', component: PokedexComponent, canActivate: [AuthGuard] },
  { path : 'favorites', component : FavoritesComponent, canActivate: [AuthGuard]},
  { path : 'login', component : LoginComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
