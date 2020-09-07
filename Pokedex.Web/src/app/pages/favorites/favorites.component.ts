import { Component, OnInit, Input } from '@angular/core';
import { Pokemon } from 'src/Interfaces/IPokemon';
import { FavoritesService } from 'src/providers/favorites.service';
import { CommonService } from 'src/providers/common.service';
import { SecurityService } from 'src/providers/security.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css']
})
export class FavoritesComponent implements OnInit {

  pokemons: Pokemon[];
  pages: number[];

  page = 1;
  pageSize = 10;
  total = 0;

  constructor(private favoriteSrv: FavoritesService,
              private commonSrv: CommonService,
              private securitySrv: SecurityService
  ) { }

  ngOnInit(): void {
    this.loadList();
  }

  onChangePage(page: number) {
    this.page = page;
    this.loadList();
  }

  loadList() {
    this.favoriteSrv.getFavorites(this.securitySrv.securityObject.id, this.page, this.pageSize).subscribe(
      res => {

        const list = res.data;

        list.forEach((pokemon) => {
          pokemon.sprite = this.commonSrv.sanitizeBase64Image(pokemon.sprite);
        });

        this.pokemons = list;
        this.total = res.total;

        const counter = 1;
        this.pages = Array(res.totalPages).fill(null).map((_, idx) => counter + idx);
      },
      err => {

      }
    );

  }
}
