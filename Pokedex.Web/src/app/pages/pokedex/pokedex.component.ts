import { Component, Input, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { PokedexService } from 'src/providers/pokedex.service';
import { Pokemon } from 'src/Interfaces/IPokemon';
import { DomSanitizer } from '@angular/platform-browser';
import { SearchTerm } from 'src/Interfaces/SearchTerm';
import { CommonService } from 'src/providers/common.service';

@Component({
  selector: 'app-pokedex',
  templateUrl: './pokedex.component.html',
  styleUrls: ['./pokedex.component.css']
})

export class PokedexComponent {

 

  minLength = 1;
  searchTerm: SearchTerm;
  pokemons: Pokemon[];
  pages: number[];

  previousTerm = '';
  previousPageSize = 0;

  total = 0;
  page = 1;
  pageSize = 10;

  UserId: number = null;

  constructor(private pokedexSrv: PokedexService,
    private commonSrv: CommonService) {
    this.searchTerm = new SearchTerm();
  }

  search() {

    if (this.searchTerm.term.length < 1) {
      return;
    }

    if (this.previousTerm !== this.searchTerm.term) {
      this.page = 1;
    }

    if (this.previousPageSize !== this.pageSize) {
      this.page = 1;
    }

    this.previousTerm = this.searchTerm.term;
    this.previousPageSize = this.pageSize;

    this.pokedexSrv.findByName(this.searchTerm.term, this.page, this.pageSize).subscribe(
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

  changePageSize(pageSize: number) {
    this.pageSize = pageSize;
    this.search();
  }

  onChangePage(page: number) {
    this.page = page;
    this.search();
  }

  
}
