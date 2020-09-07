import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Pokemon } from 'src/Interfaces/IPokemon';
import { PokedexService } from 'src/providers/pokedex.service';
import { CommonService } from 'src/providers/common.service';
import { FavoritesService } from 'src/providers/favorites.service';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-pokedex-list',
  templateUrl: './pokedex-list.component.html',
  styleUrls: ['./pokedex-list.component.css']
})
export class PokedexListComponent {

  @Input() isFavorite = false;
  @Input() pages: number[];
  @Input() pokemons: Pokemon[];
  @Output() ChangePage = new EventEmitter<number>();

  pokemon: Pokemon;
  currentPage = 1;
  isFavoritePage = false;

  constructor(
    private PokedexSrv: PokedexService,
    private favSrv: FavoritesService,
    private commonSrv: CommonService,
    private router: Router
  ) {
    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        this.isFavoritePage = val.url === '/favorites';
      }

    });
  }
  onChangePage(page: number) {
    this.currentPage = page;
    this.ChangePage.emit(page);
  }

  showDetail(id: number) {
    this.PokedexSrv.findById(id).subscribe(
      res => {
        res.sprite = this.commonSrv.sanitizeBase64Image(res.sprite);
        this.pokemon = res;
      },
      error => console.log(error)
    );
  }

  favoriteOpt(id: number, isfavorite: boolean) {
    this.favSrv.favorite(1, id, isfavorite).subscribe(
      res => {

        const pokee = this.pokemons.filter(s => s.id === id)[0];

        if (this.isFavoritePage) {

          const index = this.pokemons.indexOf(pokee);
          if (index > -1) {
            this.pokemons.splice(index, 1);
          }

        } else {
          pokee.isFavorite = !isfavorite;
          const el = document.getElementById('fav' + id);
          if (isfavorite) {
            el.classList.remove('fa-star');
            el.classList.add('fa-star-o');
          } else {
            el.classList.remove('fa-star-o');
            el.classList.add('fa-star');
          }
        }

      },
      error => console.log(error)
    );
  }

}
