import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { CommonService } from 'src/providers/common.service';
import { ConfigService } from 'src/providers/config.service';
import { PokedexService } from 'src/providers/pokedex.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpInterceptorModule } from 'src/providers/http-interceptor';
import { LoaderComponent } from './components/loader/loader.component';
import { PokedexComponent } from './pages/pokedex/pokedex.component';
import { PokemonComponent } from './pages/pokemon/pokemon.component';
import { PokedexListComponent } from './pages/pokedex-list/pokedex-list.component';
import { FavoritesComponent } from './pages/favorites/favorites.component';
import { FavoritesService } from 'src/providers/favorites.service';
import { AppRoutingModule } from './app-routing.module';
import { SessionStorageService } from 'src/providers/sessionstorage.service';
import { SecurityService } from 'src/providers/security.service';
import { LoginComponent } from './pages/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    PokedexListComponent,
    LoaderComponent,
    PokedexComponent,
    PokemonComponent,
    FavoritesComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    HttpInterceptorModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    CommonService,
    ConfigService,
    PokedexService,
    FavoritesService,
    SessionStorageService,
    SecurityService
  ],
  exports:[
    LoaderComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
