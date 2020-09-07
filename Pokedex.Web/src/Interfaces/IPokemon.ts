import { ɵSafeResourceUrl } from '@angular/core';
import { SafeResourceUrl } from '@angular/platform-browser';

export interface Pokemon {
    id: number;
    name: string;
    types: string[];
    stats: {
        hp: number,
        attack: number,
        defense: number,
        spAttack: number,
        spDefense: number,
        speed: number
    };
    sprite: any;
    isFavorite: boolean;
}

export interface ResultPokedex {
    data: Pokemon[];
    total: number;
    totalPages: number;
}