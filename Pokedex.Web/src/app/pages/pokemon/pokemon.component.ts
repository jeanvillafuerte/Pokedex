import { Component, OnInit, Input } from '@angular/core';
import { Pokemon } from 'src/Interfaces/IPokemon';

@Component({
  selector: 'app-pokemon',
  templateUrl: './pokemon.component.html',
  styleUrls: ['./pokemon.component.css']
})
export class PokemonComponent implements OnInit {

  @Input() pokemon: Pokemon;
  constructor(
  ) { }

  ngOnInit(): void {

  }

  close() {
    window.location.replace('#');
    this.pokemon = null;
  }

}
